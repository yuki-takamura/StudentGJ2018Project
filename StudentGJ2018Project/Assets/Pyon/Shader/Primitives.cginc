#ifndef primitives_cginc
#define primitives_cginc

float dot2(float3 pos)
{
	return dot(pos, pos);
}

float sphere(float3 pos, float radius)
{
	return length(pos) - radius;
}

float roundBox(float3 pos, float3 size, float round)
{
	return length(max(abs(pos) - size * 0.5, 0.0)) - round;
}

float box(float3 pos, float3 size)
{
	return roundBox(pos, size, 0);
}

float signedBox(float3 pos, float3 size)
{
	float3 d = abs(pos) - size;
	return min(max(d.x, max(d.y, d.z)), 0.0) + length(max(d, 0.0));
}

float bar(float2 pos, float width)
{
	return length(max(abs(pos) - width, 0.0));
}

float torus(float3 pos, float2 radius)
{
	float2 r = float2(length(pos.xy) - radius.x, pos.z);
	return length(r) - radius.y;
}

float floor(float3 pos)
{
	return dot(pos, float3(0.0, 1.0, 0.0)) + 1.0;
}

float cylinder(float3 pos, float2 r)
{
	float2 d = abs(float2(length(pos.xy), pos.z)) - r;
	return min(max(d.x, d.y), 0.0) + length(max(d, 0.0)) - 0.1;
}

float triangleDistanceFunc(float3 pos, float3 a, float3 b, float3 c)
{
	float3 ba = b - a;
	float3 pa = pos - a;

	float3 cb = c - b;
	float3 pb = pos - b;

	float3 ac = a - c;
	float3 pc = pos - c;

	float3 nor = cross(ba, ac);

	return sqrt(
		(sin(dot(cross(ba, nor), pa)) +
			sin(dot(cross(cb, nor), pb)) +
			sin(dot(cross(ac, nor), pc)) < 2.0)
		?
		min(min(
			dot2(ba * clamp(dot(ba, pa) / dot2(ba), 0.0, 1.0) - pa),
			dot2(cb * clamp(dot(cb, pb) / dot2(cb), 0.0, 1.0) - pb)),
			dot2(ac * clamp(dot(ac, pc) / dot2(ac), 0.0, 1.0) - pc))
		:
		dot(nor, pa) * dot(nor, pa) / dot2(nor));
}

float triPrism(float3 pos, float2 hight)
{
	float3 q = abs(pos);
	return max(q.z - hight.y, max(q.x * 0.866025 + pos.y * 0.5, -pos.y) - hight.x * 0.5);
}

#endif