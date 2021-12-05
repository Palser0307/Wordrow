Shader "Unlit/UI_Back"
{
    Properties
    {
        _sColor("StripeColor",color)=(0.05,0.05,0.05,1)//202,202,202
        _backColor("Background Color",color)=(0,0,0,1)//188,188,188
        _sWidth("Width of Stripe",Range(0,0.2)) = 0.0467
        _rotAngle("Stripe Angle",Range(0,3.14159265)) = 2.44
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
        "Queue"="Transparent" }
        LOD 100
        Blend srcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #include "UnityCG.cginc"
            struct appdata
            {
                float4 vertex : POSITION;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            fixed4 _backColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                // back color
                return _backColor;
            }
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #include "UnityCG.cginc"
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
            fixed4 _sColor;
            fixed _sWidth;
            float _rotAngle;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;
                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                //float2x2 rot=(cos(_rotAngle),-sin(_rotAngle),sin(_rotAngle),cos(_rotAngle));
                float2 p = i.uv*2.-1.;
                p.x=p.x*cos(_rotAngle)+p.y*sin(_rotAngle);
                p.y=p.x*-sin(_rotAngle)+p.y*cos(_rotAngle);

                //p = mul(rot,p.xy);
                //normalize coordinate
                float w=fmod(p.x+1+2*_sWidth,_sWidth);
                //stripe width
                w=step(_sWidth/2.,w);

                //waku
                float waku=step(0.9,abs(p.x));

                return float4(_sColor.x,_sColor.y,_sColor.z,w);
            }
            ENDCG
        }
            Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #include "UnityCG.cginc"
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;
                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                //float2x2 rot=(cos(_rotAngle),-sin(_rotAngle),sin(_rotAngle),cos(_rotAngle));
                float2 p = i.uv*2.-1.;
                //waku
                float waku=step(0.95,abs(p.x));
                waku+=step(0.95,abs(p.y))-waku*step(0.95,abs(p.y));

                waku*=sin(p.y+_Time.z);
                return float4(0.3,0.3,0.3,waku);
            }
            ENDCG
        }
        
    }
}