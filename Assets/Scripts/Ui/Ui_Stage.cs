using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Stage : MonoBehaviour {
	Transform trans;
	GameObject go;
	//public UILabel lvWave, lvEnemy;
	public GameObject point;
	public Vector3 p1, p2;
	public AnimationCurve curve;
	public float viewTime = 3f;

	void Start () {
		trans 	= transform;
		go 		= gameObject;
		trans.position = p1;
		point.SetActive (false);

		//Spawner.ins.callbackNextWave += OnNextWave;
	}

	/*
	void OnNextWave(int _waveIndex){
		lvWave.text 	= "-Wave " + (_waveIndex + 1);
		lvEnemy.text 	= "Enemies :" + Spawner.ins.waves [_waveIndex].enemyCount;

		StopCoroutine ("CoAnimation");
		StartCoroutine ("CoAnimation");	
	}

	IEnumerator CoAnimation(){
		point.SetActive (true);
		float _delay = 1f;
		float _speed = 1f / viewTime;
		float _percent = 0, _interpolation;
		while (_percent <= 1) {
			_percent += _speed * Time.deltaTime;
			_interpolation = curve.Evaluate (_percent);
			//Debug.Log (_percent + " > " + _interpolation);
			trans.localPosition = Vector3.Lerp(p1, p2, _interpolation);
			yield return null;
		}
		point.SetActive (false);
	}
	*/
}
