using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Runtime.Serialization;

/// <summary>
/// 玩家数据
/// </summary>
[System.Serializable]
public sealed class PlayerStatistics
{
	/// <summary>
	/// 玩家持有的金钱
	/// </summary>
	public int Cash { get; set; }

	/// <summary>
	/// 基础敌人的击杀数
	/// </summary>
	public int BasicEnemyKilled { get; set; }

	/// <summary>
	/// 上次玩家赚到的金钱
	/// </summary>
	public int LastCash { get; set; }

	/// <summary>
	/// 上次基础敌人的击杀数
	/// </summary>
	public int LastBasicEnemyKilled { get; set; }

	/// <summary>
	/// 该类的唯一实例
	/// </summary>
	private static PlayerStatistics instance;

	/// <summary>
	/// 私有构造，确保不会出现其它的实例
	/// </summary>
	private PlayerStatistics() { }

	/// <summary>
	/// 创造实例
	/// </summary>
	private static void CreateInstance()
	{
		// 如果已经有实例，退出
		if (instance != null) return;

		// 尝试从文件中读取，如果成功，退出
		LoadState();
		if (instance != null) return;

		// 新建实例
		instance = new PlayerStatistics();
	}

	/// <summary>
	/// 获取玩家数据的实例
	/// </summary>
	/// <returns></returns>
	public static PlayerStatistics GetStat()
	{
		CreateInstance();
		return instance;
	}

	/// <summary>
	/// 保存玩家数据到硬盘中
	/// </summary>
	public static void SaveState()
	{
		CreateInstance();
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Create(Application.persistentDataPath + "/save.bin");

		formatter.Serialize(saveFile, instance);

		saveFile.Close();
	}

    /// <summary>
    /// 从硬盘中读取玩家数据
    /// </summary>
    public static void LoadState()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile;

        try
        {
            saveFile = File.Open(Application.persistentDataPath + "/save.bin", FileMode.Open);
        }
        catch (FileNotFoundException)
        {
            return;
        }

        try
        {
            instance = (PlayerStatistics)formatter.Deserialize(saveFile);
        }
        catch (SerializationException)
        {
            Debug.LogWarning("读取到旧版本存档，旧版本存档将会被覆盖");
        }

        saveFile.Close();
    }
}
