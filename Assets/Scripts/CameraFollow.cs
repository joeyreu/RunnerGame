using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	public GameObject player;
	Vector3 offset;
	public float lerpRate;
	public bool gameOver;

	// Use this for initialization
	void Start () {
		offset = player.transform.position - transform.position;
		gameOver = false;
	}

	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			Follow ();
		}
	}

	void Follow(){
		Vector3 pos = transform.position;
		Vector3 targetPos = player.transform.position - offset;
		pos = Vector3.Lerp (pos, targetPos, lerpRate * Time.deltaTime); // delta time makes the rate the same accross platforms
		transform.position = pos;

	
	}
}
