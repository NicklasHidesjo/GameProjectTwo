Shader "Roberts/LightSensor" {
	Properties{
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf Robert noambient fullforwardshadows

			

			half4 LightingRobert(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {


				half3 h = normalize(lightDir + viewDir);

				half diff = max(0, dot(s.Normal, lightDir));

				float ramp = diff;
				float rampT = diff;

				ramp = smoothstep(0.0, 0.2, ramp);
				rampT = smoothstep(0.5, 1.0, rampT);

				ramp = lerp(ramp, rampT, 0.5);
				half4 c;
				c.rgb = (s.Albedo * _LightColor0.rgb * diff) * atten;
				c.a = s.Alpha;
				return c;
			}


			struct Input {
				float2 uv_MainTex;
			};
			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = 1;// tex2D(_MainTex, IN.uv_MainTex).rgb;
			}
		ENDCG
		}
			Fallback "Diffuse"
}