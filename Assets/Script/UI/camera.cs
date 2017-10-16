using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public Camera manCamera;

    [SerializeField]
    float shakeDec = 0.8f;

	[SerializeField]
	float shakeVol = 2;

    Vector3 originalPos;

    void Start() {
        Screen.SetResolution(1280, 800, true, 60);
        float screenAspect = 1280f / 800f; //现在android手机的主流分辨。
        manCamera.aspect = screenAspect; //摄像机的长宽比（宽度除以高度）
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (Character.ZhenDong == true) {
			StartCoroutine(ShakeCamera(shakeVol));
			Character.ZhenDong = false;
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
