# 1. 虚函数的实现原理，父类和子类的内存布局
1. 虚函数在父类和子类中都是以虚函数指针表的形式存在的，在内存中存在类的空间（程序代码区RO）
2. 一个由c/C++编译的程序占用的内存分为以下几个部分：
   1. 栈区（stack）— 由编译器自动分配释放 ，存放函数的参数值，局部变量的值等。其操作方式类似于数据结构中的栈。 

   2. 堆区（heap） — 一般由程序员分配释放；

   3. 全局区（静态区）（static）—，全局变量和静态变量的存储是放在一块的，初始化的全局变量和静态变量在一块区域（RW）， 未初始化的全局变量和未初始化的静态变量在相邻的另一块区域（ZI）。 - 程序结束后由系统释放 

   4. 文字常量区 —常量字符串就是放在这里的。 程序结束后由系统释放 （RO）

   5. 程序代码区—存放函数体的二进制代码。 （RO）

# [2. Readonly和Const的区别](https://www.cnblogs.com/daidaibao/p/4214268.html)
1. const 是静态常量，是指编译器在编译时候会对常量进行解析，并将常量的值替换成初始化的那个值。

2. readonly是动态常量，其值则是在运行的那一刻才获得的，编译器编译期间将其标示为只读常量，而不用常量的值代替，这样动态常量不必在声明的时候就初始化，而可以延迟到构造函数中初始化。

   ##### 静态常量（Const）和动态常量（Readonly）之间的区别

   |                | 静态常量（Compile-time Constant）                  | 动态常量（Runtime Constant）                                 |
   | -------------- | -------------------------------------------------- | ------------------------------------------------------------ |
   | 定义           | 声明的同时要设置常量值。                           | 声明的时候可以不需要进行设置常量值，可以在类的构造函数中进行设置。 |
   | 类型限制       | 只能修饰基元类型，枚举类型或者字符串类型。         | 没有限制，可以用它定义任何类型的常量。                       |
   | 对于类对象而言 | 对于所有类的对象而言，常量的值是一样的。           | 对于类的不同对象而言，常量的值可以是不一样的。               |
   | 内存消耗       | 无。                                               | 要分配内存，保存常量实体。                                   |
   | **综述**       | **性能要略高，无内存开销，但是限制颇多，不灵活。** | **灵活，方便，但是性能略低，且有内存开销。**                 |

   1. Const修饰的常量在声明的时候必须初始化;Readonly修饰的常量则可以延迟到构造函数初始化 。
   2. Const常量既可以声明在类中也可以在函数体内，但是Static Readonly常量只能声明在类中。Const是静态常量，所以它本身就是Static的，因此不能手动再为Const增加一个Static修饰符。
   3. Const修饰的常量在编译期间就被解析，即：经过编译器编译后，我们都在代码中引用Const变量的地方会用Const变量所对应的实际值来代替; Readonly修饰的常量则延迟到运行的时候。
   
   ##### 动态常量（Readonly）被赋值后不可以改变
   
      ReadOnly 变量是运行时变量，它在运行时第一次赋值后将不可以改变。其中“不可以改变”分为两层意思：
   
      1. 对于值类型变量，值本身不可以改变（Readonly， 只读）
      2. 对于引用类型变量，引用本身（相当于指针）不可改变。
   
   ##### Const和Readonly的最大区别(除语法外)
   Const的变量是嵌入在IL代码中，编译时就加载好，不依赖外部dll（这也是为什么不能在构造方法中赋值）。Const在程序集更新时容易产生版本不一致的情况。
   Readonly的变量是在运行时加载，需请求加载dll，每次都获取最新的值。Readonly赋值引用类型以后，引用本身不可以改变，但是引用所指向的实例的值是可以改变的。在构造方法中，我们可以多次对Readonly赋值。
# [3. C++中的malloc和new关键字的区别](https://www.cnblogs.com/ygsworld/p/11261810.html)

##### 分配的内存空间

1. new操作符从自由存储区（free store）上为对象动态分配内存空间; 自由存储区是C++基于new操作符的一个抽象概念，凡是通过new操作符进行内存申请，该内存即为自由存储区。
2. malloc函数从堆上动态分配内存。而堆是操作系统中的术语，是操作系统所维护的一块特殊内存，用于程序的内存动态分配，C语言使用malloc从堆上分配内存，使用free释放已分配的对应内存。

**那么自由存储区是否能够是堆（问题等价于new是否能在堆上动态分配内存），这取决于operator new 的实现细节。自由存储区不仅可以是堆，还可以是静态存储区，这都看operator new在哪里为对象分配内存。**

##### 返回类型安全性

1. new操作符内存分配成功时，返回的是对象类型的指针，类型严格与对象匹配，无须进行类型转换，故new是符合类型安全性的操作符。

2. malloc内存分配成功则是返回void * ，需要通过强制类型转换将void*指针转换成我们需要的类型。

