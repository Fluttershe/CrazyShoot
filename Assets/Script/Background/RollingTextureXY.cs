using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingTextureXY : MonoBehaviour
{
	[SerializeField, Range(0f, 1f)]
	float x;

	[SerializeField, Range(0f, 1f)]
	float y;

	Vector2 offset = Vector2.zero;
	Material material;

	// Use this for initialization
	void Start()
	{
		material = GetComponent<Renderer>().material;
	}

	// Update is called once per frame
	void Update()
	{
		material.mainTextureOffset = offset;
		offset.x += x * Time.deltaTime;
		offset.y += y * Time.deltaTime;
	}
}
