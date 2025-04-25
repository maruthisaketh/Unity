Shader "Custom/DiffuseTextureShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SecondTex ("Texture", 2D) = "Blue" {}
    }
    SubShader
    {
        CGPROGRAM
        
        #pragma surface surf Lambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _SecondTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_SecondTex;
        };
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Emission = tex2D (_SecondTex, IN.uv_SecondTex);
            o.Alpha = c.a;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
