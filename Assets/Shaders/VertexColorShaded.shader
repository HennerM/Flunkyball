Shader "Custom/Vertex Color" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Emission ("Emission", Float) = 0
//		_Gamma ("Gamma", Float) = 2.2
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma vertex vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct v2f {
           float4 pos : SV_POSITION;
           fixed4 color : COLOR;
         };

        struct Input {
			fixed4 color;
		};

		half _Glossiness;
		half _Metallic;
//		half _Gamma;
		fixed4 _Color;
		fixed _Emission;
 
		void vert (inout appdata_full v, out Input o)
		{
		    UNITY_INITIALIZE_OUTPUT(Input,o);
		    // apply sRGB color correction to color values (Gamma 2.2)
			o.color = pow(v.color,half4(1.0/2.2,1.0/2.2,1.0/2.2,1.0));
			o.color += o.color * fixed4(_Emission,_Emission,_Emission,0);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			o.Albedo = _Color * IN.color;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Color.a * IN.color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
