Shader "Custom/StencilMask"
{
    Properties
    {
        //Set de Stencil Reference for the Mask
        _StencilMask("Stencil mask", Int) = 0
    }
    SubShader
    {
        Tags {
			"RenderType" = "Opaque"
			"Queue" = "Geometry-1"
		}

        ColorMask 0
        ZWrite off
        
        Stencil{
            Ref[_StencilMask]
            Comp Always
            Pass replace
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return half4(0,0,0,0);
            }
            ENDCG
        }
    }
}
