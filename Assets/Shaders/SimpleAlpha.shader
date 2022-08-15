Shader "Transparent/SimpleAlpha"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Filter("Filter", 2D) = "black" {}
        _AlphaExtend("Alpha Extend", Range(0, 1)) = 0.9
    }
    SubShader
    {
        Tags { 
            "Queue" = "Transparent" //ѡ����Ⱦ����ΪTransparent������������Ⱦ��͸������֮������Ⱦ������
            "IgnoreProjector" = "True" //������ӰͶ����Ӱ��
            "RenderType" = "Transparent"//����shader����Ԥ�����õ��࣬��������shader�滻
        }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha //������ͨ���,��ʵ�ְ�͸��Ч��
        //https://blog.csdn.net/qq_21315789/article/details/126105298
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_f : TEXCOORD1;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_f : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _Filter;
            float4 _MainTex_ST;
            float4 _Filter_ST;
            fixed _AlphaExtend;

            v2f vert (appdata v) 
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv_f = TRANSFORM_TEX(v.uv_f, _Filter);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return fixed4(col.rgb,1- (tex2D(_Filter, i.uv_f).g)*_AlphaExtend);
            }
            ENDCG
        }
    }
}
