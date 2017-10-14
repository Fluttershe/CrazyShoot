using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData : ScriptableObject
{

	[SerializeField]
	float fireDispersion;

	[SerializeField]
	float heatPerShoot;

	[SerializeField]
	float heatDispSpeed;

	[SerializeField]
	StateValue heat;

	[SerializeField]
	StateValue reloadTime;

	[SerializeField]
	StateValue magazine;

	[SerializeField]
	StateValue delayBeforeFire;

	[SerializeField]
	StateValue delayBeforeHeatDisp;

	[SerializeField]
	bool triggerHeld;

	[SerializeField]
	bool overheated;
}
