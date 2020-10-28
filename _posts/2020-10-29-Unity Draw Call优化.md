---
layout:     post
title:      "Unity Draw Call 优化"
subtitle:   " \"面试小记\""
date:       2020-10-20 00:55:00
author:     "Mas9uerade"
header-img: "img/unityshader.jpg"
tags:
    - Unity3d
    - Optimize
---

> “最近面试被问到Unity的Draw Call优化相关的问题，遂学习了一遍并做笔记，转载自 [](http://www.cnblogs.com/xsln/p/5151951.html)”
## Unity Draw Call 优化
 *Drawcall影响的是CPU的效率。因为draw call是CPU调用图形接口在屏幕上绘制对应的东西。

### Batch
     为了在屏幕上draw一个物件（因为render和draw有些区别，所以为了区分清楚，这些概念用英文），引擎需要提供一个draw call的API。draw call调用性能开销是很大的，会导致CPU部分的性能负载。这通常是因为draw call间的状态改变（例如不同材质间的切换）导致，因为这些行为会导致显卡驱动进行开销很大的验证和转化步骤。
    Unity用static batch来处理这件事情。static batch的目的是为了用尽可能少的缓冲区来重组尽可能多的mesh，从而获得更好的性能。因而static batch会出现一些巨大的mesh被渲染，而不是很多的小mesh被渲染。合并后的这些资源虽然在不同的地方出现，但是Unity会认为是同样的资源（因为这些小资源已经合并了）来循环进行渲染。它会为每个static bached mesh来做一系列的快速的draw call。
    构建时做batch，在Unity5中只有一种方式，会构建index buffer， 然后一个draw call会被提交来用来处理合并的mesh里的每个可见的子mesh

#### 材质
    只有使用同样材质的物件才能够合并。因而，如果你想获得好的合并效果，你需要尽可能多的使不同的物件贡献同样的材质。
    如果你有两个典型的材质，它们仅仅只是贴图不同，你可以合并这些贴图到一个大的贴图里----这个过程通常叫做texture atlasing（也就是图集）。一旦贴图在同一个图集里，你就能只使用一个材质来代替。
    *texture atlasing可参见以下网页https://en.wikipedia.org/wiki/Texture_atlas
    如果你需要从脚本里获得共享重用的材质属性，你要注意修改Rendering.material将会创建这个材质的拷贝。因而你需要使用Renderer.sharedMaterial来获取找个共享的材质。

#### Static Batching（静态批次）
    静态批次，对于没有移动且公用材质的物件，能有效减少drawcall。一般来说，静态批次比动态批次更有效，但它会占用更多的内存，也会消耗更少的CPU。
    为了达到静态批次的效果，你需要显式的注明哪些物件是静态的，不移动，不旋转也不缩放的。你可以通过下面的方式来表明使用静态批次：
     
    使用静态批次，需要额外的内存来存储合并的图形。如果一些物件使用的是同一份几何图形，做静态批次后，每个物件都会创建一份几何图形，无论是在编辑器还是运行时。这不是个好的方案，因此有时不得不避免采用静态批次（虽然会损失渲染的性能），但是可以保持一个较小的内存。例如，在一个密集的森林里把树都标记成需要静态批次的，会有严重的内存影响。
    静态批次是通过转换一堆的静态物件到世界坐标系，来构建一个大的顶点和索引buffer。可见的物件在同一个静态批次，而且不会有状态的切换（状态切换的代价是很大的）

Dynamic Batching（动态批次）
    当物件使用相同的材质，并且满足一定的条件时，Unity会自动合并物件来减少draw call。Dynamic batching 是自动做的，不需要教你做其他的事情。

Tips：
    *Batching动态物件会给个顶点增加额外的开销，所以batch只适用于所有mesh合起来少于900个顶点的情况
          **如果你的shader用的是vertex position, normal和 single UV，你能合并300个顶点；如果你的shader用的是vertex position ，normal， UV0, UV1和Tangent，只能达到180个
          **PS：这条Unity5的规则数目新的版本里有可能会变

    *一般来说，物件应该使用相同的trasform scale。
          **对于不规则缩放的物件除外；如果一些物件有自己的不同的不规则的transform，他们还是会被batch的
     
    *用不同的材质实例，会导致物件不被batch(即便这些材质实例实际上是相同的)
     
    *使用光照图的物件会有额外的渲染参数：包括光照图的索引、相对光照图缩放和偏移。最好动态的光照图的物件能指向同一个光照图的位置，这样可以batch
     
    *接收实时阴影的物件不会batch
     
    *多通道的shader会破坏batch。几乎unity中所有的着色器在前向渲染中都支持多个光源，并为它们有效地开辟多个通道。为了“额外的每个像素光照”的draw call是不会batch的

Other batching tips
    目前，只有Mesh Render和粒子系统被batch。其他的组件（如skin mesh， 布料）都不会被batch。另外半透明的shader因为要渲染半透明的效果常常要求物件是从后往前来绘制。Unity会按照这个顺序给物件排序，然后再尝试batch他们。但是因为顺序是严格要求的，所以半透明的物件能被batch的效果远不如不透明的物件。
    一些Unity的render不能被batch。比如shadow casters，摄像机的深度图和GUI都会batch.

