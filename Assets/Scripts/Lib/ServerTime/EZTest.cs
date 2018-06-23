using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EZTest{
	public class GameManager : MonoBehaviour {
		DateTime currentTime;

		void Start () {
			currentTime = DateTime.Now;
			Debug.Log ("Time:" + currentTime);
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Debug.Log ("EZTime:" + EZTime.ConvertLocalToServerTime (currentTime)
					+ " CT:" + DateTime.Now);
			}			
		}
	}
}