Shader "Roberts/MOC_ForArtist" {
	Properties{
		[Header(Celshade Admount)]
		_Cel("CelShade", Range(0,1)) = 0
		_CelGray("MidTone", Range(0,1)) = 0
		[Header(Basic Color)]
		_MainColor("Color", Color) = (0.5,0.5,0.5,0)
	
		_MainTex("Texture", 2D) = "white" {}
		_Amb("Ambient", 2D) = "white" {}

		_BumpMap("Bumpmap", 2D) = "bump" {}
		
		[Header(Specular)]
		_SpColor("SpcularColor", Color) = (0,0,0,0)
		_Specular("SpecSpot", Range(1,100)) = 90

		[Header(Rim Basic)]
		_RimColor("Rim Color", Color) = (0,0,0,0.0)
		_RimPower("Rim Power", Range(0.5,8.0)) = 3.0

		[Header(Reflection)]
		_ReflectColor("ReflectColor", Color) = (0,0,0,0)
		_RimReflPower("RimRelfectPower", Range(0.5,8.0)) = 3.0
		_ReflectPure("PureReflection", Range(0,1)) = 0
		_Cube("Cubemap", CUBE) = "Black" {}

		[Header(HightMap)]
		_HColor("HColor", Color) = (1,1,1,0)
		_HTex("HTexture", 2D) = "white" {}
		_HmapA("HAdmount", Range(0.0,1.0)) = 0.0
		_MinH("MinHight", Range(-10.0,10.0)) = 0.0
		_Hscale("Hscale", Range(0.0,1.0)) = 0.1
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf Robert// noambient

			float _Cel;
			half4 _SpColor;
			float _Specular;
			float _CelGray;

			half4 LightingRobert(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {


				half3 h = normalize(lightDir + viewDir);

				half diff = max(0, dot(s.Normal, lightDir));

				float nh = max(0, dot(s.Normal, h));
				float spec = pow(nh, _Specular);
				spec = round(spec);

				float ramp = diff;
				float rampOne = diff;
				float rampTwo = diff;

				ramp = smoothstep(0.0, 0.1, diff);
				rampTwo = smoothstep(_CelGray, 1, diff);

				ramp += rampTwo;
				ramp *= 0.5;

				diff = lerp(ramp, diff, _Cel);
				half4 c;
				c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec * _SpColor) * atten;
				c.a = s.Alpha;
				return c;
			}


			struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
				half3 Emission;

				//ForCubeMap
				float _ReflectPure;
				float3 worldRefl;
				INTERNAL_DATA

				//For Rim
				float3 viewDir;
				float3 worldPos;
			};

			half4 _MainColor;
			sampler2D _Amb;
			sampler2D _MainTex;
			sampler2D _BumpMap;
			samplerCUBE _Cube;

			float4 _RimColor;
			float _RimPower;

			float4 _ReflectColor;
			float _RimReflPower;
			float _ReflectPure;


			float _HmapA;
			float _Hscale;
			float _MinH;
			half4 _HColor;
			sampler2D _HTex;

			void surf(Input IN, inout SurfaceOutput o) {

				//HightMap
				half hMap = (IN.worldPos.y);
				hMap -= _MinH;
				hMap *= _Hscale;
				hMap = lerp(1, hMap, _HmapA);
				hMap = clamp(hMap, 0, 1);

				o.Albedo = lerp(tex2D(_HTex, IN.uv_MainTex).rgb * _HColor, tex2D(_MainTex, IN.uv_MainTex).rgb * _MainColor.rgb, hMap) * tex2D(_Amb, IN.uv_MainTex).rgb;
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

				half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));

				half3 rimColor = _RimColor.rgb * pow(rim, _RimPower);
				half3 reflectRim = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb * pow(rim, _RimReflPower) * _ReflectColor;
				half3 pureReflection = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb * _ReflectPure * _ReflectColor;
				o.Emission = rimColor + reflectRim + pureReflection;
			}
		ENDCG
	}
		Fallback "Diffuse"
}