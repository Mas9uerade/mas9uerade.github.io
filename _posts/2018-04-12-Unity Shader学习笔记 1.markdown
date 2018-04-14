---
layout:     post
title:      "Unity3d Shader 入门精要笔记（1）"
subtitle:   " \"Unity Shader 入门笔记\""
date:       2018-04-14 17:57:00
author:     "Mas9uerade"
header-img: "img/watdog2_sf.jpg"
tags:
    - Unity3d
    - Shader
---

> “在读[CandyCat](Http://CandyCat1992.github.io)的Unity Shader入门精要之后，发现忘得比看得快，所以索性重看一遍并记了一段笔记”


### 渲染流程分为三个阶段
1. 应用阶段（Application Stage); CPU实现
	1. 把数据加载到显存中；
		硬盘（HDD） -> RAM -> VRAM
	2. 设置渲染状态；
		使用哪种类型的着色器，光源属性，材质等；
	3. 调用Draw Call；
2. 几何阶段（Geometry Stage);   GPU实现
3. 光栅化阶段（Rasterizer Stage); GPU实现


### GPU渲染流水线
#### 1 . 几何阶段

#### **顶点数据 （显存）-> 顶点着色器 -> 曲面细分着色器 -> 几何着色器 -> 裁剪 -> 屏幕映射**

###### 顶点着色器(Vertex Shader）
完全可编程， 实现顶点的空间变化，顶点着色的功能；
1. 实现坐标变换，把顶点坐标从模型空间转换到齐次裁剪空间，之后使用透视除法，获得归一化的设备坐标（Normalized Device Coordinates);
2. 逐顶点光照，

##### 曲面细分着色器（Tessellation Shader)
可选着色器，用于细分图元；

##### 几何着色器(Geometry Shader)
可选着色器，可以被用于执行逐图元的着色操作，或者被用于产生更多的图元；

##### 裁剪（Clipping）
可配置，剔除不在摄像机视野内的顶点，剔除某些三角图元的面片；

##### 屏幕映射（Screen Mapping）
把每个图元的x，y坐标转换到屏幕坐标系；
注意：  OpenGL与DirectX的屏幕坐标系不同；

#### 2. 光栅化阶段

#### **屏幕映射 -> 三角形设置 -> 三角形遍历 -> 片元着色器 -> 逐片元操作 -> 屏幕图像**

##### 三角形设置（Triangle Setup)
计算光栅化一个三角形网络所需的信息，通过顶点计算三角形边界的表达方式；

##### 三角形遍历（Triangle Travelsal)
检查每个像素是否被一个三角网络覆盖，若被覆盖则生成一个片元（Fragment），最终输出片元序列（包含了最终颜色，屏幕坐标信息，深度信息，法线，纹理坐标等);

##### 片元着色器(Fragment Shader)
可编程着色器，在DirectX中也称像素着色器(Pixel Shader)，实质上是通过对顶点插值，通过颜色、纹理的计算，输出最终的颜色信息。

##### 逐片元操作（Per-Fragment Operations)

**片元 -> 模板测试 ->深度测试 -> 混合 -> 颜色缓冲区（帧缓冲区）**

OpenGL的说法，在DirectX中为输出合并阶段（Output-Merger）
1. 决定每个片元的可见性，涉及到了深度测试，模板测试等；
2. 若一个片元通过测试，就需要将该片元的颜色与存储在颜色缓冲区中的颜色进行混合。

##### 颜色缓冲区（帧缓冲区）
就是帧缓冲区（图形设备的内存），需要渲染的场景的每一个像素都最终写入该缓冲区，然后由他渲染到屏幕上显示。

##### 模板测试（Stencil Test)
~~GPU会首先模板缓冲区（Stencil Buffer)中该片元位置的模板值，将该值与读取到的参考值进行比较，进而修改模板缓冲区，....~~ 模板测试中可进行渲染阴影，轮廓渲染等。
 
##### 深度测试
主要影响透明效果

#### DrawCall相关
1. draw call 对帧率的影响在于cpu提交命令的速度;
2. 减少draw call需要避免使用大量很小的网格，避免使用过多的材质。


### 着色器介绍
```c
Shader
{
    subShader
	{
    	pass
    	{
        
    	}
	}
}
```
#### 1. 表面着色器（Surface Shader）
Unity特有的对于顶点/片元着色器Vertex/Fragment Shader的抽象封装，自动完成了光照细节的处理，但相应的渲染代价更大，性能更低。
写在SubShader内，不需要考虑pass的渲染问题。
#### 2.顶点/片元着色器(Vertex/Fragment Shader)
写在Pass内，可以通过选择pass控制渲染的实现细节；
