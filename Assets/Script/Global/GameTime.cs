using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 游戏计时器，记录一场游戏从开始到结束的时间
/// </summary>
public class GameTime : SingleBehaviour<GameTime>
{
	private float passedTime = 0;
	private Action<float> updateCall;
	
	/// <summary>
	/// 更新事件，每一帧呼叫一次，给予每一帧与上一帧的时间差
	/// </summary>
	public static event Action<float> UpdateCall
	{
		add
		{
			// 加入更新事件
			Instance.updateCall += value;
		}

		remove
		{
			// 移除更新事件
			Instance.updateCall -= value;
		}
	}

	/// <summary>
	/// 从游戏开始后经过的时间
	/// </summary>
	public static float PassedTime
	{
		get { return Instance.passedTime; }
	}

	/// <summary>
	/// Unity内部更新
	/// </summary>
	private void Update()
	{
		this.passedTime += Time.deltaTime;
		if (updateCall != null)
			updateCall.Invoke(Time.deltaTime);
	}
}
