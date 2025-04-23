Shader "Custom/SurfaceShader"
{
    Properties
    {
        _myColor ("Color", Color) = (1,1,1,1)
        _myEmission ("Emission", Color) = (1, 1, 1, 1)
        _myNormal ("Normal", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        CGPROGRAM
        //Surface Shader with Lambert lighting
        #pragma surface surf Standard Lambert

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _myColor;
        fixed4 _myEmission;
        fixed4 _myNormal;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a color defined in properties
            o.Albedo = _myColor.rgb;
            o.Emission = _myEmission.rgb;
            o.Normal = _myNormal.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
