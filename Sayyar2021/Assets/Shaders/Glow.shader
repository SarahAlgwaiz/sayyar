Shader "Custom/Glow"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _ColorTint("Color Tint", Color) = (1,1,1,1)
        _BumpMap("Normal Map",2D) = "bump" {}
        _Size ("Atmosphere Size Multiplier", Range(0,16)) = 4
        _RimColor("Rim Color",Color) = (1,1,1,1)
        _RimPower("Rim Strength", Range(0,8)) = 4
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert

        // Use shader model 3.0 target, to get nicer looking lighting

        struct Input
        {
            float2 uv_MainTex;
            float4 color: Color;
            float2 uv_BumpMap;
            float3 viewDir;
        };

        sampler2D _MainTex;
        float4 _ColorTint;
        sampler2D _BumpMap;
        float4 _RimColor;
        float _RimPower;

        void surf (Input IN, inout SurfaceOutput o)
        {
            IN.color = _ColorTint;
            o.Albedo = tex2D(_MainTex,IN.uv_MainTex).rgb * IN.color;
            o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
            half rim = 1.0 - saturate(dot(normalize(IN.viewDir),o.Normal));
            o.Emission = _RimColor.rgb * pow(rim,_RimPower);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
