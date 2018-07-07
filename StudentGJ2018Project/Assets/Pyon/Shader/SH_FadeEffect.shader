Shader "CustomPostEffect/SH_FadeEffect"
{
	Properties
	{
		_MainTex("MainTex", 2D) = ""{}
		_Color("Color", Color) = (1, 1, 1, 1)
		_Threshold("Threshold", Range(0, 1)) = 0
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert_img
			#pragma fragment frag

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			fixed4 _Color;
			float _Threshold;

			struct Input {
				float2 uv_MainTex;
			};

			fixed4 frag(v2f_img i) : COLOR
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				
				//_Thresholdが0のとき色が被らないようにする
				_Threshold = 1.0f - _Threshold;

				//_Thresholdだけ色をかぶせる
				color *= _Threshold;

				//かぶせる色を_Colorに変更する
				color += _Color * (1.0f - _Threshold);
				
				return color;
			}
			ENDCG
		}
	}
}
