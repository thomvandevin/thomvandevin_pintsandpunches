﻿// RGB sprite split shader test unitycoder.com
Shader "UnityCoder/RGBSplit"
{
Properties
{
[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
//        _Color ("Tint", Color) = (1,1,1,1)
[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
_RedOffsetX ("RedOffsetX", Float) = 0
_RedOffsetY ("RedOffsetY", Float) = 0
_GreenOffsetX ("GreenOffsetX", Float) = 0
_GreenOffsetY ("GreenOffsetY", Float) = 0
_BlueOffsetX ("BlueOffsetX", Float) = 0
_BlueOffsetY ("BlueOffsetY", Float) = 0
}
 
SubShader
{
Tags
{
"Queue"="Transparent"
"IgnoreProjector"="True"
"RenderType"="Transparent"
"PreviewType"="Plane"
"CanUseSpriteAtlas"="True"
}
 
Cull Off
Lighting Off
ZWrite Off
Fog { Mode Off }
//        Blend SrcAlpha OneMinusSrcAlpha
//        Blend SrcAlpha OneMinusSrcAlpha          // Soft Additiv
 
// RED
Pass
{
//Blend One One
Blend SrcAlpha OneMinusSrcAlpha          // Soft Additiv
//        Blend One One                       // Additive
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile DUMMY PIXELSNAP_ON
#include "UnityCG.cginc"
 
struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};
 
struct v2f
{
float4 vertex   : SV_POSITION;
fixed4 color    : COLOR;
half2 texcoord  : TEXCOORD0;
};
 
//            fixed4 _Color;
float _RedOffsetX;
float _RedOffsetY;
 
v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
OUT.texcoord = IN.texcoord;
//                OUT.color = IN.color * _Color;
OUT.color = IN.color * float4(1,0,0,1);
OUT.vertex.xy += float2(_RedOffsetX,_RedOffsetY);
#ifdef PIXELSNAP_ON
OUT.vertex = UnityPixelSnap (OUT.vertex);
#endif
 
return OUT;
}
 
sampler2D _MainTex;
 
fixed4 frag(v2f IN) : COLOR
{
return tex2D(_MainTex, IN.texcoord) * IN.color;
}
ENDCG
}
 
// GREEN
Pass
{
//Blend SrcAlpha OneMinusSrcAlpha          // Soft Additiv
Blend One One                       // Additive
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile DUMMY PIXELSNAP_ON
#include "UnityCG.cginc"
 
struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};
 
struct v2f
{
float4 vertex   : SV_POSITION;
fixed4 color    : COLOR;
half2 texcoord  : TEXCOORD0;
};
 
//fixed4 _Color;
float _GreenOffsetX;
float _GreenOffsetY;
 
v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
OUT.texcoord = IN.texcoord;
//                OUT.color = IN.color * _Color;
OUT.color = IN.color * float4(0,1,0,1);
OUT.vertex.xy += float2(_GreenOffsetX,_GreenOffsetY);
#ifdef PIXELSNAP_ON
OUT.vertex = UnityPixelSnap (OUT.vertex);
#endif
 
return OUT;
}
 
sampler2D _MainTex;
 
fixed4 frag(v2f IN) : COLOR
{
return tex2D(_MainTex, IN.texcoord) * IN.color;
}
ENDCG
} // end pass
 
// BLUE
Pass
{
//Blend SrcAlpha OneMinusSrcAlpha          // Soft Additiv
Blend One One                       // Additive
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile DUMMY PIXELSNAP_ON
#include "UnityCG.cginc"
 
struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};
 
struct v2f
{
float4 vertex   : SV_POSITION;
fixed4 color    : COLOR;
half2 texcoord  : TEXCOORD0;
};
 
//fixed4 _Color;
float _BlueOffsetX;
float _BlueOffsetY;
 
v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
OUT.texcoord = IN.texcoord;
//                OUT.color = IN.color * _Color;
OUT.color = IN.color * float4(0,0,1,1);
OUT.vertex.xy += float2(_BlueOffsetX,_BlueOffsetY);
#ifdef PIXELSNAP_ON
OUT.vertex = UnityPixelSnap (OUT.vertex);
#endif
 
return OUT;
}
 
sampler2D _MainTex;
 
fixed4 frag(v2f IN) : COLOR
{
return tex2D(_MainTex, IN.texcoord) * IN.color;
}
ENDCG
} // end pass
 
}
}