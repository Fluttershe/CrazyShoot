using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffect : AutoRelease
{
	protected void Start()
	{
		releaseTime = GetComponent<ParticleSystem>().main.duration;
	}
}
