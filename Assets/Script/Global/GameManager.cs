using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏管理器
/// </summary>
public class GameManager : SingleBehaviour<GameManager>
{
	[SerializeField]
	private GameObject gamePanel;

	[SerializeField]
	private GameoverPanel gameoverPanel;

	[SerializeField]
	private MenuPanel mainMenu;

	[SerializeField]
	private SwitchScene sceneSwitcher;

	private PlayerStatistics state;

	protected override void Awake()
	{
		base.Awake();
		state = PlayerStatistics.GetStat();
		DontDestroyOnLoad(this.gameObject);

		ShowMainMenu(true);
	}

	private void OnApplicationQuit()
	{
		PlayerStatistics.SaveState();
	}

	public static void StartGame()
	{
		CreateInstance();
		PauseGame(false);
		Instance.ShowMainMenu(false);
		Instance.ShowGamePanel(true);
		Instance.ShowGameOver(false);

		var state = Instance.state;

		state.LastBasicEnemyKilled = 0;
		state.LastCash = 0;
		state.LastMGShootTimes = 0;
		state.LastPlayTime = 0;
	}

	public static void Gameover()
	{
		var state = Instance.state;
		state.LastPlayTime = GameTime.PassedTime;

		if (state.LongestPlayTime < state.LastPlayTime)
			state.LongestPlayTime = state.LastPlayTime;
		state.Cash += state.LastCash;
		state.MGShootTimes += state.LastMGShootTimes;
		state.BasicEnemyKilled += state.LastBasicEnemyKilled;

		CreateInstance();
		PauseGame(true);
		Instance.ShowMainMenu(false);
		Instance.ShowGameOver(true);
		Instance.ShowGamePanel(false);

	}

	public void ToMenu()
	{
		CreateInstance();
		PauseGame(false);
		if (sceneSwitcher == null) return;
		sceneSwitcher.Switch("Menu");
		sceneSwitcher.OnSceneLoaded += OnLoadedMenu;
	}

	public void ToPlay()
	{
		CreateInstance();
		PauseGame(false);
		if (sceneSwitcher == null) return;
		sceneSwitcher.Switch("Play");
		sceneSwitcher.OnSceneLoaded += OnLoadedPlay;
	}

	public static void PauseGame(bool pause)
	{
		CreateInstance();
		if (pause) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	private void OnLoadedMenu()
	{
		sceneSwitcher.OnSceneLoaded -= OnLoadedMenu;
		PauseGame(false);
		ShowMainMenu(true);
		ShowGamePanel(false);
		ShowGameOver(false);
	}

	private void OnLoadedPlay()
	{
		sceneSwitcher.OnSceneLoaded -= OnLoadedPlay;
		StartGame();
	}

	private void ShowMainMenu(bool show)
	{
		CreateInstance();
		if (mainMenu == null) return;

		mainMenu.gameObject.SetActive(show);
		mainMenu.ShowState();
	}

	private void ShowGamePanel(bool show)
	{
		CreateInstance();
		if (gamePanel == null) return;

		gamePanel.SetActive(show);
	}

	private void ShowGameOver(bool show)
	{
		CreateInstance();
		if (gameoverPanel == null) return;

		gameoverPanel.gameObject.SetActive(show);
		gameoverPanel.ShowState();
	}
}
