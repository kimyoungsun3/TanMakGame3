using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveP1P2 : MonoBehaviour {
	public List<Transform> ways = new List<Transform>();
	public float speed = .5f;
	Vector3 wayPoint;
	int wayIndex = 0;

	void Start(){
		wayPoint = ways [wayIndex].position;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position == wayPoint) {
			wayIndex++;
			if (wayIndex >= ways.Count) {
				wayIndex = 0;
			}
			wayPoint = ways [wayIndex].position;
		}
		transform.position = Vector3.MoveTowards (transform.position, wayPoint, speed * Time.deltaTime);
	}
}
