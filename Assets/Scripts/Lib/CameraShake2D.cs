

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake2D : MonoBehaviour {
	public static CameraShake2D ins;
	Camera cam;
	Transform trans;

	//public AnimationCurve curve;
	float shakeStartTime, shakeEndTime;
	public float shakeDurationTime = .5f;
	public float shakeOffX = 1f;
	Vector3 oldPos, offSet;
	bool bShakeStart, bShakeEnd;

	// Use this for initialization
	void Awake () {
		ins		= this;
		cam 	= GetComponent<Camera>();	
		trans 	= cam.transform;
		oldPos 	= trans.position;
		//Debug.Log ("Num 1 is Screen Shake(Coroutine)");
	}

	//Coroutine co;
	public void Shaking(){
		shakeStartTime 	= Time.time;
		shakeEndTime 	= Time.time + shakeDurationTime;
		//oldPos 		= trans.position;
		//Debug.Log (" > Shaking Coroutine");

		StopAllCoroutines ();
		StartCoroutine (CoShake ());
	}

	IEnumerator CoShake(){
		while(Time.time < shakeEndTime) 
		{
			float _interval = (Time.time - shakeStartTime) / shakeDurationTime;
			offSet = Random.insideUnitCircle * shakeOffX;
			trans.position = oldPos + offSet;

			yield return null;
		}
		trans.position = oldPos;
	}

	#if UNITY_EDITOR
	//Update is called once per frame
	void LateUpdate () {
		//Debug.Log (this + ":" + 1);
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			Shaking ();
		}					
	}
	#endif
}