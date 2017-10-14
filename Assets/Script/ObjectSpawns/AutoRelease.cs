using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRelease : SpawnableObject
{
	[SerializeField]
	protected float releaseTime;

	[SerializeField]
	bool released;

	float releaseTimer;

	private void OnEnable()
	{
		Initialize();
	}

	public override void Initialize()
	{
		base.Initialize();
		released = false;
		releaseTimer = 0;
	}

	protected virtual void Update()
	{
		if (releaseTimer < releaseTime)
			releaseTimer += Time.deltaTime;
		else
			ReleaseSelf();
	}
}
