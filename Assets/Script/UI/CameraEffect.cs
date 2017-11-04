using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour {

    [SerializeField]
    float shakeDec = 0.5f;

	[SerializeField]
	float shakeVol = 2;

    private int baozhakongzhi;

    Vector3 originalPos;

	static CameraEffect instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning(name + ": 存在多个CameraEffect！ 将删除多余CameraEffect。");
			Destroy(this);
		}

		instance = this;
	}

	private void Start()
	{
        Screen.SetResolution(1280, 800, true, 60);
        float screenAspect = 1280f / 800f; //现在android手机的主流分辨。

		Camera[] cameras = GetComponentsInChildren<Camera>();
		foreach (Camera c in cameras)
		{
			c.aspect = screenAspect; //摄像机的长宽比（宽度除以高度）
		}

        originalPos = transform.localPosition;
	}

    public static void Shake()
	{
		instance.ShakeCamera();
	}

	void ShakeCamera()
	{
        baozhakongzhi = Random.Range(1, 3);
        if (baozhakongzhi == 2)
        {
            StartCoroutine(ShakeCamera(shakeVol));
        }
	}

	IEnumerator ShakeCamera(float shakeVol)
	{
		while (shakeVol > 0)
        {
                float randomX = Random.Range(-shakeVol, shakeVol);
                float randomY = Random.Range(-shakeVol, shakeVol);
                transform.localPosition = originalPos + new Vector3(randomX, randomY, 0);
                shakeVol -= shakeDec;
                yield return null;
        }

		transform.localPosition = originalPos;
	}
}
