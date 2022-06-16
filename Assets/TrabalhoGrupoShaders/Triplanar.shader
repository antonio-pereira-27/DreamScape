Shader "Unlit/Triplanar"
{
    Properties
    {
        _TopTex ("Moss", 2D) = "white" {}
        _BackTex ("Pedra", 2D) = "white" {}
        _NoiseTex ("Noise", 2D) = "white" {}
        _MossRange ("Moss Range", Range(0,10)) = 1
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

           struct appdata
            {
                float4 pos : POSITION;
                float4 normal : NORMAL;

            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 wPos : TEXCOORD0;
                float3 normalWorld : TEXCOORD1;

                float4 color : COLOR;
            };

            sampler2D _TopTex;
            float4 _TopTex_ST;

            sampler2D _BackTex;
            float4 _BackTex_ST;

            fixed _MossRange;

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.pos);
                o.wPos = mul(unity_ObjectToWorld, v.pos);
                o.normalWorld = UnityObjectToWorldNormal(v.normal);

               float lambertEfect = max(0, dot(o.normalWorld  , _WorldSpaceLightPos0.xyz));

                o.color = (lambertEfect+0.2) * _LightColor0;
              
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
             
                float3 triplanarUV = i.wPos;
                float3 normalWorldRep = abs(i.normalWorld);

                float4 x = tex2D(_BackTex, triplanarUV.yz);
                float4 y = tex2D(_BackTex, triplanarUV.xz);
                float4 z = tex2D(_BackTex, triplanarUV.xy);
           
                float4 pedraTex = x * normalWorldRep.x + y * normalWorldRep.y + z * normalWorldRep.z;
              
                
                float4 xTop = tex2D(_TopTex, triplanarUV.yz);
                float4 yTop = tex2D(_TopTex, triplanarUV.xz);
                float4 zTop = tex2D(_TopTex, triplanarUV.xy);

                float4 ervaTex =  xTop * normalWorldRep.x + yTop * normalWorldRep.y + zTop * normalWorldRep.z;

                float4 xNoise = tex2D(_NoiseTex, triplanarUV.yz);
                float4 yNoise = tex2D(_NoiseTex, triplanarUV.xz);
                float4 zNoise = tex2D(_NoiseTex, triplanarUV.xy);

                float4 noiseTex =  xNoise * normalWorldRep.x + yNoise * normalWorldRep.y + zNoise * normalWorldRep.z;

                float4 col = step(_MossRange * noiseTex.x, i.normalWorld.y) * ervaTex + step(i.normalWorld.y, _MossRange * noiseTex.x) * pedraTex;
           

                return col * i.color;
            }
            ENDCG
        }
    }
}
