Shader "Roberts/WorldUV" {

	//THX. Unityforum @Setsuki (world scale normal)
	//THX. Keijiro Takahashi (toggle)
	Properties
	{
		[Toggle(UseWorldUV)]
		_UseWorldUV("WorldUV", Float) = 0

		[Header(UV scale)]
		_Scale("UV scale", Range(0.01,1)) = 0.2

		[Header(Basic Color)]
		_MainColor("Color", Color) = (1,1,1,0)
		_MainTex("Texture", 2D) = "white" {}
		_BumpMap("Normalmap", 2D) = "bump" {}

		[Header(Specular)]
		_SpecularColor("SpcularColor", Color) = (0,0,0,0)
		_Specular("Specular Size", Range(1,100)) = 90

		[Header(Basic Color)]
		_Emission("Lumination", 2D) = "black"{}
		[Header(ZFade)]
		_ZfadeColor("HColor", Color) = (0.376,0.322,0.251,0)
		_ZfadeTexture("HTexture", 2D) = "white" {}
		_ZfadeAdmount("HAdmount", Range(0.0,1.0)) = 1.0
		_ZfadeStart("MinHight", Range(-10.0,10.0)) = 0.0
		_ZfadeScale("Hscale", Range(0.0,1.0)) = 0.1
	}

		SubShader
		{
				Tags { "RenderType" = "Opaque" }
				LOD 400

				CGPROGRAM
				#pragma surface surf Robert  //noambient
			  #pragma shader_feature UseWorldUV




				half _Scale;

				fixed4 _MainColor;
				sampler2D _MainTex;
				sampler2D _BumpMap;

				sampler2D _Emission;

				float _ZfadeAdmount;
				float _ZfadeScale;
				float _ZfadeStart;
				half4 _ZfadeColor;
				sampler2D _ZfadeTexture;

				//Useing data
				struct Input
				{
//					float4 color : COLOR;
					float2 uv_MainTex;
					float3 worldPos;
					float3 worldNormal;
					INTERNAL_DATA
				};

				//Surface Color
				void surf(Input IN, inout SurfaceOutput o)
				{
					float2 uv = IN.uv_MainTex;

#ifdef UseWorldUV
					//Worldscale Normalmap
					float3 correctWorldNormal = WorldNormalVector(IN, float3(0, 0, 1));
					uv = IN.worldPos.zx;
					uv.x = IN.worldPos.x;
					uv.y = IN.worldPos.z;

					if (abs(correctWorldNormal.x) > 0.5) {
						uv.x = -IN.worldPos.z;
						uv.y = -IN.worldPos.y;
						if ((correctWorldNormal.x) < 0.0) {
							uv.x = IN.worldPos.z;
							uv.y = -IN.worldPos.y;
						}
					}
					if (abs(correctWorldNormal.z) > 0.5) {
						uv.x = IN.worldPos.x;
						uv.y = -IN.worldPos.y;
						if ((correctWorldNormal.z) < 0.0) {
							uv.x = IN.worldPos.x;
							uv.y = IN.worldPos.y;
						}
					}

					uv.x *= -_Scale;
					uv.y *= -_Scale;

#else
					uv = IN.uv_MainTex;
#endif

					//HightMap
					half hMap = (IN.worldPos.y);
					hMap -= _ZfadeStart;
					hMap *= _ZfadeScale;
					hMap = lerp(1, hMap, _ZfadeAdmount);
					hMap = clamp(hMap, 0, 1);

					fixed4 tex = tex2D(_MainTex, uv) * _MainColor;
					fixed4 texTwo = tex2D(_ZfadeTexture, uv) * _ZfadeColor;
					o.Albedo = lerp(texTwo.rgb, tex.rgb, hMap);
					o.Normal = UnpackNormal(tex2D(_BumpMap, uv));
					o.Emission = tex2D(_Emission, uv);
				}

				half4 _SpecularColor;
				float _Specular;

				//Lightning
				half4 LightingRobert(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {

					//Light
					half NdotL = dot(s.Normal, lightDir);
					half diff = NdotL * 0.5 + 0.5;


					half3 h = normalize(lightDir + viewDir);
					float nh = max(0, dot(s.Normal, h));
					float spec = pow(nh, _Specular);
					spec = round(spec) * atten;

					//Ramp
					float ramp = smoothstep(0.5, 0.7, diff);
					float rampTwo = smoothstep(0.8, 0.85, diff);
					ramp += rampTwo;
					ramp *= 0.5;

					float fakeAtten = smoothstep(0, 1, atten);
					ramp *= fakeAtten;

					//Set light color
					half4 c;
					c.rgb = (s.Albedo * _LightColor0.rgb * ramp + _LightColor0.rgb * spec * _SpecularColor);
					c.a = s.Alpha;
					return c;
				}
				ENDCG
		}
			FallBack "Bumped Specular"
}