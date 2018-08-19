---
layout:     post
title:      "Python与C的DLL调用"
subtitle:   " \"Unity Shader\""
date:       2018-05-02 15:48:00
author:     "Mas9uerade"
header-img: "img/watchdog2_sf.jpg"
tags:
    - Python
    - C
---

> “一个简单的使用Python调用C的DLL中的回调函数”

之前在思考Python如何调用C的Dll，网上也只搜到简单的加法调用，因此测试了一些简单的Python调用C的DLL的方式，附上例子。

## 具体流程 ##

##### 用简单函数指针调用 #####

由于想用C来写所以不能加C++的一些特性

首先是头文件
```C
#ifndef  _TEST_ONE_H_
#define _TEST_ONE_H_

//导出方法的定义
#define DLLExport __declspec(dllexport)

//第一阶段只导出最简单的函数
extern "C"
{
    int callback1(int a, int b);
    int callback2(int a, int b);
    DLLExport int test1(int protoid);
}

#endif // ! _TEST_ONE_H_
```

然后是源文件
```C
#include "stdafx.h"
#include "test1.h"

//根据输入的调用函数指针实现加法与减法
int test1(int protoid)
{
    int(*l_func)(int, int) = NULL;
    if (protoid == 1)
    {
        l_func = &callback1;
        return l_func(1, 1);
    }
    else if (protoid == 2)
    {
        l_func = &callback2;
        return l_func(1, 3);
    }
    return  0;
}

int callback1(int a, int b)
{
    return a + b;
}

int callback2(int a, int b)
{
    return a - b;
}
```

Python调用部分 调用ctypes这个模块实现
```C
from ctypes import *
dll = CDLL("Test1.dll")
print(dll.test1(1))
print(dll.test1(2))
```

##### 函数指针调用与结构体传参 #####

1. 测试了引用传入；
2. 测试了结果的传出（返回）
3. 使用函数指针数组作为回调，同时传入与返回；

```C
extern "C"
{
    DLLExport int test2(BasicEvent *ev);
}

extern "C"
{
    DLLExport BasicEvent test3(BasicEvent *ev);
}

extern "C"
{
    int callback3(BasicEvent*);
    int callback4(BasicEvent*);
    int callback5(int);

    typedef int(*p_func)(BasicEvent*);
    p_func funcArr[100]
    {
        funcArr[3] = callback3,
        funcArr[4] = callback4
    };

    typedef int (*x_func)(int);
    x_func funArr2[10]
    {
        funArr2[0] = callback5
    };
    

}
```
## 然后是源文件，由于返回引用指针时，python无法识别，故把回传的指针强制转换为int
```C
int test2(BasicEvent *ev)
{
    int(*m_func)(BasicEvent*) = callback3;

    if (ev->id == 3)
    {
        return callback3(ev);
    }
    else if (ev->id == 4)
    {
        return (funcArr[4](ev));
    }
    
    ev->len = 19;
    return (int)ev;
}

BasicEvent test3(BasicEvent *ev)
{
    return *ev;
}

int callback3(BasicEvent *ev)
{
    ev->id = 33;
    ev->len += 1;
    return (int)ev;
}

int callback4(BasicEvent *ev)
{
    ev->len += 2;
    return (int)ev;
}

int callback5(int a)
{
    return a + 10;
}
```

Python调用部分
1. 首先定义结构体
2. 返回结构体的话，需要设置restype返回值的类型
3. 返回引用也需要设置restype返回值类型
```C
from ctypes import *
import struct

class BasicEvent(Structure):
    _fields_ = [('a', c_int),
                ('b', c_int)]


ev = BasicEvent()
ev.a = 3
ev.b = 4

dll1 = CDLL("Test1.dll")
dll1.test3.restype = BasicEvent
dll1.test2.restype = POINTER(BasicEvent)

print(byref(ev))
ev3 = dll1.test3(byref(ev))
print(ev3.a)
print(ev3.b)

ev4 = dll1.test2(byref(ev))
print(ev4)
print(ev4.contents.a)
print(ev4.contents.b)

```

#### 总结
1. Python调用C的Dll，通过ctypes可以实现dll的调用和python数据类型与C数据类型的转换
2. Python传参时需要设置argtype与restype来设置传入参数类型与返回参数类型
3. Python调用DLL返回引用时，需要把C的地址转化为int作为返回值，再由POINTER()方法返回值类型；

