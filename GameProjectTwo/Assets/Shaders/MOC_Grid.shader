Shader "Roberts/Grid" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
		_Specular("Specular", Range(0,100)) = 90
		_SpecularStr("SStrength", Range(0,1)) = 1
		_Cel("Cel", Range(0,1)) = 0
			_Scale("Scale", Range(0,10)) = 1
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
				float rampT = diff;

				ramp = smoothstep(0.0, 0.2, ramp);
				rampT = smoothstep(0.5, 1.0, rampT);
				//ramp *= 0.75;  //<-Note incorect

				ramp = lerp(ramp, rampT, 0.5);
				diff = lerp(ramp, diff, _Cel);
				half4 c;
				c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
				//c.rgb = ramp;
				c.a = s.Alpha;
				return c;
			}


			struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
				float _Specular;
				float _SpecularStr;
				float _Cel;
				float3 worldPos;
				float3 worldNormal;
				float _Scale;
			};
			sampler2D _MainTex;
			sampler2D _BumpMap;
			float _Scale;

			void surf(Input IN, inout SurfaceOutput o) {
				//https://gamedev.stackexchange.com/questions/136652/uv-world-mapping-in-shader-with-unity
				float2 uv;
				fixed4 c;

				if (abs(IN.worldNormal.x) > 0.5)
				{
					uv = IN.worldPos.yz; // side
					c = tex2D(_MainTex, uv * _Scale); // use WALLSIDE texture
				}
				else if (abs(IN.worldNormal.z) > 0.5)
				{
					uv = IN.worldPos.xy; // front
					c = tex2D(_MainTex, uv * _Scale); // use WALL texture
				}
				else
				{
					uv = IN.worldPos.xz; // top
					c = tex2D(_MainTex, uv * _Scale); // use FLR texture
				}

				float hFog = 4 + IN.worldPos.y;
				hFog *=0.1;
				o.Albedo = c.rgb * hFog;
			}
			ENDCG
		}
			Fallback "Diffuse"
}