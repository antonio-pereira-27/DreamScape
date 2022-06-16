Shader "Unlit/Holograma"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ChoosenColor("Choose Color", Color) = (1,1,1,1)
    }
    SubShader
    {
         Pass
        {
            Cull off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"


            float3 rotate3Dx(float3 pos, float angle){
                float3x3 rot = {1, 0, 0,
                                0, cos(angle), -sin(angle),
                                0, sin(angle), cos(angle)};
                return mul(rot, pos);
            }

            float3 rotate3Dy(float3 pos, float angle){
                float3x3 rot = {cos(angle), 0, sin(angle),
                                0, 1, 0,
                                -sin(angle), 0, cos(angle)};
                return mul(rot, pos);
            }

            
            struct appdata
            {
                float4 vertex:POSITION;
                half3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float4 clipPos:SV_POSITION;
                half3 normal : NORMAL;
                half3 viewDir: POSITION1;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _ChoosenColor;

            v2f vert (appdata v)
            {
                v2f o;
                
                float _OutlineSize = 1.3;
                float _Speed = 0.5;
                float _AnguloX = 1;

                o.normal = v.normal;
                o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
                
                v.vertex.xyz =  rotate3Dy(v.vertex.xyz, _AnguloX * _Speed * _Time.y); //+ rotate3Dx(v.vertex.xyz, _AnguloX  * _Speed * _Time.y) ;
                o.clipPos=UnityObjectToClipPos(v.vertex * _OutlineSize);
                o.uv = (v.uv * _MainTex_ST.xy) + _MainTex_ST.zw;

                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _ChoosenColor;
        
                half rim = 1.0 - dot(i.normal, i.viewDir);
                col = pow(col * rim, 3);

                
                
                return col;
            }
            ENDCG
        }
    }
}
