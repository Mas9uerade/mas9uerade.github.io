# UGUI drawcall合并原理

高数量的drawcall带来的坏处不用多说了，本篇重点说的是UGUI是如何合并drawcall的。 通过这篇博客，你将学会如何精算一个UGUI界面到底有几个drawcall，并且能想象出各UI控件的渲染顺序（即Frame Debugger窗口里的渲染顺序）。
以下案列的unity版本：
![这里写图片描述](https://img-blog.csdn.net/20180708172217243?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

\##一、 概念篇
在学习本篇之前，你需要了解以下几个名词。

### bottomUI

A是B的bottomUI需要满足：(单条只是必要条件，1、2、3合起来才是充分条件）

1. **B的mesh构成的矩形和A的mesh构成的矩形有相交，注意不是RectTransform的矩形相交，这点需要认真理解一下，下面给出一组案列帮助大家理解。**
2. **A.siblingIndex < B.siblingIndex (即在Hierachy里A在B之上）**
3. **如果B有多个UI满足1、2条规则，则B的bottomUI应取siblingIndex差值的绝对值最小的那个（有点绕哈，depth的案例有这种情况）**

![这里写图片描述](https://img-blog.csdn.net/20180707125511105?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)
![这里写图片描述](https://img-blog.csdn.net/20180707125253665?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)
黑色的线框即是mesh的矩形了，上图的Text组件和image组件是没有相交的，但注意他们的RectTransform其实是已经有相交了。此时，**Text组件不能算作Image组件的bottomUI**，因为不满足第1条。

### 合批

当两个UI控件的材质球的instanceId（材质球的instanceId和纹理）一样，那么这两个UI控件才有可能合批

### depth

depth是UGUI做渲染排序的第一参考值，它是通过一些简单的规则计算出来的。 在理解了bottomUI的基础上，depth就很好算了。先给出计算规则：
![这里写图片描述](https://img-blog.csdn.net/20180707172938252?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

下面给出一个案例来帮助理解:
![这里写图片描述](https://img-blog.csdn.net/20180707174612358?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

![这里写图片描述](https://img-blog.csdn.net/20180707174626818?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

根据树的深度优先遍历，容易得到UI节点遍历顺序：I1、T1、I2、R2、R1
根据depth的计算规则，得到每个UI控件的depth值

| UI控件名称 | depth值 | 说明（I=Image、T=Text、R=RawImage）                          |
| ---------- | ------- | ------------------------------------------------------------ |
| I1         | 0       |                                                              |
| T1         | 1       |                                                              |
| I2         | 2       | I2的bottomUI是T1，且两者的mesh有相交，还不能合批，所以I2.depth = T1.depth + 1 = 1 + 1 = 2 |
| R2         | 2       | 先来确定R2的bottomUI应该是I2，而非I1，因为依据bottomUI规则的第3条，R2.siblingIndex - I2.siblingIndex = 1,R2.siblingIndex - I1.siblingIndex = 3。然后R2和I2能够合批（为什么能够合批，后文会做解释），所以 R2.depth = I2.depth = 2 |
| R1         | 2       |                                                              |

这样就算出了各个UI控件的depth值了。
不要以为 I2 和 R2 的控件类型不一样就不能合批了，UGUI的渲染引擎不会去考虑两个UI控件类型是否一样，它只考虑两个UI控件的材质球及其参数是否一样，如果一样，就可以合批，否则不能合批。而实际项目中，我们往往都会认为一个RawImage就会占用一个drawcall，其实这个说法只是一种经验，并不完全正确。因为我们使用RawImage的时候都是拿来显示一些单张的纹理，比如好友列表里的头像，如果这些头像都是玩家自定义上传的头像，往往互不相同，当渲染到RawImage的时候，就会导致头像的材质球使用的纹理不同而导致不能合批而各占一个drawcall。但如果是使用的系统头像，那么就可以让两个使用了相同系统头像的RawImage合批。我们这个案例，I2和R2使用的材质球（Default UI Material) 和 纹理（Unity White）都是一样的，所以能够合批。

### 材质球ID

材质球的 InstanceID

### 纹理ID

纹理的InstanceID

## 二、排序and计算drawcall 数

有了上面的数据，UGUI会对所有的UI控件(CanvasRenderer)按depth、材质球ID、纹理ID做一个排序，那么这些字段的排序优先级也是有规定的：

![这里写图片描述](https://img-blog.csdn.net/20180708180400642?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

给出一个案列来帮助理解：
![这里写图片描述](https://img-blog.csdn.net/20180707203642565?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

![这里写图片描述](https://img-blog.csdn.net/20180707203653556?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

![这里写图片描述](https://img-blog.csdn.net/20180707203717338?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

![这里写图片描述](https://img-blog.csdn.net/2018070720373061?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

![这里写图片描述](https://img-blog.csdn.net/20180707203740249?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

![这里写图片描述](https://img-blog.csdn.net/20180707203808105?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2FrYWsyMDEwMTEw/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

| UI控件名称 | 使用的材质球                         | 使用的纹理                            |
| ---------- | ------------------------------------ | ------------------------------------- |
| I1         | M_InstID_Bigger                      | texture_InstID_Smaller                |
| I2         | M_InstID_Smaller                     | texture_InstID_Smaller                |
| R1         | M_InstID_Bigger                      | texture_InstID_Bigger                 |
| T1         | UI Default Matiaral(UGUI 默认材质球) | Font Texture(unity自带的一个字体纹理) |

步骤1：先算各个UI控件的depth值

| UI控件名称 | depth值 |
| ---------- | ------- |
| I1         | 0       |
| I2         | 0       |
| R1         | 0       |
| T1         | 1       |

步骤二：排序
1、**按depth值做升序排序。**得 I1、I2、R1、T1
2、**材质球ID排序。**因为 I1、I2、R1的depth值相等，那么再对他们进行材质球ID进行升序排序，得：
**I2.materialID < I1.materialID = R1.materialID**
所以经过材质球排序后：I2、I1、R1、T1
3、**纹理ID排序。\**因为I1和R1的材质球ID相同，故需要进行\**纹理ID降序**排序，得 R1.TexutureID > I1.TextureID
所以经过纹理排序后：I2、R1、I1、T1

至此，就把所有的UI控件都排好序了，得到了所谓的 VisibleList = {I2,R1,I1,T1}
用表格来表示：

| UI控件名称 | depth值 |
| ---------- | ------- |
| I2         | 0       |
| R1         | 0       |
| I1         | 0       |
| T1         | 1       |

步骤三：**合批。**对depth相等的连续相邻UI控件进行合批（注意只有depth相等的才考虑合批，如果depth不相等，即使符合合批条件，也不能合批）。很显然，I2，I1，R1虽然他们depth相等，但不符合合批条件，所以都不能合批。

步骤四：**计算drawcall。**合批步骤完成后就可以计算drawcall数了，即drawcall count = 合批1 + 合批2 + … + 合批n = n * 1 = n (其中 合批i = 1）
所以这个案例的 drawcall count = 1 + 1 + 1 + 1 = 4。渲染顺序（就是我们最终排好序的顺序）是：先画 I2，其次 R1，其次 I1，最后T1。只要打开 Window > Frame Debugger 窗口就可以轻松验证，这里就不贴图了。

最后，希望想搞明白点的能动动手，自己建一个空工程，摆弄一些案例，利用本文的知识来自己算算drawcall数及推出UGUI的渲染顺序。最后经过反复的练习能够总结出一些用较少的drawcall来拼UI的规律。

下一篇，将探究臭名昭著的mask，看看它是不是真的那么不堪，还是我们了解的还不够！

虽然 Mask 的名声不是很好，但是在做界面的时候很多时候都需要它，比如滚动列表。要想精算出一个UI的drawcall数和渲染顺序，光掌握[上一篇](https://blog.csdn.net/akak2010110/article/details/80953370)的知识或许还有些吃力，因为mask在项目里真的太常见了。也往往是UI的drawcall数的一个重灾区。本文不说Mask和RectMask2D是如何实现的，仅仅设立一些案例来总结出两者的性质，希望能给我们做UI时一些实用的指导性作用。也希望看完了本文之后，能了解这两者的区别。
希望看到这篇文章的朋友确定已经掌握了上一篇的知识。

# 一、RectMask2D

RectMask2D不需要依赖一个Image组件，其裁剪区域就是它的RectTransform的rect大小。

## 性质1：RectMask2D节点下的所有孩子都不能与外界UI节点合批且多个RectMask2D之间不能合批。

## 性质2：计算depth的时候，所有的RectMask2D都按一般UI节点看待，只是它没有CanvasRenderer组件，不能看做任何UI控件的bottomUI。

# 二、Mask

Mask组件需要依赖一个Image组件，裁剪区域就是Image的大小。

## 性质1：Mask会在首尾（首=Mask节点，尾=Mask节点下的孩子遍历完后）多出两个drawcall，多个Mask间如果符合合批条件这两个drawcall可以对应合批（mask1 的首 和 mask2 的首合；mask1 的尾 和 mask2 的尾合。首尾不能合）

## 性质2：计算depth的时候，当遍历到一个Mask的首，把它当做一个不可合批的UI节点看待，但注意可以作为其孩子UI节点的bottomUI。

## 性质3：Mask内的UI节点和非Mask外的UI节点不能合批，但多个Mask内的UI节点间如果符合合批条件，可以合批。

从Mask的性质3可以看出，并不是Mask越多越不好，因为Mask间是可以合批的。得出以下结论：
**当一个界面只有一个mask，那么，RectMask2D 优于 Mask**
**当有两个mask，那么，两者差不多。**
**当 大于两个mask，那么，Mask 优于 RectMask2D。**

# 三、drawcall数和渲染顺序

利用本文的性质，再利用上一篇的知识，就可以精算出一个带有mask的UI的drawcall数和渲染顺序。多利用 Frame Debugger 窗口多多练习，熟能生巧！！