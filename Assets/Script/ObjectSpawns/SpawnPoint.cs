using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出生点
/// </summary>
public class SpawnPoint : GameTime {

	// 出生的对象
	[SerializeField]
	protected SpawnableObject objPrefab;

	// 出生的冷却
	[SerializeField, MinMaxSlider(0, 4)]
	protected Vector2 cooldownLimit;
	protected float cooldown;

	// 最多出生的数量
	[Tooltip("为负数时不限制生成量")]
	[SerializeField]
	protected int maxSpawnAmount = -1;

	// 已出生的数量
	[SerializeField]
	protected int spawnedAmount = 0;

	protected virtual void Start() {

		// 如果在出生对象上找不到SpawnableObject的话...
		if (objPrefab == null) {
			// ..报告错误并停止该出生点
			Debug.LogError(name + ": 这个出生点的出生对象Prefab上没有SpawnableObject, 此出生点将会关闭.");
			this.enabled = false;
		}
	}

	protected virtual void Update() {
		// 如果出生数量达到上限，退出
		if (spawnedAmount >= maxSpawnAmount &&
			maxSpawnAmount >= 0) return;

		// 如果冷却完了..
		if (CountingCooldown()) { 
			// ..生成一个对象
			SpawnObject();
		}
    }

	/// <summary>
	/// 冷却计数，当冷却完成时返回true, 否则返回false
	/// </summary>
	/// <returns></returns>
	public virtual bool CountingCooldown() {
		if (cooldown > 0) cooldown -= Time.deltaTime;
		return cooldown <= 0;
	}

	/// <summary>
	/// 生成一个对象
	/// </summary>
	protected virtual void SpawnObject() {
		// 生成一个对象
		SpawnableObject obj = ObjectPool.Acquire(objPrefab);

		obj.transform.position = transform.position;
		obj.transform.rotation = transform.rotation;

		// 初始化对象
		InitializeObject(obj);
		obj.Initialize();

		// 挂上对象毁灭事件
		obj.Destroyed += CharacterDestroyed;

		// 生成结束
		SpawnOver();
	}

	/// <summary>
	/// 当一个对象生成完毕后这个函数将被调用
	/// </summary>
	protected virtual void SpawnOver() {
		// 重置冷却
		cooldown = cooldownLimit.RandomFloat();
		spawnedAmount++;
	}

	/// <summary>
	/// 当一个对象生成时这个函数将被调用来初始化这个对象
	/// </summary>
	protected virtual void InitializeObject(SpawnableObject obj) { }

	/// <summary>
	/// 当一个生成的对象被摧毁时这个函数将被调用
	/// </summary>
	protected virtual void CharacterDestroyed() {
		spawnedAmount --;
	}
}
