using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Work with slider.
/// </summary>
public class FlashColor : MonoBehaviour
{
	[SerializeField]
	Color color1;

	[SerializeField]
	Color color2;

	[SerializeField]
	float flashRate;

	[SerializeField]
	Image image;

	public void OnValueChangeCallback(float value)
	{
		if (value >= 1)
			StartCoroutine(Flash());
		if (value <= 0)
		{
			StopAllCoroutines();
			image.color = color1;
		}
	}

	IEnumerator Flash()
	{
		while (true)
		{
			image.color = color2;
			yield return new WaitForSeconds(flashRate);
			image.color = color1;
			yield return new WaitForSeconds(flashRate);
		}
	}
}
