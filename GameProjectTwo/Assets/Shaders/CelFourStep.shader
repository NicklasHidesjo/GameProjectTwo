Shader "Roberts/CelShader4step" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
		_Specular("Specular", Range(0,100)) = 90
		_SpecularStr("SStrength", Range(0,1)) = 1
		_Cel("Cel", Range(0,1)) = 0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf Robert// noambient

			float _Specular;
			float _Cel;
			float _SpecularStr;

			half4 LightingRobert(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {


				half3 h = normalize(lightDir + viewDir);

				half diff = max(0, dot(s.Normal, lightDir));

				float nh = max(0, dot(s.Normal, h));
				float spec = pow(nh, _Specular);
				spec = round(spec);
				spec *= _SpecularStr;

				float ramp = diff;
				float rampOne = diff;
				float rampTwo = diff;
				float rampTree = diff;

				rampOne = smoothstep(0.2, 0.3, ramp);
				rampTwo = smoothstep(0.45, 0.55, ramp);
				rampTree = smoothstep(0.7, 0.8, ramp);
				ramp = smoothstep(0.0, 0.1, ramp);

				ramp += rampOne + rampTwo + rampTree;
				ramp *= 0.25f;

				diff = lerp(ramp, diff, _Cel);
				half4 c;
				c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
				c.a = s.Alpha;
				return c;
			}


			struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
				float _Specular;
				float _SpecularStr;
				float _Cel;
			};
			sampler2D _MainTex;
			sampler2D _BumpMap;
			void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			}
		ENDCG
		}
			Fallback "Diffuse"
}