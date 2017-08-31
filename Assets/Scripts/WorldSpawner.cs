using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour {

	// different types of terrain
	public GameObject flat1;

	// player - to track position
	public GameObject player;

	// tracking values
	Vector2 lastPos;
	float x_offset;
	float y_offset;

	// Use this for initialization
	void Start () {
		lastPos = flat1.transform.position;
		x_offset = flat1.GetComponent<SpriteRenderer>().bounds.size.x;
		y_offset = 0;

		Instantiate(flat1, lastPos, Quaternion.identity);
		
		SpawnFlat();
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerPos = player.transform.position;
		Vector2 diff = lastPos - playerPos;
		if(diff.x < 4){
			SpawnFlat();
		}

	}


	void SpawnFlat(){
		Vector2 pos = lastPos;
		pos.x += x_offset;
		pos.y += y_offset;
		Instantiate(flat1, pos, Quaternion.identity);

		lastPos = pos;
		x_offset = flat1.GetComponent<SpriteRenderer>().bounds.size.x;
		y_offset = 0;
	}
}
