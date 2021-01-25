---

layout:     post
title:      "Unity Shader 概念重析"
subtitle:   " \"面试小记\""
date:       2020-11-29 15:48:49
author:     "Mas9uerade"
header-img: "img/IMG_2616.jpg"
tags:
    - Unity 
    - Unity Shader
---

> “面试知识点整理，转自 [[Unity 官方推送](https://mp.weixin.qq.com/s/gVsOjetvZKdQrtZ0wFOAdQ)]

# Unity Shader 总结

## 1. Unity Shader 概念重析

### **1 .1 三大 Shader 编程语言（CG/HLSL/GLSL）**

Shader Language的发展方向是设计出在便携性方面可以和C++、Java等相比的高级语言，“赋予程序员灵活而方便的编程方式”，并“尽可能的控制渲染过程”同时“利用图形硬件的并行性，提高算法效率”。

Shader Language目前主要有3种语言：基于 OpenGL 的 OpenGL Shading Language，简称 GLSL; 基于 DirectX 的 High Level Shading Language, 简称 HLSL; 基于 NVIDIA 公司的 C for Graphic，简称 Cg 语言。



### 1.2 渲染管线

渲染管线也称为渲染流水线，是显示芯片内部处理图形信号相互独立的并行处理单元。

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QdCkEqCzqZ2kQHGteBSNeEzATlorAwBU0H8XxibjxUEK5lxFibFvmTkicQ/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)



### 1.3 Shader的基础定义

Gpu流水线上一些可高度编程的阶段，而由着色器编译出来的最终代码是会在Gpu上运行的；有一些特定类型的着色器，如顶点着色器，片元着色器等。依靠着色器我们可以控制流水线中的渲染细节，例如用顶点着色器来进行顶点变换及传递数据，用片元着色器来进行逐像素渲染。



### 1.4 Unity Shader并非真正的Shader

Unity Shader实际上指的就是一个ShaderLab文件。以.shader作为后缀的一种文件。在Unity shader里面，我们可以做的事情远多于一个传统意义上的Shader。在传统的shader中，我们仅可以编写特定类型的Shader，例如顶点着色器，片元着色器等。在Unity Shader中，我们可以在同一个文件里面同时包含需要的顶点着色器和片元着色器代码。在传统shader中，我们无法设置一些渲染设置，例如是否开启混合，深度测试等，这些是开发者在另外的代码中自行设置的。而Unity shader中，我们通过一行特定的指令就可以完成这些设置。在传统shader中，我们需要编写冗长的代码设置着色器的输入和输出，要小心的处理这些输入输出的位置对应关系等。而在Unity shader中，我们只需要在特定语句块中声明一些属性，就可以依靠材质来方便的改变这些属性。而对于模型自带的数据（如顶点，纹理坐标，法线等），Unity Shader也提供了直接访问的方法，不需要开发者自行编码来传给着色器。



## 2.  **Unity Shader 关键知识点提炼**

### 2.1 Cg编程

通常采用动态编译的方式（Cg也支持静态编译方式），即在宿主程序运行时，利用Cg运行库（Cg Runtimer Library）动态编译Cg代码。



Cg数据类型：

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QWEstxTLOkXxl6qjMMHQjOKUGGjwMHNYy61C1BQZg9Pyib0ZVGVXmrQA/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)



### 2.2 **Unity Shader ShaderLab编程**

**01 基础功能类型函数**

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QL1bvwoDeibfUfkvtaBkicUeibuicBaQu77LcUGkjq9wacyoTeibeW0N6X6A/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

**几何函数：**

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QwDwDpxiaceaY1pp3iaWRVwc0C9J7QLUnzsNibgpDWtOQMBTVCbSjVO8YQ/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

**纹理函数：**

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QciblWjt16uCPjnhksOzJkDb1yIYHic4DxwocYBXPPa1pK024fn2oLPrg/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)



***02*** **渲染顺序（Render Queue）**