**类型安全很大程度上可以等价于内存安全，类型安全的代码不会试图方法自己没被授权的内存区域。关于C++的类型安全性可说的又有很多了。**

##### 内存分配失败时的返回值
1. new内存分配失败时，会抛出bac_alloc异常，它不会返回NULL；
2. malloc分配内存失败时返回NULL。
在使用C语言时，我们习惯在malloc分配内存后判断分配是否成功：

##### 是否需要指定内存大小
1. 使用new操作符申请内存分配时无须指定内存块的大小，编译器会根据类型信息自行计算;
2. malloc则需要显式地指出所需内存的尺寸。

##### 是否调用构造函数/析构函数
1. 使用new操作符来分配对象内存时会经历三个步骤：
	
	1. 调用operator new 函数（对于数组是operator new[]）分配一块足够大的，原始的，未命名的内存空间以便存储特定类型的对象。
	
   2. 编译器运行相应的构造函数以构造对象，并为其传入初值。
   
   3. 对象构造完成后，返回一个指向该对象的指针。
   
2. 使用delete操作符来释放对象内存时会经历两个步骤：
	1. 调用对象的析构函数。
	2. 编译器调用operator delete(或operator delete[])函数释放内存空间。
	
3. malloc则不会不会调用构造和析构函数

##### 对数组的处理
1. C++提供了new[]与delete[]来专门处理数组类型:
	1. new对数组的支持体现在它会分别调用构造函数函数初始化每一个数组元素，释放对象时为每个对象调用析构函数。注意delete[]要与new[]配套使用，不然会找出数组对象部分释放的现象，造成内存泄漏。
2. 至于malloc，它并知道你在这块内存上要放的数组还是啥别的东西，反正它就给你一块原始的内存，在给你个内存的地址就完事。所以如果要动态分配一个数组的内存，还需要我们手动自定数组的大小：
```cpp
int * ptr = (int *) malloc( sizeof(int) );//分配一个10个int元素的数组
```

##### new与malloc是否可以相互调用

operator new /operator delete的实现可以基于malloc，而malloc的实现不可以去调用new。下面是编写operator new /operator delete 的一种简单方式，其他版本也与之类似：


```cpp
void * operator new (sieze_t size)
{
      if(void * mem = malloc(size)
          return mem;
      else
          throw bad_alloc();
}
void operator delete(void *mem) noexcept
{
     free(mem);
}
```

##### 是否可以被重载

opeartor new /operator delete可以被重载。标准库是定义了operator new函数和operator delete函数的8个重载版本：


```cpp
//这些版本可能抛出异常
void * operator new(size_t);
void * operator new[](size_t);
void * operator delete (void * )noexcept;
void * operator delete[](void *0）noexcept;
//这些版本承诺不抛出异常
void * operator new(size_t ,nothrow_t&) noexcept;
void * operator new[](size_t, nothrow_t& );
void * operator delete (void *,nothrow_t& )noexcept;
void * operator delete[](void *0,nothrow_t& ）noexcept;
```

我们可以自定义上面函数版本中的任意一个，前提是自定义版本必须位于全局作用域或者类作用域中。太细节的东西不在这里讲述，总之，我们知道我们有足够的自由去重载operator new /operator delete ,以决定我们的new与delete如何为对象分配内存，如何回收对象。

而malloc/free并不允许重载。

##### 能够直观地重新分配内存

使用malloc分配的内存后，如果在使用过程中发现内存不足，可以使用realloc函数进行内存重新分配实现内存的扩充。realloc先判断当前的指针所指内存是否有足够的连续空间，如果有，原地扩大可分配的内存地址，并且返回原来的地址指针；如果空间不够，先按照新指定的大小分配空间，将原有数据从头到尾拷贝到新分配的内存区域，而后释放原来的内存区域。

new没有这样直观的配套设施来扩充内存。

##### 客户处理内存分配不足

在operator new抛出异常以反映一个未获得满足的需求之前，它会先调用一个用户指定的错误处理函数，这就是new-handler。new_handler是一个指针类型：

```cpp
namespace std
{
     typedef void (*new_handler)();
}
```

指向了一个没有参数没有返回值的函数,即为错误处理函数。为了指定错误处理函数，客户需要调用set_new_handler，这是一个声明于的一个标准库函数:

```cpp
namespace std
{
    new_handler set_new_handler(new_handler p ) throw();
}
```

set_new_handler的参数为new_handler指针，指向了operator new 无法分配足够内存时该调用的函数。其返回值也是个指针，指向set_new_handler被调用前正在执行（但马上就要发生替换）的那个new_handler函数。

对于malloc，客户并不能够去编程决定内存不足以分配时要干什么事，只能看着malloc返回NULL。

##### 总结

将上面所述的10点差别整理成表格：

