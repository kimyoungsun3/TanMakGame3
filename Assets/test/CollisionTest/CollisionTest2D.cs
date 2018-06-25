using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest2D : MonoBehaviour {

	public float distance = 2f;
	[Range(0, 2)]public int mode = 0;
	public LayerMask mask;
	Color color;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			mode++;
			mode %= 2;
		}

		if (mode == 0) {
			color = Color.red;
			RaycastHit2D hit = Physics2D.Raycast (
				                   transform.position, 
				                   Vector2.up, 
				                   distance, 
				                   mask);
			if (hit) {
				color = Color.blue;
				Debug.Log (this + " > Physics2D.Raycast Hit");
			}
			Debug.DrawLine (transform.position, transform.position + Vector3.up * distance, color);
		} else if (mode == 1) {
			color = Color.green;

			Vector3 p1 = transform.position;
			Vector3 p2 = transform.position + Vector3.up * distance;
			RaycastHit2D hit = Physics2D.Linecast(p1, p2, mask);
			if(hit){
				color = Color.blue;
				Debug.Log (this + " > Physics2D.Linecast Hit");
			}
			Debug.DrawLine (transform.position, transform.position + Vector3.up * distance, color);
		}
	}
}
