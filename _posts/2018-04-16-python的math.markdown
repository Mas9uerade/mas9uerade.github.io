---
layout:     post
title:      "Python Number(数字)"
subtitle:   "初学python"
date:       2018-04-16 12:11:00
author:     "nankenan"
header-img: "img/watchdog2_sf.jpg"
tags:
    - Python
---

> Reposted from [nankenan](https://nankenan.github.io)

# Python Number(数字)

转载于http://www.runoob.com/python/python-numbers.html

Python 中数学运算常用的函数基本都在 math 模块、cmath 模块中。

Python math 模块提供了许多对浮点数的数学运算函数。

Python cmath 模块包含了一些用于复数运算的函数。



Python Number 数据类型用于存储数值。

数据类型是不允许改变的,这就意味着如果改变 Number 数据类型的值，将重新分配内存空间。

以下实例在变量赋值时 Number 对象将被创建：

```python
var1 = 1
var2 = 10
```

可以通过使用del语句删除单个或多个对象，例如：

```python
del var
del var_a, var_b
```

Python 支持四种不同的数值类型：

- **整型(Int)** - 通常被称为是整型或整数，是正或负整数，不带小数点。
- **长整型(long integers)** - 无限大小的整数，整数最后是一个大写或小写的L。
- **浮点型(floating point real values)** - 浮点型由整数部分与小数部分组成，浮点型也可以使用科学计数法表示（2.5e2 = 2.5 x 102 = 250）
- **复数(complex numbers)** - 复数由实数部分和虚数部分构成，可以用a + bj,或者complex(a,b)表示， 复数的实部a和虚部b都是浮点型。

| int    | long                  | float      | complex    |
| ------ | --------------------- | ---------- | ---------- |
| 10     | 51924361L             | 0.0        | 3.14j      |
| 100    | -0x19323L             | 15.20      | 45.j       |
| -786   | 0122L                 | -21.9      | 9.322e-36j |
| 080    | 0xDEFABCECBDAECBFBAEl | 32.3+e18   | .876j      |
| -0490  | 535633629843L         | -90.       | -.6545+0J  |
| -0x260 | -052318172735L        | -32.54e100 | 3e+26J     |
| 0x69   | -4721885298529L       | 70.2-E12   | 4.53e-7j   |

- 长整型也可以使用小写"L"，但是还是建议您使用大写"L"，避免与数字"1"混淆。Python使用"L"来显示长整型。
- Python还支持复数，复数由实数部分和虚数部分构成，可以用a + bj,或者complex(a,b)表示， 复数的实部a和虚部b都是浮点型

## Python数学函数

| 函数              | 返回值（描述）                                               |
| ----------------- | ------------------------------------------------------------ |
| abs(x)            | 返回数字的绝对值，如abs(-10) 返回 10                         |
| ceil(x)           | 返回数字的上入整数，如math.ceil(4.1) 返回 5                  |
| cmp(x,y)          | 如果 x < y 返回 -1, 如果 x == y 返回 0, 如果 x > y 返回 1    |
| exp(x)            | 返回e的x次幂(ex),如math.exp(1) 返回2.718281828459045         |
| fabs(x)           | 返回数字的绝对值，如math.fabs(-10) 返回10.0                  |
| floor(x)          | 返回数字的下舍整数，如math.floor(4.9)返回 4                  |
| log(x)            | 如math.log(math.e)返回1.0,math.log(100,10)返回2.0            |
| long10(x)         | 返回以10为基数的x的对数，如math.log10(100)返回 2.0           |
| max(x1,x2...)     | 返回给定参数的最大值，参数可以为序列。                       |
| min(x1,x2...)     | 返回给定参数的最小值，参数可以为序列。                       |
| modf(x)           | 返回x的整数部分与小数部分，两部分的数值符号与x相同，整数部分以浮点型表示。 |
| pow(x,y)          | 返回x的y次方                                                 |
| round( x [, n]  ) | 返回浮点数x的四舍五入值，如给出n值，则代表舍入到小数点后的位数。 |
| sqrt(x)           | 返回数字x的平方根                                            |

## Python随机数函数

随机数可以用于数学，游戏，安全等领域中，还经常被嵌入到算法中，用以提高算法效率，并提高程序的安全性。

Python包含以下常用随机数函数：

| 函数                                                         | 描述                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [choice(seq)](http://www.runoob.com/python/func-number-choice.html) | 从序列的元素中随机挑选一个元素，比如random.choice(range(10))，从0到9中随机挑选一个整数。 |
| [randrange ([start,\] stop [,step])](http://www.runoob.com/python/func-number-randrange.html) | 从指定范围内，按指定基数递增的集合中获取一个随机数，基数缺省值为1 |
| [random()](http://www.runoob.com/python/func-number-random.html) | 随机生成下一个实数，它在[0,1)范围内。                        |
| [seed([x\])](http://www.runoob.com/python/func-number-seed.html) | 改变随机数生成器的种子seed。如果你不了解其原理，你不必特别去设定seed，Python会帮你选择seed。 |
| [shuffle(lst)](http://www.runoob.com/python/func-number-shuffle.html) | 将序列的所有元素随机排序                                     |
| [uniform(x, y)](http://www.runoob.com/python/func-number-uniform.html) | 随机生成下一个实数，它在[x,y]范围内。                        |

------

## Python三角函数

Python包括以下三角函数：

| 函数                                                         | 描述                                              |
| ------------------------------------------------------------ | ------------------------------------------------- |
| [acos(x)](http://www.runoob.com/python/func-number-acos.html) | 返回x的反余弦弧度值。                             |
| [asin(x)](http://www.runoob.com/python/func-number-asin.html) | 返回x的反正弦弧度值。                             |
| [atan(x)](http://www.runoob.com/python/func-number-atan.html) | 返回x的反正切弧度值。                             |
| [atan2(y, x)](http://www.runoob.com/python/func-number-atan2.html) | 返回给定的 X 及 Y 坐标值的反正切值。              |
| [cos(x)](http://www.runoob.com/python/func-number-cos.html)  | 返回x的弧度的余弦值。                             |
| [hypot(x, y)](http://www.runoob.com/python/func-number-hypot.html) | 返回欧几里德范数 sqrt(x*x + y*y)。                |
| [sin(x)](http://www.runoob.com/python/func-number-sin.html)  | 返回的x弧度的正弦值。                             |
| [tan(x)](http://www.runoob.com/python/func-number-tan.html)  | 返回x弧度的正切值。                               |
| [degrees(x)](http://www.runoob.com/python/func-number-degrees.html) | 将弧度转换为角度,如degrees(math.pi/2) ， 返回90.0 |
| [radians(x)](http://www.runoob.com/python/func-number-radians.html) | 将角度转换为弧度                                  |

------

## Python数学常量

| 常量 | 描述                                  |
| ---- | ------------------------------------- |
| pi   | 数学常量 pi（圆周率，一般以π来表示）  |
| e    | 数学常量 e，e即自然常数（自然常数）。 |