Shader "Unlit/EfeitoSangue"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BloodTex("Texture", 2D) = "white" {}
       
        _Raio("Raio", Range(0, 1)) = 0.012
        _Velocidade("Velocidade", Range(1,25)) = 7.1
        _Ondulacao("Ondulação", Range(1,25)) = 9.1
        Trigger("Trigger", Float) = 1
        
        
    }
        SubShader
        {
            // No culling or depth
            Cull Off ZWrite Off ZTest Always

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
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };
                
                sampler2D _BloodTex;
                sampler2D _MainTex;
                float4 _BloodTex_ST;
                float _Raio;
                float _Velocidade;
                float _Ondulacao;
                float Trigger;

                
                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _BloodTex);
                    return o;
                }              

                fixed4 frag(v2f i) : SV_Target
                {

                    fixed4 col = tex2D(_MainTex, i.uv);
                    
                    float2 newUvs = _Raio * float2(sin(_Time.y * _Velocidade + i.uv.y * _Ondulacao), cos(_Time.x * _Velocidade + i.uv.x * _Ondulacao));

                    
                    if (Trigger == 1) {
                        
                        // guarda o alpha
                        float alpha = tex2D(_BloodTex, i.uv + newUvs).a;

                        // se alpha maior que zero desenha a textura
                        if (alpha > 0) {
                            col = tex2D(_BloodTex, i.uv + newUvs);
                        }
                    }
                    
                    return col;
                }
                ENDCG
            }
        }
}