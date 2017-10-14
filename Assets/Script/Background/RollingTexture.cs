using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingTexture : MonoBehaviour {

	[SerializeField, Range(0f, 1f)]
	float initialSpeed;

	[SerializeField, Range(0f, 1f)]
	float acceleration;

	Vector2 offset = Vector2.zero;
	Material material;

	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		material.mainTextureOffset = offset;
		offset.x += initialSpeed * Time.deltaTime;
		initialSpeed += acceleration * Time.deltaTime;
	}
}
