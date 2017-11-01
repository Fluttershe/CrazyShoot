using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Action = System.Action;

public sealed class SwitchScene : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer fadeBlock;

	[SerializeField]
	private float fadingRate;

	[SerializeField]
	private string startScene;

	private bool processing;
	private bool processed = true;

	private Action onSceneLoaded;
	public event Action OnSceneLoaded
	{
		add { onSceneLoaded += value; }
		remove { onSceneLoaded -= value; }
	}

	public void Start()
	{
		SceneManager.LoadScene(startScene);
	}

	public void Switch(string to)
	{
		Switch(SceneManager.GetActiveScene().name, to);
	}

	public void Switch(string from, string to)
	{
		if (!processed) return;
		
		StartCoroutine(SwitchScenesWithFadeInAndOut(from, to));
	}

	IEnumerator SwitchScenesWithFadeInAndOut(string from, string to)
	{
		processed = false;
		StartCoroutine(FadeOut());
		while (processing) yield return null;
		StartCoroutine(LoadNextScene(to));
		while (processing) yield return null;
		//StartCoroutine(UnloadCurrentScene(from));
		//while (processing) yield return null;
		StartCoroutine(FadeIn());
		processed = true;
		if (onSceneLoaded != null)
			onSceneLoaded.Invoke();
	}

	IEnumerator LoadNextScene(string sceneName)
	{
		processing = true;
		var opera = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		while (opera.progress < 0.9f)
			yield return null;
		processing = false;
	}

	IEnumerator UnloadCurrentScene(string sceneName)
	{
		processing = true;
		var opera = SceneManager.UnloadSceneAsync(sceneName);
		while (opera.progress < 0.9f)
			yield return null;
		processing = false;
	}

	IEnumerator FadeOut()
	{
		processing = true;
		Color color = fadeBlock.color;
		while (color.a < 1)
		{
			color.a += fadingRate * Time.deltaTime;
			fadeBlock.color = color;
			yield return null;
		}
		processing = false;
	}

	IEnumerator FadeIn()
	{
		processing = true;
		Color color = fadeBlock.color;
		while (color.a > 0)
		{
			color.a -= fadingRate * Time.deltaTime;
			fadeBlock.color = color;
			yield return null;
		}
		processing = false;
	}
}
