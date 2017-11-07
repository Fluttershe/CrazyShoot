using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Missile : Ammo
{
	[SerializeField, Range(0, 20)]
	private float explosionRange;

	private int explosionMask;

	private bool striked;

	public override void Initialize()
	{
		base.Initialize();
		striked = false;
		explosionMask = Physics2D.GetLayerCollisionMask(this.gameObject.layer);
	}

	protected override void OnTriggerEnter2D(Collider2D collider2D)
	{
		if (striked) return;
		striked = true;
		Physics2D.GetLayerCollisionMask(this.gameObject.layer);
		var result = Physics2D.OverlapCircleAll(transform.position, explosionRange, explosionMask);

		foreach (var obj in result)
		{
			base.OnTriggerEnter2D(obj);
		}
	}
}