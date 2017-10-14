using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class ExtensionMethods
{
	/// <summary>
	/// 以x为最小值，y为最大值，在这之间随机取一整数
	/// </summary>
	/// <param name="vector"></param>
	/// <returns></returns>
	public static int RandomInt(this Vector2 vector) {
		return Random.Range((int)vector.x, (int)vector.y);
	}

	/// <summary>
	/// 以x为最小值，y为最大值，在这之间随机取一浮点数
	/// </summary>
	/// <param name="vector"></param>
	/// <returns></returns>
	public static float RandomFloat(this Vector2 vector) {
		return Random.Range(vector.x, vector.y);
	}

	/// <summary>
	/// 判断一个Vector2的长度是否短于一个给定长度
	/// </summary>
	/// <param name="vector"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	public static bool IsShorterThan(this Vector2 vector, float length) {
		return vector.sqrMagnitude < (length * length);
	}

	/// <summary>
	/// 判断一个Vector2的长度是否长于一个给定长度
	/// </summary>
	/// <param name="vector"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	public static bool IsLongerThan(this Vector2 vector, float length)
	{
		return vector.sqrMagnitude > (length * length);
	}
}