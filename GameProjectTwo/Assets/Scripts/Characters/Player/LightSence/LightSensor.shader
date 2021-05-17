Shader "Roberts/LightSensor" {
	Properties{
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf Lambert fullforwardshadows noambient

			


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