| 特征               | new/delete                            | malloc/free                          |
| ------------------ | ------------------------------------- | ------------------------------------ |
| 分配内存的位置     | 自由存储区                            | 堆                                   |
| 内存分配失败返回值 | 完整类型指针                          | void*                                |
| 内存分配失败返回值 | 默认抛出异常                          | 返回NULL                             |
| 分配内存的大小     | 由编译器根据类型计算得出              | 必须显式指定字节数                   |
| 处理数组           | 有处理数组的new版本new[]              | 需要用户计算数组的大小后进行内存分配 |
| 已分配内存的扩充   | 无法直观地处理                        | 使用realloc简单完成                  |
| 是否相互调用       | 可以，看具体的operator new/delete实现 | 不可调用new                          |
| 分配内存时内存不足 | 客户能够指定处理函数或重新制定分配器  | 无法通过用户代码进行处理             |
| 函数重载           | 允许                                  | 不允许                               |
| 构造函数与析构函数 | 调用                                  | 不调用                               |

malloc给你的就好像一块原始的土地，你要种什么需要自己在土地上来播种
而new帮你划好了田地的分块（数组），帮你播了种（构造函数），还提供其他的设施给你使用:

当然，malloc并不是说比不上new，它们各自有适用的地方。在C++这种偏重OOP的语言，使用new/delete自然是更合适的。

# 4. 协程和进程的区别，怎么实现，协程程序怎么写，有什么问题

1. 进程拥有自己独立的堆和栈，既不共享堆，亦不共享栈，进程由操作系统调度。

2. 线程拥有自己独立的栈和共享的堆，共享堆，不共享栈，线程亦由操作系统调度(标准线程是的)。

3. 协程和线程一样共享堆，不共享栈，协程由程序员在协程的代码里显示调度。

### Unity协程执行原理

unity中协程执行过程中，通过yield return XXX，将程序挂起，去执行接下来的内容，注意协程不是线程，在为遇到yield return XXX语句之前，协程额方法和一般的方法是相同的，也就是程序在执行到yield return XXX语句之后，接着才会执行的是 StartCoroutine（）方法之后的程序，走的还是单线程模式，仅仅是将yield return XXX语句之后的内容暂时挂起，等到特定的时间才执行。
那么挂起的程序什么时候才执行，也就是协同程序主要是update（）方法之后，lateUpdate()方法之前调用的



Struct和class的区别，值类型和引用类型区别C#装箱和拆箱

Inline函数有啥作用，和宏定义有啥区别

闭包





Action和function区别，以及内部实现，注册函数如何防止重复，如何删除

Map怎么实现的，Dictionary如何实现

红黑树和avl树还有堆的区别，内存&效率

快排的时间空间复杂度以及实现

堆排的实现，时间空间复杂度

Enum作Key的问题



**Dictionary**的key必须是唯一的标识，因此**Dictionary**需要对 key进行判等的操作，如果key的类型没有实现 IEquatable接口，则默认根据System.Object.Equals()和GetHashCode()方法判断值是否相等。我们可以看看常用作key的几种类型在.NET Framework中的定义：

public sealed class String : IComparable, ICloneable, IConvertible, IComparable<string>, IEnumerable<string>, IEnumerable, IEquatable<string> 

public struct Int32 : IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int> 

public abstract class **Enum** : ValueType, IComparable, IFormattable, IConvertible

注意**Enum**类型的定义与前两种类型的不同，它并没有实现IEquatable接口。因此，当我们使用**Enum**类型作为key值时，**Dictionary**的内部操作就需要将**Enum**类型转换为System.Object，这就导致了Boxing的产生。它是导致**Enum**作为 key值的性能瓶颈。

CLR是什么

il2cpp和mono

如何实现一个扇形进度条

渲染管线流程，mvp变换等，

各种test

Shadowmap实现如何高效实现阴影

CPU和GPU区别，

如何设计几种反走样算法实现、问题、效率

前向渲染和延迟渲染的区别

什么时候用延迟渲染需要几个buffer，需要记录什么信息

数组和链表区别

C# GC算法

内存管理器实现

如何实现战争迷雾

Pbr最重要的参数，几个方程

**如何搭建一个pbr工作流**

Topk问题以及变种，各种解法

100万个数据快速插入删除和随机访问

1小时燃烧完的绳子，任意根，衡量出15分钟

**Unity资源相关问题，内存分布，是否copy等Unity动画相关问题

帧同步和状态同步区别等一系列问题帧同步要注意的问题**随机数如何保证同步如何设计一个技能系统以及buff系统**数组第k大的数1~n有一个数字没有，找到这个数如何

分析程序运行效率瓶颈，log分析**UI背包如何优化**Unity ui如何自适应A\*寻路实现**Unity navimesh**体素的思想和实现碰撞检测算法，优化，物理引擎检测优化连续碰撞检测**设计个UIManager，ui层级关系，ui优化**Mvc思想设计模式准则Unity优化手段，dc cpu gpu** **UML图** 消息管理器实现lua dofile和require区别Lua面向对象实现以及与区别 **如何防止大量GC** **如何实现热更****游戏AI如何实现，行为树要点** 如何实现一个延时运行功能**Unity纹理压缩格式**    Mipmap思想，内存变化

```

```