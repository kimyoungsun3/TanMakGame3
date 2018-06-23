using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitMessage{
	public class CubeMove : MonoBehaviour {
		public List<Transform> localPoint = new List<Transform>();
		List<Vector3> wayPoints = new List<Vector3>();
		int index;
		Transform trans;
		Vector3 point;
		public float speed = 2f;
		public bool bMove = false;

		//Hit message
		public Camera gameCamera;
		public Camera uiCamera;
		//UICamera uiCameraScp;
		public UIFallowManager uiFallowManager;

		// Use this for initialization
		void Start () {
			//Move
			trans = transform;
			for (int i = 0; i < localPoint.Count; i++) {
				wayPoints.Add (localPoint [i].position);
			}
			index = 0;
			point = wayPoints [index];
			//uiCameraScp = uiCamera.GetComponent<UICamera> ();
		}


		void Update () {
			if (bMove && trans.position == point) {
				index++;
				if (index >= wayPoints.Count) {
					index = 0;
				}
				point = wayPoints [index];
				bMove = false;
			}

			trans.position = Vector3.MoveTowards (trans.position, point, speed * Time.deltaTime);
		}

		void OnPress (bool _bPress)
		{
			if (_bPress) {
				Debug.Log ("Click");
				bMove = true;
				//Debug.Log ("3D:" + trans.position + ":" + UICamera.lastWorldPosition);

				uiFallowManager.SetPosition ("msg", UICamera.lastWorldPosition, 2f);
			}

			//trans.position(3D) -> NGUI
			//Vector3 _vpos = gameCamera.WorldToViewportPoint(trans.position);

			//Vector3 _wpos = uiCamera.ViewportToWorldPoint (_vpos);
			//Vector3 _lpos = transform.InverseTransformPoint(pos);

			//HitMessage(20f, Vector3);
		}
	}
}