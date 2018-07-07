Shader "Raymarching/Test"
{
	Properties
	{
		_MainColor("Diffuse Color", Color) = (1, 1, 1, 1)
		_EmissiveColor("Emissive Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "defaulttexture" {}
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" "DisableBatching" = "True" "Queue" = "Geometry+10" }
		Cull Off

		CGINCLUDE
		#include "UnityCG.cginc"
		#include "Utils.cginc"
		#include "Primitives.cginc"

		#define PI 3.1415926535

		float quad(float3 pos, float3 a, float3 b, float3 c, float3 d)
		{
			float3 ba = b - a;
			float3 pa = pos - a;
			float3 cb = c - b;
			float3 pb = pos - b;
			float3 dc = d - c;
			float3 pc = pos - c;
			float3 ad = a - d;
			float3 pd = pos - d;
			float3 nor = cross(ba, ad);

			return sqrt(
				(sign(dot(cross(ba, nor), pa)) +
					sign(dot(cross(cb, nor), pb)) +
					sign(dot(cross(dc, nor), pc)) +
					sign(dot(cross(ad, nor), pd)) < 3.0)
				?
				min(min(min(
					dot2(ba * clamp(dot(ba, pa) / dot2(ba), 0.0, 1.0) - pa),
					dot2(cb * clamp(dot(cb, pb) / dot2(cb), 0.0, 1.0) - pb)),
					dot2(dc * clamp(dot(dc, pc) / dot2(dc), 0.0, 1.0) - pc)),
					dot2(ad * clamp(dot(ad, pd) / dot2(ad), 0.0, 1.0) - pd))
				:
				dot(nor, pa) * dot(nor, pa) / dot2(nor));
		}
		
		float sdHeart(float3 p)
		{
			// 回転
			// mat3 m_y = mat3(cos(time),0,-sin(time),0,1,0,sin(time),0,cos(time));
			// p = m_y * p;
			float3x3 m_z = float3x3(cos(3.14),-sin(3.14),0,
				sin(3.14),cos(3.14),0,
				0,0,1);
			//p = m_z * p;

			// return sqrt(p.x*p.x+p.y*p.y+2.25*p.z*p.z+pow(p.x*p.x+0.1125*p.z*p.z, 0.33)*p.y)-1.0;
			// Heart距離関数(lengthあり)
			float3 q = float3(p.x, p.y, 1.5*p.z);
			return sqrt(length(q)*length(q) + pow(p.x*p.x + 0.1125*p.z*p.z, 0.33)*p.y) - 1.0;
		}

		float DistanceFunc(float3 pos)
		{
			//pos.z += abs(sin(_Time.y)) * 5;
			float bar_x = bar(repeat(rotate(pos, PI / 4, float3(0, 1, 0)), 90).yz, 2);
			float bar_y = bar(repeat(rotate(pos, PI / 4, float3(0, 1, 0)), 90).xz, 2);
			float bar_z = bar(repeat(rotate(pos, PI / 4, float3(0, 1, 0)), 90).xy, 2);
			float crossbar = min(min(bar_x, bar_y), bar_z);

			float width = 5;
			float d2 = sdHeart(repeat(rotate(pos, _Time.y * 0.02, float3(0, 0, 1)), 10));
			return d2;
			//return smoothMin(crossbar, d2, 1);
		}

		float3 GetCameraPosition()
		{
			return _WorldSpaceCameraPos;
		}

		float3 GetCameraForward()
		{
			return -UNITY_MATRIX_V[2].xyz;
		}

		float3 GetCameraUp()
		{
			return UNITY_MATRIX_V[1].xyz;
		}

		float3 GetCameraRight()
		{
			return UNITY_MATRIX_V[0].xyz;
		}

		float GetCameraFocalLength()
		{
			return abs(UNITY_MATRIX_P[1][1]);
		}

		float GetCameraMaxDistance()
		{
			return _ProjectionParams.z - _ProjectionParams.y;
		}

		float GetDepth(float3 pos)
		{
			float4 vpPos = mul(UNITY_MATRIX_VP, float4(pos, 1.0));
#if defined(SHADER_TARGET_GLSL)
			return (vpPos.z / vpPos.w) * 0.5 + 0.5;
#else
			return vpPos.z / vpPos.w;
#endif
		}

		float3 GetNormal(float3 pos)
		{
			const float d = 0.001;
			return 0.5 + 0.5 * normalize(float3(
				DistanceFunc(pos + float3(d, 0.0, 0.0)) - DistanceFunc(pos + float3(-d, 0.0, 0.0)),
				DistanceFunc(pos + float3(0.0, d, 0.0)) - DistanceFunc(pos + float3(0.0, -d, 0.0)),
				DistanceFunc(pos + float3(0.0, 0.0, d)) - DistanceFunc(pos + float3(0.0, 0.0, -d))));
		}

		ENDCG

			Pass
		{
			Tags {"LightMode" = "Deferred"}

			Stencil
			{
				Comp Always
				Pass Replace
				Ref 128
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile ___ UNITY_HDR_ON

			#include "UnityCG.cginc"

			float4 _MainColor;
			float4 _EmissiveColor;
			sampler2D _MainTex;

			struct VertexInput
			{
				float4 vertex : POSITION;
			};

			struct VertexOutput
			{
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD0;
			};

			struct GBufferOut
			{
				half4 diffuse : SV_Target0;
				half4 specular : SV_Target1;
				half4 normal : SV_Target2;
				half4 emission : SV_Target3;
				float depth : SV_Depth;
			};

			VertexOutput vert(VertexInput v)
			{
				VertexOutput o;
				o.vertex = v.vertex;
				o.screenPos = o.vertex;
				return o;
			}

			GBufferOut frag(VertexOutput i)
			{
				float4 screenPos = i.screenPos;
#if UNITY_UV_STARTS_AT_TOP
				screenPos.y *= -1.0;
#endif
				screenPos.x *= _ScreenParams.x / _ScreenParams.y;

				float3 camPos = GetCameraPosition();
				float3 camDir = GetCameraForward();
				float3 camUp = GetCameraUp();
				float3 camSide = GetCameraRight();
				float focalLen = GetCameraFocalLength();
				float maxDistance = GetCameraMaxDistance();

				float3 rayDir = normalize(
					camSide * screenPos.x +
					camUp * screenPos.y +
					camDir * focalLen);

				float distance = 0.0;
				float len = 0.0;
				float3 pos = camPos + _ProjectionParams.y * rayDir;
				int loopNum = 30;
				for (int i = 0; i < loopNum; ++i)
				{
					distance = DistanceFunc(pos);
					len += distance;
					pos += rayDir * distance;
					if (distance < 0.001 || len > maxDistance)
						break;
				}

				if (distance > 0.001)
					discard;

				float depth = GetDepth(pos);
				float3 normal = GetNormal(pos);

				/*float u = fmod(pos.x, 1.0);
				float v = fmod(pos.y, 1.0);*/

				GBufferOut o;
				o.diffuse = _MainColor;
				o.specular = 0;
				o.emission = _EmissiveColor * abs(sin(_Time.y));
				o.depth = depth;
				o.normal = float4(normal, 1.0);

#ifndef UNITY_HDR_ON
				o.emission = exp2(-o.emission);
#endif
				return o;
			}

			ENDCG
		}
	}
	Fallback Off
}
