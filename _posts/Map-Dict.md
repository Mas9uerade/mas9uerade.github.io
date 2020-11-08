# Map

hashmap的底层数据结构散列表，即：数组+链表，创建的时候初始化一个数组，每个节点可以为一个链表

# 一，前言

### 1.1，概述

 现实生活中，我们常会看到这样的一种集合：IP地址与主机名，身份证号与个人，系统用户名与系统用户对象等，这种一一对应的关系，就叫做映射（K-V）。Java提供了专门的集合类用来存放这种对象关系的对象，即`java.util.Map`接口。

- `Collection`中的集合，元素是孤立存在的（理解为单身），向集合中存储元素采用一个个元素的方式存储。
- `Map`中的集合，元素是成对存在的(理解为夫妻)。每个元素由键与值两部分组成，通过键(K)可以找对所对应的值(V)。
- `Collection`中的集合称为单列集合，`Map`中的集合称为双列集合。
- 需要注意的是，`Map`中的集合不能包含重复的键，值可以重复；每个键只能对应一个值。

![img](https://img2018.cnblogs.com/blog/1655301/201909/1655301-20190908150806989-176094445.png)
​ 通过查看Map接口描述，看到Map有多个子类，这里我们主要讲解常用的HashMap集合、LinkedHashMap集合。

- **HashMap<K,V>**：存储数据采用的哈希表结构，元素的存取顺序不能保证一致。由于要保证键的唯一、不重复，需要重写键的hashCode()方法、equals()方法。
- **LinkedHashMap<K,V>**：HashMap下有个子类LinkedHashMap，存储数据采用的哈希表结构+链表结构。通过链表结构可以保证元素的存取顺序一致；通过哈希表结构可以保证的键的唯一、不重复，需要重写键的hashCode()方法、equals()方法。

> tips：Map接口中的集合都有两个泛型变量<K,V>,在使用时，要为两个泛型变量赋予数据类型。两个泛型变量<K,V>的数据类型可以相同，也可以不同。

### 1.2，常用方法

 Map接口中定义了很多方法，常用的如下：

- `public V put(K key, V value)`: 把指定的键与指定的值添加到Map集合中。
- `public V remove(Object key)`: 把指定的键 所对应的键值对元素 在Map集合中删除，返回被删除元素的值。
- `public V get(Object key)` 根据指定的键，在Map集合中获取对应的值。
- `boolean containsKey(Object key)` 判断集合中是否包含指定的键。
- `public Set<K> keySet()`: 获取Map集合中所有的键，存储到Set集合中。
- `public Set<Map.Entry<K,V>> entrySet()`: 获取到Map集合中所有的键值对对象的集合(Set集合)。

> tips:
>
>  1，使用put方法时，若指定的键(key)在集合中没有，则没有这个键对应的值，返回null，并把指定的键值添加到集合中；
>
>  2，若指定的键(key)在集合中存在，则返回值为集合中键对应的值（该值为替换前的值），并把指定键所对应的值，替换成指定的新值。

# 二，哈希表

 Map的底层都是通过哈希表进行实现的，那先来看看什么是哈希表。

 **JDK1.8**之前，哈希表底层采用**数组+链表**实现，即使用链表处理冲突，同一hash值的链表都存储在一个链表里。但是当位于一个桶中的元素较多，即hash值相等的元素较多时，通过key值依次查找的效率较低。

 **JDK1.8中**，哈希表存储采用**数组+链表+红黑树**实现，当链表长度超过阈值（8）时，将链表转换为**红黑树**，这样大大减少了查找时间。如下图（画的不好看，只是表达一个意思。）：

![img](https://img2018.cnblogs.com/blog/1655301/201909/1655301-20190908150835369-1148673490.png)
​ 说明：

 1，进行键值对存储时，先通过hashCode()计算出键（K）的哈希值，然后再数组中查询，如果没有则保存。

 2，但是如果找到相同的哈希值，那么接着调用equals方法判断它们的值是否相同。只有满足以上两种条件才能认定为相同的数据，因此对于Java中的包装类里面都重写了hashCode()和equals()方法。

 **JDK1.8**引入红黑树大程度优化了HashMap的性能，根据对象的hashCode和equals方法来决定的。如果我们往集合中存放自定义的对象，那么保证其唯一，就必须复写hashCode和equals方法建立属于当前对象的比较方式。

 下面说说关于hashCode()和equals()方法。

### 2.1，hashCode()和equals()

 关于hashCode()我们先来看看API文档的解释。

![img](https://img2018.cnblogs.com/blog/1655301/201909/1655301-20190908150847633-595211233.png)

 再来看equals()的API解释：

 ![img](https://img2018.cnblogs.com/blog/1655301/201909/1655301-20190908150859027-2076951776.png)

 总结：通过直接观看API文档中的解释，在结合哈希表的特点。我们得知为什么要使用hashCode()方法和equals()方法来作为元素是否相同的判断依据。

 1，使用hashCode()方法可以提高查询效率，假如现在有10个位置，存储某个元素如果说没有哈希值的使用，要查找该元素就要全部遍历，在效率上是缓慢的。而通过哈希值就可以很快定位到该元素的位置，节省了遍历数组的时间。

 2，但是通过哈希值就能确定唯一的值吗，当然不是。因此才需要使用equals再次进行判断。判断的目的在于当元素哈希值相等时，使用equals判断它们到底是不是同一个对象，如果是则代表是同一个元素，否则不是同一个元素那么就将其保存到链表上。

 因此哈希值的使用就是为提高查询速度，equals的使用就是判断对象是否为重复元素。

### 2.2，Map遍历方式

 在文章上面讲述到，map保存的是键值对形式，也就是说K和V的类型有可能是不一样的，也有可能是自定义的对象。因此不能像使用普通for循环遍历集合去遍历map集合。别急，在Map中已经为我们提供的两种方式，keySet()和entrySet()。

 1，keySet()方式

![img](https://img2018.cnblogs.com/blog/1655301/201909/1655301-20190908150913162-1749626620.png)

 通过该方法可以获取map中的全部key，返回的是一个set集合。那么获取到map中的key难道还拿不到对应的value吗。请看如下代码：

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
Map<String,Object> map = new HashMap<>();
        map.put("Hello","World");
        map.put("你好","世界");

        // 1，通过ketSet方式获取map集合中的key
        Set<String> keySet = map.keySet();
        // 通过迭代器方式获取key，先获取一个迭代器
        Iterator<String> setIterator = keySet.iterator();
        while(setIterator.hasNext()){
            // 获取key
            String keyMap = setIterator.next();
            Object obj = map.get(keyMap);
            System.out.println("map--->"+obj);
        }

        // 2，使用增强for遍历set集合
        for(String key : keySet){
            System.out.println(map.get(key));
        }
        // 简化for循环
        for(String key : map.keySet()){
            System.out.println(map.get(key));
        }
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

 2，entrySet()

![img](https://img2018.cnblogs.com/blog/1655301/201909/1655301-20190908150925450-1046172037.png)

 我们已经知道，`Map`中存放的是两种对象，一种称为**key**(键)，一种称为**value**(值)，它们在在`Map`中是一一对应关系，这一对对象又称做`Map`中的一个`Entry(项)`。`Entry`将键值对的对应关系封装成了对象。即键值对对象，这样我们在遍历`Map`集合时，就可以从每一个键值对（`Entry`）对象中获取对应的键与对应的值。

既然Entry表示了一对键和值，那么也同样提供了获取对应键和对应值得方法：

- `public K getKey()`：获取Entry对象中的键。
- `public V getValue()`：获取Entry对象中的值。

在Map集合中也提供了获取所有Entry对象的方法：

- `public Set<Map.Entry<K,V>> entrySet()`: 获取到Map集合中所有的键值对对象的集合(Set集合)。

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
// 使用Entry键值对
        Set<Map.Entry<String, Object>> entrySet = map.entrySet();

        // 1，使用迭代器
        Iterator<Map.Entry<String, Object>> iterator = entrySet.iterator();
        while(iterator.hasNext()){
            Map.Entry<String, Object> entry = iterator.next();
            System.out.println(entry.getKey()+"--->"+entry.getValue());
        }
        System.out.println("=============================");

        // 2，使用for循环遍历
        for(Map.Entry<String,Object> entry : map.entrySet()){
            System.out.println(entry.getKey()+"--->"+entry.getValue());
        }
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

# 三，Hash Map实现原理

### 3.1，常量的使用

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
//创建 HashMap 时未指定初始容量情况下的默认容量   
      static final int DEFAULT_INITIAL_CAPACITY = 1 << 4; 
  
  　 //HashMap 的最大容量
      static final int MAXIMUM_CAPACITY = 1 << 30;
  
      //HashMap 默认的装载因子,当 HashMap 中元素数量超过 容量装载因子时，进行resize()操作,至为什么是0.75，官方说法是这个值是最佳的阈值。
      static final float DEFAULT_LOAD_FACTOR = 0.75f;
  
     //用来定义在哈希冲突的情况下，转变为红黑树的阈值
     static final int TREEIFY_THRESHOLD = 8;
 
     // 用来确定何时将解决 hash 冲突的红黑树转变为链表
     static final int UNTREEIFY_THRESHOLD = 6;
  
     /* 当需要将解决 hash 冲突的链表转变为红黑树时，需要判断下此时数组容量，若是由于数组容量太小（小于　MIN_TREEIFY_CAPACITY　）导致的 hash 冲突太多，则不进行链表转变为红黑树操作，转为利用　resize() 函数对　hashMap 扩容　*/
17     static final int MIN_TREEIFY_CAPACITY = 64;
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

### 3.2，node节点类

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
// Node是单向链表，它实现了Map.Entry接口并且实现数组及链表的数据结构  
static class Node<k,v> implements Map.Entry<k,v> {  
    final int hash;  // 保存元素的哈希值
    final K key;     // 保存节点的key
    V value;         // 保存节点的value
    Node<k,v> next;  // 链表中，指向下一个链表的节点
    //构造函数Hash值 键 值 下一个节点  
    Node(int hash, K key, V value, Node<k,v> next) {  
        this.hash = hash;  
        this.key = key;  
        this.value = value;  
        this.next = next;  
    }  
   
    public final K getKey()        { return key; }  
    public final V getValue()      { return value; }  
    public final String toString() { return key + = + value; }  
   
    public final int hashCode() {  
        return Objects.hashCode(key) ^ Objects.hashCode(value);  
    }  
   
    public final V setValue(V newValue) {  
        V oldValue = value;  
        value = newValue;  
        return oldValue;  
    }  
    //重写equals方法，与我们上文中讲述的原理
    public final boolean equals(Object o) {  
        if (o == this)  
            return true;  
        if (o instanceof Map.Entry) {  
            Map.Entry<!--?,?--> e = (Map.Entry<!--?,?-->)o;  
            if (Objects.equals(key, e.getKey()) &&  
                Objects.equals(value, e.getValue()))  
                return true;  
        }  
        return false;  
    }
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

### 3.3，红黑树源码

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
static final class TreeNode<K,V> extends LinkedHashMap.Entry<K,V> {
        TreeNode<K,V> parent;  // 定义红黑树父节点
        TreeNode<K,V> left;    // 左子树
        TreeNode<K,V> right;   // 右子树
        TreeNode<K,V> prev;    // 上一个节点，后期会根据上一个节点作相应判断
        boolean red;           // 判断颜色的属性
        TreeNode(int hash, K key, V val, Node<K,V> next) {
            super(hash, key, val, next);
        }

        /**
         * 返回根节点
         */
        final TreeNode<K,V> root() {
            for (TreeNode<K,V> r = this, p;;) {
                if ((p = r.parent) == null)
                    return r;
                r = p;
            }
        }
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

### 3.4，构造函数

 第一种构造函数，指定初始化容量大小及装载因子。

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public HashMap(int initialCapacity, float loadFactor) {
        if (initialCapacity < 0)
            throw new IllegalArgumentException("Illegal initial capacity: " +initialCapacity);
        if (initialCapacity > MAXIMUM_CAPACITY)
            initialCapacity = MAXIMUM_CAPACITY;
        if (loadFactor <= 0 || Float.isNaN(loadFactor))
            throw new IllegalArgumentException("Illegal load factor: " +
                                               loadFactor);
        this.loadFactor = loadFactor;
        this.threshold = tableSizeFor(initialCapacity);
    }
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

 第二种构造函数，仅指定装载因子。

```
public HashMap(int initialCapacity) {
        this(initialCapacity, DEFAULT_LOAD_FACTOR);
    }
```

 第三种构造函数，所有的参数都使用默认值。

```
public HashMap() {
        this.loadFactor = DEFAULT_LOAD_FACTOR; // all other fields defaulted
    }
```

### 3.5，put方法

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public V put(K key, V value) {  
        return putVal(hash(key), key, value, false, true); 
    }  
    
final V putVal(int hash, K key, V value, boolean onlyIfAbsent,  
                   boolean evict) {  
        Node<K,V>[] tab;   
    Node<K,V> p;   
    int n, i;  
        if ((tab = table) == null || (n = tab.length) == 0)  
            n = (tab = resize()).length; 
        
    // 1，如果table的在（n-1）&hash的值是空，就新建一个节点插入在该位置 
        if ((p = tab[i = (n - 1) & hash]) == null)  
            tab[i] = newNode(hash, key, value, null);  
    // 2，否则表示有冲突,开始处理冲突  
        else {  
            Node<K,V> e;   
        K k;  
        
    // 3，接着检查第一个Node，p是不是要找的值 
            if (p.hash == hash &&((k = p.key) == key || (key != null && key.equals(k))))  
                e = p;  
            else if (p instanceof TreeNode)  
                e = ((TreeNode<K,V>)p).putTreeVal(this, tab, hash, key, value);  
            else {  
                for (int binCount = 0; ; ++binCount) {  
        // 4，如果指针为空就挂在后面  
                    if ((e = p.next) == null) {  
                        p.next = newNode(hash, key, value, null);  
                        if (binCount >= TREEIFY_THRESHOLD - 1) // -1 for 1st  
                            treeifyBin(tab, hash);  
                        break;  
                    }  
        // 5，如果有相同的key值就结束遍历 
                    if (e.hash == hash &&((k = e.key) == key || (key != null && key.equals(k))))  
                        break;  
                    p = e;  
                }  
            }  
            // 6，就是链表上有相同的key值 
            if (e != null) { // existing mapping for key，就是key的Value存在  
                V oldValue = e.value;  
                if (!onlyIfAbsent || oldValue == null)  
                    e.value = value;  
                afterNodeAccess(e);  
                return oldValue;//返回存在的Value值  
            }  
        }  
        ++modCount;  
     // 7,如果当前大小大于定义的阈值，0.75f 
        if (++size > threshold)  
            resize();//扩容两倍  
        afterNodeInsertion(evict);  
        return null;  
    }
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

### 3.6，get方法

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public V get(Object key) {  
        Node<K,V> e;  
        return (e = getNode(hash(key), key)) == null ? null : e.value;  
    }  
     // 1，通过哈希值和key查找元素
    final Node<K,V> getNode(int hash, Object key) {  
        Node<K,V>[] tab;//Entry对象数组  
    Node<K,V> first,e; 
    int n;  
    K k;  
    // 2，找到插入的第一个Node，方法是hash值和n-1相与，tab[(n - 1) & hash]  
    //  表示在一条链上的hash值相同的  
        if ((tab = table) != null && (n = tab.length) > 0 &&(first = tab[(n - 1) & hash]) != null) {  
    // 3，检查第一个Node是不是要找的Node  
            if (first.hash == hash && // always check first node  
            // 4，判断条件是hash值要相同，key值要相同  
                ((k = first.key) == key || (key != null && key.equals(k))))
                return first;  
      // 5，检查下一个元素 
            if ((e = first.next) != null) {  
                if (first instanceof TreeNode)  
                    return ((TreeNode<K,V>)first).getTreeNode(hash, key);  
                // 6，遍历后面的链表，找到key值和hash值都相同的Node节点
                do {  
                    if (e.hash == hash &&  
                        ((k = e.key) == key || (key != null && key.equals(k))))  
                        return e;  
                } while ((e = e.next) != null);  
            }  
        }  
        return null;  
    }
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

### 3.7，扩容机制

 hashmap扩容是一件相对很耗时的事情，在初始化hash表结构时，如果没有指定大小则默认为16，也就是node数组的大小。当容量达到最大值时，扩容到原来的2倍。

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
final Node<K,V>[] resize() {  
       Node<K,V>[] oldTab = table;  
       int oldCap = (oldTab == null) ? 0 : oldTab.length;  
       int oldThr = threshold;  
       int newCap, newThr = 0;  
      
    // 1，判断旧表的长度不是空，且大于最大容量
       if (oldCap > 0) {  
           if (oldCap >= MAXIMUM_CAPACITY) {  
               threshold = Integer.MAX_VALUE;  
               return oldTab;  
           }  
        // 2，把新表的长度设置为旧表长度的两倍，newCap=2*oldCap 
           else if ((newCap = oldCap << 1) < MAXIMUM_CAPACITY &&  
                    oldCap >= DEFAULT_INITIAL_CAPACITY)  
      // 3，把新表的阈值设置为旧表阈值的两倍，newThr=oldThr*2  
               newThr = oldThr << 1; // double threshold  
       }  
        // 初始容量设置新的阈值
       else if (oldThr > 0) // initial capacity was placed in threshold  
           newCap = oldThr;  
       else {               // zero initial threshold signifies using defaults  
           newCap = DEFAULT_INITIAL_CAPACITY;  
           newThr = (int)(DEFAULT_LOAD_FACTOR * DEFAULT_INITIAL_CAPACITY);  
       }      
       if (newThr == 0) {  
           float ft = (float)newCap * loadFactor;//新表长度乘以加载因子  
           newThr = (newCap < MAXIMUM_CAPACITY && ft < (float)MAXIMUM_CAPACITY ?  
                     (int)ft : Integer.MAX_VALUE);  
       }  
       threshold = newThr;  
       @SuppressWarnings({"rawtypes","unchecked"})  
        // 4，创建新的表，并初始化原始数据  
       Node<K,V>[] newTab = (Node<K,V>[])new Node[newCap];  
       table = newTab;//把新表赋值给table  
       // 原表不是空要把原表中数据移动到新表中   
       if (oldTab != null) {   
           // 开始遍历原来的旧表        
           for (int j = 0; j < oldCap; ++j) {  
               Node<K,V> e;  
               if ((e = oldTab[j]) != null) {  
                   oldTab[j] = null;  
                   if (e.next == null)//说明这个node没有链表直接放在新表的e.hash & (newCap - 1)位置  
                       newTab[e.hash & (newCap - 1)] = e;  
                   else if (e instanceof TreeNode)  
                       ((TreeNode<K,V>)e).split(this, newTab, j, oldCap);  
                   else { // preserve order保证顺序  
                //新计算在新表的位置，并进行搬运  
                       Node<K,V> loHead = null, loTail = null;  
                       Node<K,V> hiHead = null, hiTail = null;  
                       Node<K,V> next;  
                       do {  
                           next = e.next;//记录下一个结点  
                          //新表是旧表的两倍容量，实例上就把单链表拆分为两队，    
                           if ((e.hash & oldCap) == 0) {  
                               if (loTail == null)  
                                   loHead = e;  
                               else  
                                   loTail.next = e;  
                               loTail = e;  
                           }  
                           else {  
                               if (hiTail == null)  
                                   hiHead = e;  
                               else  
                                   hiTail.next = e;  
                               hiTail = e;  
                           }  
                       } while ((e = next) != null);  
                      
                       if (loTail != null) {//lo队不为null，放在新表原位置  
                           loTail.next = null;  
                           newTab[j] = loHead;  
                       }  
                       if (hiTail != null) {//hi队不为null，放在新表j+oldCap位置  
                           hiTail.next = null;  
                           newTab[j + oldCap] = hiHead;  
                       }  
                   }  
               }  
           }  
       }  
       return newTab;  
   }
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

 map集合的扩容要比list集合复杂的多。

# 四，Linked Hash Map

 对于LinkedHashMap而言，它继承与HashMap、底层使用**哈希表与双向链表**来保存所有元素。其基本操作与父类HashMap相似，它通过重写父类相关的方法，来实现自己的链接列表特性，同时它也保证元素是有序的。

# 五，Hash Table

 Hashtable的实现原理和Hash Map是类似的，但区别是它是线程安全的，也正因为如此导致查询速度较慢。

![img](https://img2018.cnblogs.com/blog/1655301/201909/1655301-20190908150958766-538121585.png)

 **注意：Hash Table中K和V是不允许存储null值。**

# 六，Hash Set

Hash Set底层就是通过Hash Map实现的。

![img](https://img2018.cnblogs.com/blog/1655301/201909/1655301-20190908151012491-1199390757.png)

 从源码看出这简直是赤裸裸的new了一个Hash Map，虽然原理类似但多少有区别，毕竟这是两个体系的集合。

 最根本区别在于set集合存储单值对象。而map是键值对，但有一个相同点是都不能存储重复元素。

 **注意：Hash Set也是线程不安全的。**