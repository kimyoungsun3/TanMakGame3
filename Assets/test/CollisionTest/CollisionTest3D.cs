using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest3D : MonoBehaviour {

	[Range(0, 2)] public int mode = 0;
	public float distance = 2f;
	public LayerMask mask;
	Color color;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			mode++;
			mode %= 2;
		}
		
		if (mode == 0) {
			color = Color.red;

			Ray ray = new Ray (transform.position, Vector3.up);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, distance, mask, QueryTriggerInteraction.Collide)) {
				color = Color.blue;
				Debug.Log (this + " > Physics.Raycast Hit");
			}
			Debug.DrawLine (transform.position, transform.position + Vector3.up * distance, color);

		} else if (mode == 1) {
			color = Color.green;

			Vector3 p1 = transform.position;
			Vector3 p2 = transform.position + Vector3.up * distance;
			RaycastHit hit;
			if(Physics.Linecast(p1, p2, out hit, mask, QueryTriggerInteraction.Collide)){
				color = Color.blue;
				Debug.Log (this + " > Physics.Linecast Hit");
			}
			Debug.DrawLine (transform.position, transform.position + Vector3.up * distance, color);
		}
	}
}