![img](https://mmbiz.qpic.cn/mmbiz_jpg/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QVbhYcQEJ5tXv6Y8PHVaEEb51fmwr5emDMyx4PWKFCIVNmlPfByWh4w/640?wx_fmt=jpeg&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)



***03*** **透明效果**：只要一个片元的透明度不满足条件（通常小于某个阈值），那么就舍弃对应的片元。被舍弃的片元不会在进行任何的处理，也不会对颜色缓冲产生任何影响；否则，就会按照普通的不透明物体来处理，即进行深度测试，深度写入等等。虽然简单，但是很极端，要么完全透明，要么完全不透明。



**透明度混合**：可以得到真正的半透明效果，它会使当前片元的透明度作为混合因子，与已经储存在颜色缓冲中的颜色值进行混合么，得到新的颜色。但是，透明度混合需要关闭深度写入，这使得我们要非常小心物体的渲染顺序。注意：透明度混合只关闭了深度写入，但没有关闭深度测试。这表示当使用透明度混合渲染一个片元时，还是会比较它的深度值与当前深度缓冲中的深度值，如果深度值距离摄像机更远，那么就不会在进行混合操作。比如一个不透明物体在透明物体前面，我们先渲染不透明物体，可以正常的挡住不透明物体。



**混合操作：**

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QYvd7BmK90icy8FXebZx1RbVpmf3tanKNdD3ia69ibW6eA9K2dusa2TMOA/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)



**混合因子：**

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QIzh9VbS7ibnphp3XRcLicAcciaKB5Cn9dKr4RSj3W9QloOTcO9A1hXn3Q/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3Q0aVocs0PRuf1rXg8vs6mFx9VFr8QKVa8ZQa4g0dtMjKpibJAnYkdOgg/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)



**常见混合操作类型应用：**

**A**  //正常（Normal）透明度混合 Blend SrcAlpha OneMinusSrcAlpha

**B**  //柔和相加 Blend OneMinusDstColor One

**C**  //正片叠底 Blend DstColor Zero

**D**  //两倍相乘 Blend DstColor SrcColor

**E**  //变暗 BlendOp min Blend One One

**F**  //变亮 Blend OneMinusDstColor One Blend One OneMinusSrcColor

**G**  //线性减淡 Blend One One



## 3. 光照模型

