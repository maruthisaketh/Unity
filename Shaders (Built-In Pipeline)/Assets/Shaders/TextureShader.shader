Shader "Custom/TextureShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "blue" {}
        _Range ("Range", Range(0, 5)) = 2.0
        _Cube ("Cube", CUBE) = "" {}
    }
    SubShader
    {

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert

        sampler2D _MainTex;
        samplerCUBE _Cube;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldRefl;
        };

        fixed4 _Color;
        half _Range;

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color * _Range;
            o.Albedo.rbg = c.rgb;
            //o.Emission = texCUBE (_Cube, IN.worldRefl).rgb * _Range;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
