using UnityEngine;

public class EnemySpawn : SpawnPoint
{
	[SerializeField]
	BoxArea area;

	[SerializeField]
	bool IsFront;

	TimeEvent quickenCooldown;

	protected override void Start() {
		base.Start();
		cooldown = Random.Range(0, (int)cooldownLimit.x);
		quickenCooldown = new TimeEvent(10, () => { cooldownLimit *= 0.9f; });
		quickenCooldown.Start();
	}

    protected override void InitializeObject(SpawnableObject obj)
	{
		if (area != null)
			(obj as Enemy).TargetPosition = area.GetRandomPoint();
		else
			(obj as Enemy).TargetPosition = transform.position;

		obj.transform.position = transform.position;
		(obj as Enemy).FromFront = IsFront;
	}
}
