using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
	public float speed;
	public float upperlimit;
	public float lowerlimit;
	public GameObject block;

	// Use this for initialization
	void Start () {
		speed = speed / 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 current = block.transform.position;
		// reverse direction
		if (current.y > upperlimit || current.y < lowerlimit){ 
			speed *= -1; 
		}
		current.y += speed;
		block.transform.position = current;
	}
}
