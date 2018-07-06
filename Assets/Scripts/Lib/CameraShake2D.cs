

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake2D : MonoBehaviour {
	public static CameraShake2D ins;
	Camera cam;
	Transform trans;

	public AnimationCurve curve;
	float shakeStartTime, shakeEndTime, shakeDurationTime;
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
	public void Shaking(float _duration = .5f){
		shakeDurationTime = _duration;
		shakeStartTime 	= Time.time;
		shakeEndTime 	= Time.time + shakeDurationTime;
		//oldPos 			= trans.position;
		//Debug.Log (" > Shaking Coroutine");

		StopAllCoroutines ();
		StartCoroutine (CoShake ());
	}

	IEnumerator CoShake(){
		while(Time.time < shakeEndTime) 
		{
			//float _interval = (Time.time - shakeStartTime) / shakeDurationTime;
			//offSet = Random.insideUnitCircle * shakeOffX;
			float _interval = (Time.time - shakeStartTime) / shakeDurationTime;
			float _x = curve.Evaluate (_interval) * shakeOffX;
			offSet.Set (Random.Range (-_x, _x), Random.Range (-_x, _x), 0);
			trans.position = oldPos + offSet;

			yield return null;
		}
		trans.position = oldPos;
	}

	//#if UNITY_EDITOR
	//Update is called once per frame
	//public float debugShakeDurationTime = .5f;
	//void LateUpdate () {
	//	//Debug.Log (this + ":" + 1);
	//	if (Input.GetKeyDown(KeyCode.Alpha1)) {
	//		Shaking (debugShakeDurationTime);
	//	}					
	//}
	//#endif
}