![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QMGFoVVav7JSJZ9hvncjAsHTPIia6dIYx9fMeUQTKwT622Z6oBSicEP9A/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

## 4. 表面着色器
用。使用表面着色器，用户仅需要编写最关键的表面函数，其余周边代码将由Unity自动生成，包括适配各种光源类型、渲染实时阴影以及集成到前向/延迟渲染管线中等。

**01  编写表面着色器有几个规则**

 表面着色器的实现代码需要放在CGPROGRAM..ENDCG代码块中，而不是Pass结构中，它会自己编译到各个Pass。

 使用#pragma surface..命令来指明它是一个表面着色器。



**A **  #pragma surface surfaceFunction lightModel [optionalparams]：Surface Shader和CG其他部分一样，代码也是要写在CGPROGRAM和ENDCG之间。但区别是，它必须写在SubShader内部，而不能写在Pass内部。Surface Shader自己会自动生成所需的各个Pass。由上面的编译格式可以看出，surfaceFunction和lightModel是必须指定的。

**B**  surfaceFunction通常就是名为surf的函数（函数名可以任意），它的函数格式是固定的：
void surf (Input IN, inout SurfaceOutput o)
void surf (Input IN, inout SurfaceOutputStandard o)
void surf (Input IN, inout SurfaceOutputStandardSpecular o)

**C**  lightModel也是必须指定的。由于Unity内置了一些光照函数——Lambert（diffuse）和Blinn-Phong（specular），因此这里在默认情况下会使用内置的Lambert模型。当然我们也可以自定义。

**D**  optionalparams包含了很多可用的指令类型，包括开启、关闭一些状态，设置生成的Pass类型，指定可选函数等。除了上述的surfaceFuntion和lightModel，我们还可以自定义两种函数：vertex:VertexFunction和finalcolor:ColorFunction。也就是说，Surface Shader允许我们自定义四种函数。



**02  计算函数汇总**

 **//最重要的计算函数**
void surf (Input IN, inout SurfaceOutput o)
void surf (Input IN, inout SurfaceOutputStandard o)
void surf (Input IN, inout SurfaceOutputStandardSpecular o)

 **//顶点修改**
void vert (inout appdata_full v)
void vert(inout appdata_full v, out Input o)

 //unity老版本（新版本兼容）不包含GI的
half4 Lighting (SurfaceOutput s, half3 lightDir, half atten)
half4 Lighting (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)

**//unity新版本（包含GI，要自定义GI函数）**
half4 Lighting (SurfaceOutput s, UnityGI gi)
half4 Lighting (SurfaceOutput s, half3 viewDir, UnityGI gi)

 **//延迟渲染**
half4 Lighting_Deferred (SurfaceOutput s, UnityGI gi, out half4 outDiffuseOcclusion, out half4 outSpecSmoothness, out half4 outNormal)

 //遗留的延迟渲染
half4 Lighting_PrePass (SurfaceOutput s, half4 light)

 **//自定义GI**
half4 Lighting_GI (SurfaceOutput s, UnityGIInput data, inout UnityGI gi);

**//着色**
void frag(Input IN, SurfaceOutput o, inout fixed4 color)

 **//最终颜色修改**
void final(Input IN, SurfaceOutput o, inout fixed4 color)
void final(Input IN, SurfaceOutputStandard o, inout fixed4 color)
void final(Input IN, SurfaceOutputStandardSpecular o, inout fixed4 color)

两个结构体就是指struct Input和SurfaceOutput。其中Input结构体是允许我们自定义的。如下表。这些变量只有在真正使用的时候才会被计算生成。在一个贴图变量之前加上uv两个字母，就代表提取它的uv值，例如uv_MainTex 。

另一个结构体是（SurfaceOutput、SurfaceOutputStandard和SurfaceOutputStandardSpecular）。我们也可以自定义这个结构体内的变量，自定义最少需要有4个成员变量：Albedo、Normal、Emission和Alpha，缺少一个都会报错。关于它最难理解的也就是每个变量的具体含义以及工作机制（对像素颜色的影响）。

```CG
struct SurfaceOutput 
{
    fixed3 Albedo;
    fixed3 Normal;
    fixed3 Emission;
    half Specular;
    fixed Gloss;
    fixed Alpha;
};
```

**Albedo**：我们通常理解的对光源的反射率。它是通过在Fragment Shader中计算颜色叠加时，和一些变量（如vertex lights）相乘后，叠加到最后的颜色上的。(漫反射颜色)

**Normal**：即其对应的法线方向。只要是受法线影响的计算都会受到影响。

**Emission**：自发光。会在Fragment 最后输出前（调用final函数前，如果定义了的话），使用下面的语句进行简单的颜色叠加：c.rgb += o.Emission;

**Specular**：高光反射中的指数部分的系数。影响一些高光反射的计算。按目前的理解，也就是在光照模型里会使用到（如果你没有在光照函数等函数——包括Unity内置的光照函数，中使用它，这个变量就算设置了也没用）。有时候，你只在surf函数里设置了它，但也会影响最后的结果。这是因为，你可能使用了Unity内置的光照模型，如BlinnPhong，它会使用如下语句计算高光反射的强度（在Lighting.cginc里）：float spec = pow (nh, s.Specular*128.0) * s.Gloss;

**Gloss**：高光反射中的强度系数。和上面的Specular类似，一般在光照模型里使用。

**Alpha**：通常理解的透明通道。在Fragment Shader中会直接使用下列方式赋值（如果开启了透明通道的话）：c.a = o.Alpha;

**Unity Shader实战应用**



因为涉及的代码量比较大，小编这边直接在GitHub上分享通用效果测试的工程。



![img](https://mmbiz.qpic.cn/mmbiz_png/YIXoZTpc5xefJHnsmWZ5DwVV1QZ12o3QgIQY2uUWUPfDvQib8Kb9LfIiakuK5IkKPewBTZHOHcz6WI6v8sRLmutQ/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)



部分测试效果展示（雪融&火焰流动效果）：