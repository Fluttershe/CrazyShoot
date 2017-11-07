using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveTriggerEvent : MonoBehaviour {
	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		print(collision.name);
	}
}
