Shader "Unlit/HUD"
{
    Properties
    {
        //ナニコレ
        _CircleSize ("CircleSize", Range(0,0.1)) = 0.02
        //真ん中のサイズ
        _CenterCircleSize("CenterCircleSize",Range(0,0.3))=0.05
        //ラインのサイズ
        _LineSize("LineSize",Range(0,0.05)) =0.01
        //外側のサークルの位置
        _OutCirclePosition("Position of OutCircle",Range(0,1))=0.9
        //外側のサークルのサイズ
        _OutCircleSize("OutCicleSize",Range(0,1))=0.1
        //サブサークルの生成可否
        [MaterialToggle] _SubCircleGenerate("Generate SubCircle",Float)=1
        //サブサークルの最大位置
        _SubCirclePosition("Max Position of SubCircle",Range(0,1))=0.9
        //サブサークルの量
        _SubCircleAmount("Amount of SubCircle",Range(0,20))=3
        //サブサークルの間隔
        _SubCircleSpace("Space of SubCircle",Range(0,0.5))=0.05
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
                "Queue" = "Transparent" }
        LOD 100
        Cull off
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _CircleSize;
            float _LineSize;
            float _CenterCircleSize;
            float _OutCircleSize;
            float _OutCirclePosition;

            int _SubCircleGenerate;
            int _SubCircleAmount;
            float _SubCirclePosition;
            float _SubCircleSpace;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 p = i.uv*2.-1.;

                //1でdiscardすればQuad感がなくなる
                if(length(p) > 1) discard;

                //中央の点
                float c =1- step(_CenterCircleSize,length(p));

                //OutCircleの位置と大きさ
                c+=(1-step(_OutCirclePosition-0.05,length(p)))-(1-step(_OutCirclePosition-_OutCircleSize,length(p)));

                //SubCircle
                for(int i=0;i<_SubCircleAmount*_SubCircleGenerate;i++){
                    c+=(1-step(_SubCirclePosition-i*_SubCircleSpace,length(p)))-(1-step(_SubCirclePosition-0.005-i*_SubCircleSpace,length(p)));
                }
                //line
                //y line
                c +=1.- step(_LineSize,abs(p.x)); 
                //x line
                c+=1. -step(_LineSize,abs(p.y));


                
                float4 col = float4(0,c,0,c);

                return col;
            }
            ENDCG
        }
    }
}
