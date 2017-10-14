using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlashColor : MonoBehaviour
{
	[SerializeField]
	protected Color color1;

	[SerializeField]
	protected Color color2;

	[SerializeField]
	protected float flashRate;

	protected Image image;

	private bool flashing;

	private void Start()
	{
		image = GetComponent<Image>();
	}

	public void StartFlash()
	{
		if (flashing) return;
		flashing = true;
		StartCoroutine(Flash());
	}

	public void StopFlash()
	{
		if (!flashing) return;
		flashing = false;
		StopAllCoroutines();
		image.color = color1;
	}

	protected IEnumerator Flash()
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
