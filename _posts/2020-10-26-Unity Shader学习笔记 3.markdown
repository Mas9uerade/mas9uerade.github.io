---
layout:     post
title:      "Unity3d Shader 学习笔记 3"
subtitle:   " \"Unity Shader 入门笔记\""
date:       2020-10-26 22:34:00
author:     "Mas9uerade"
header-img: "img/UnityShader3/title_holography_s1.png"
tags:
    - Unity3d
    - Shader	
---

>  最近这段时间在找工作，顺便把之前项目里做的一些Shader重新看一遍

### 半透明材质

![holography](https://mas9uerade.github.io/img/in-post/UnityShader3/holography.gif)

思路：

就是在uv的贴图上，额外增加Alpha通道的权重处理，再放在全黑的环境下显示

```C
Shader "Custom/Holography"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Color", Color) = (1.0000, 1.0000, 1.0000, 1.0000)
	}
		SubShader
		{
			Tags
			{
				"RenderType" = "Transparent"
			}


			ZWrite Off
			Cull Off
			Blend One One

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
	

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				struct appdata
				{
					float4 vertex   : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					half2 uv  : TEXCOORD0;
				};

				sampler2D _MainTex;
				uniform float4 _MainTex_ST;
				uniform float4 _Color;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv) * _Color;
					col.rgb *= col.a * _Color.a;
					return col;
				}
				ENDCG
			}
		}
}
```
