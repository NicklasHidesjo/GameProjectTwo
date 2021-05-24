Shader "Roberts/FogFX"
{
	Properties
	{

		 _Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
	_Str("Strength", Float) = 0.5
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" }
 Blend SrcAlpha One
	ZWrite Off
		LOD 10

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Str;

			fixed4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{

				float2 invUV = i.uv;

				// sample the texture
				float2 uv = i.uv;
				float2 uv2 = i.uv * 0.5;
				uv.x += sin(_Time * 16) * 0.02;
				uv.y += _Time * 2;

				uv2.x += sin(_Time * 16 + 0.5) * 0.02 + 0.3;
				uv2.y += _Time * 2;


				fixed4 col = (i.uv.y / 4);
				col += tex2D(_MainTex, uv) * _Str;
				col += tex2D(_MainTex, uv2) * _Str;
				col *= (i.uv.y / 4);
				col *= _Color;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
