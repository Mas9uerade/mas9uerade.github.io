---
layout:     post
title:      "Python解析Xml文件"
subtitle:   " \"Unity Shader\""
date:       2018-05-02 15:48:00
author:     "Mas9uerade"
header-img: "img/watchdog2_sf.jpg"
tags:
    - Python
    - xml
---

> “简单的笔记”

最近在工程中需要去裁剪一些图集，所以写了一个根据xml数据裁剪图集的小脚本。

## 基本思路
1. 使用Python的PIL库打开图集；
2. 解析Xml配置表获取图片的像素位置与大小，调用Crop()进行裁剪；

### 附上脚本

```Python
from PIL import Image
import os
from xml.dom.minidom import parse
import xml.dom.minidom as xmldom

#图集路径与配置表路径
imgPath = "./TroopCardAll.png"
xmlPath = "./TroopCardAll.xml"
# 使用PIL打开图像
img = Image.open(imgPath)
# 使用minidom解析器打开 XML 文档
DOMTree = xmldom.parse(xmlPath)
# 获取子节点
collection = DOMTree.documentElement
print("DocumentElement:", collection)
# 子节点的签名为 SubTexture
subTextures = collection.getElementsByTagName("SubTexture")
print("getElementsByTagName:", type(subTextures))
print(len(subTextures))
# 遍历子节点并切图
for subTexture in subTextures:
    name = subTexture.getAttribute("name")
    x = int(subTexture.getAttribute("x"))
    y = int(subTexture.getAttribute("y"))
    w = int(subTexture.getAttribute("width"))
    h = int(subTexture.getAttribute("height"))
    print("name:", name, "x:", x, "y:", y, "w:", w, "h:", h)
    subImg = img.crop((x, y, x+w, y+h))
    filename = ("./TroopCardAll/" + name + ".png")
    print(filename)
    # 确认是否有子路经并存储
    if "/" in name:
        dir = name.rfind("/")
        fileDir = "./TroopCardAll/" + name[0:dir]
        if not (os.path.exists(fileDir)):
            os.makedirs(fileDir)
        print("CreateDir:", fileDir)
    # 存储图片   
    subImg.save(filename)

print("Done")

```