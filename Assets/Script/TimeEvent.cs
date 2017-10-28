using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 定时事件，在经过指定的时间后激发事件
/// </summary>
public class TimeEvent
{
	protected bool once;
	protected bool active;
	protected Action action;
	protected float riseTime;
	protected float passedTime;

	/// <summary>
	/// 定时事件构造函数
	/// </summary>
	/// <param name="riseTime">隔多长时间激发事件</param>
	/// <param name="action">所激发的事件</param>
	public TimeEvent(float riseTime, Action action)
	{
		this.riseTime = riseTime;
		this.action = action;
		once = false;
	}

	/// <summary>
	/// 定时事件构造函数
	/// </summary>
	/// <param name="riseTime">隔多长时间激发事件</param>
	/// <param name="action">所激发的事件</param>
	/// <param name="once">是否只激发一次事件</param>
	public TimeEvent(float riseTime, Action action, bool once) : this(riseTime, action)
	{
		this.once = once;
	}

	/// <summary>
	/// 启动定时
	/// </summary>
	public void Start()
	{
		// 如果已经启动了，则直接退出
		if (active) return;
		active = true;

		// 清除之前的计时
		passedTime = 0;

		// 开始接收时间更新
		GameTime.UpdateCall += ReceiveTimeUpdate;
	}

	/// <summary>
	/// 停止定时
	/// </summary>
	public void Stop()
	{
		// 如果已经停止了，则直接退出
		if (!active) return;
		active = false;

		// 停止接收时间更新
		GameTime.UpdateCall -= ReceiveTimeUpdate;
	}

	/// <summary>
	/// 检查时间，看是否到达定时时间
	/// </summary>
	/// <returns></returns>
	protected virtual bool CheckTime()
	{
		// 如果尚未到达定时时间，返回false
		if (passedTime < riseTime) return false;

		// 从累计时间中减去定时的时间
		passedTime -= riseTime;

		// 如果这个定时器只执行一次，停止定时器
		if (once) Stop();
		return true;
	}

	/// <summary>
	/// 时间更新函数
	/// </summary>
	/// <param name="deltaTime"></param>
	protected virtual void ReceiveTimeUpdate(float deltaTime)
	{
		// 累计时间
		passedTime += deltaTime;
		// 检查是否到达定时，且如果有可执行事件的话，执行它们
		if (CheckTime() && action != null)
			action.Invoke();
	}
}
