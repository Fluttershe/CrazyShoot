using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRelease : SpawnableObject
{
	[SerializeField]
	protected float releaseTime;

	Timer releaseTim;

	private void OnEnable()
	{
		Initialize();
	}

	public override void Initialize()
	{
		base.Initialize();
		releaseTim = new Timer(releaseTime, ReleaseSelf, true);
		releaseTim.Start();
	}
}
