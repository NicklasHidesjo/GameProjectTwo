Shader "WorldUV/Bumped Diffuse" {

	//THX. Unityforum @Setsuki
	Properties
	{
		[Header(Celshade Admount)]
		_ExtCutOff("ExtremeCutOff", Range(0,0.2)) = 0.15

		[Header(Basic Color)]
		_Color("Color", Color) = (0.5,0.5,0.5,0)

		[Header(Specular)]
		_SpColor("SpcularColor", Color) = (0,0,0,0)
		_Specular("SpecSpot", Range(1,100)) = 90

		[Header(Color)]
		_MainTex("Base", 2D) = "white" {}
		_BumpMap("Normalmap", 2D) = "bump" {}
		_Scale("Scale", Range(0.01,1)) = 0.25

		[Header(HightMap)]
		_HColor("HColor", Color) = (1,1,1,0)
		_HTex("HTexture", 2D) = "white" {}
		_HmapA("HAdmount", Range(0.0,1.0)) = 0.0
		_MinH("MinHight", Range(-10.0,10.0)) = 0.0
		_Hscale("Hscale", Range(0.0,1.0)) = 0.1
	}

		SubShader
		{
				Tags { "RenderType" = "Opaque" }
				LOD 400

				CGPROGRAM
				#pragma surface surf Robert  


				struct Input
				{
					float4 color : COLOR;
					float2 uv_MainTex;
					float2 uv_BumpMap;
					float3 worldPos;
					float3 worldNormal;
					INTERNAL_DATA
				};

				sampler2D _MainTex;
				sampler2D _BumpMap;
				fixed4 _Color;

				half _Scale;


				float _HmapA;
				float _Hscale;
				float _MinH;
				half4 _HColor;
				sampler2D _HTex;

				void surf(Input IN, inout SurfaceOutput o)
				{
					//Worldscale Normalmap
					float3 correctWorldNormal = WorldNormalVector(IN, float3(0, 0, 1));
					float2 uv = IN.worldPos.zx;
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


					//HightMap
					half hMap = (IN.worldPos.y);
					hMap -= _MinH;
					hMap *= _Hscale;
					hMap = lerp(1, hMap, _HmapA);
					hMap = clamp(hMap, 0, 1);

					fixed4 tex = tex2D(_MainTex, uv) * _Color;
					fixed4 texTwo = tex2D(_HTex, uv) * _HColor;
					o.Albedo = IN.color * lerp(texTwo.rgb, tex.rgb, hMap);
					o.Normal = UnpackNormal(tex2D(_BumpMap, uv));
				}


				half4 _SpColor;
				float _Specular;
				float _ExtCutOff;


				half4 LightingRobert(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {

					//Light
					half NdotL = dot(s.Normal, lightDir);
					half diff = NdotL * 0.5 + 0.5;


					half3 h = normalize(lightDir + viewDir);
					float nh = max(0, dot(s.Normal, h));
					float spec = pow(nh, _Specular);
					spec = round(spec) * atten;

					//Ramp
					diff -= _ExtCutOff;
					diff *= atten;
					float ramp = diff;
					ramp = round(ramp * 2);
					ramp *= 0.5;

					//set light
					half4 c;
					c.rgb = (s.Albedo * _LightColor0.rgb * ramp + _LightColor0.rgb * spec * _SpColor);
					c.a = s.Alpha;
					return c;
				}
				ENDCG
		}
			FallBack "Bumped Specular"
}