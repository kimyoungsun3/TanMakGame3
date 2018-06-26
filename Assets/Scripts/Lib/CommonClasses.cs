using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

[System.Serializable]
public class Wave{
	public string enemyKind;		//자리수 5개(종류:health) + 횟수...
									//1:1:100:1:1:1		-> 일반...
									//1:1:100:1:1:999	-> 무한...
	public string enemyHealth;		//1:1:10:1:1
	public string enemySpeed;		//8:8:8:8:8
	public string enemyDamage;		//1:1:1:1:1
	//편집하기 편하게 하기 위해서.

	//[Space]
	public int intervalCount		= 1;
	public float intervalDelayTime	= 1f;
	public float spawnDelayTime 	= 1.5f;
	public bool spawnContinue 		= false;
	public Color color 				= Color.white;
	[HideInInspector] public int[] enemyKindVal 	= new int[5];
	[HideInInspector] public float[] enemyHealthVal = new float[5];
	[HideInInspector] public float[] enemySpeedVal 	= new float[5];
	[HideInInspector] public float[] enemyDamageVal = new float[5];
	
	public void Parse(int _index, Dictionary<int, GameObject> _dic){		
		Parse (enemyKindVal,  	enemyKind);
		Parse (enemyHealthVal, 	enemyHealth);
		Parse (enemySpeedVal, 	enemySpeed);
		Parse (enemyDamageVal, 	enemyDamage);

		if (spawnContinue) {
			spawnDelayTime = intervalCount * intervalDelayTime;
		}


		#if UNITY_EDITOR
		debugIndex = _index;
		//DisplayParseInfo ();

		//Enemy Kind의 매칭 정보 비교...
		if (!_dic.ContainsKey (enemyKindVal [0])){
			Debug.LogWarning ("Waves[" + debugIndex + "] EnemyKind 0번째 ["+enemyKindVal [0]+"] 존재안함");
			debugError = true;
		}else if(!_dic.ContainsKey (enemyKindVal [1])){
			Debug.LogWarning ("Waves[" + debugIndex + "] EnemyKind 1번째 ["+enemyKindVal [1]+"] 존재안함");
			debugError = true;
		}else if(!_dic.ContainsKey (enemyKindVal [2])){
			Debug.LogWarning ("Waves[" + debugIndex + "] EnemyKind 2번째 ["+enemyKindVal [2]+"] 존재안함");
			debugError = true;
		}else if(!_dic.ContainsKey (enemyKindVal [3])){
			Debug.LogWarning ("Waves[" + debugIndex + "] EnemyKind 3번째 ["+enemyKindVal [3]+"] 존재안함");
			debugError = true;
		}else if(!_dic.ContainsKey (enemyKindVal [4])){
			Debug.LogWarning ("Waves[" + debugIndex + "] EnemyKind 4번째 ["+enemyKindVal [4]+"] 존재안함");
			debugError = true;
		}
		#endif
	}


	#if UNITY_EDITOR
	public bool debugError = false;
	public int debugIndex;
	public void DisplayParseInfo(){
		Debug.Log ("index:" + debugIndex);
		Debug.Log (enemyKind + " => " + GetString (enemyKindVal));
		Debug.Log (enemyHealth + " => " + GetString (enemyHealthVal));
		Debug.Log (enemySpeed + " => " + GetString (enemySpeedVal));
		Debug.Log (enemyDamage + " => " + GetString (enemyDamageVal));
	}

	string GetString(int[] _arr){
		return "(" + _arr [0] + ", " + _arr [1] + ", " + _arr [2] + ", " + _arr [3] + ", " + _arr [4] + ")";
	}

	string GetString(float[] _arr){
		return "(" + _arr [0] + ", " + _arr [1] + ", " + _arr [2] + ", " + _arr [3] + ", " + _arr [4] + ")";
	}
	#endif

	void Parse(int[] _v, string _src){
		string[] _s = _src.Split (':');

		if (_s.Length == 5) {
			_v[0] = int.Parse(_s [0]);
			_v[1] = int.Parse(_s [1]);
			_v[2] = int.Parse(_s [2]);
			_v[3] = int.Parse(_s [3]);
			_v[4] = int.Parse(_s [4]);
			#if UNITY_EDITOR
		} else {
			Debug.LogWarning ("파라미터[" + debugIndex + "] error:" + _src);
			debugError = true;
			#endif
		}
	}

	void Parse(float[] _v, string _src){
		string[] _s = _src.Split (':');

		if (_s.Length == 5) {
			_v[0] = float.Parse(_s [0]);
			_v[1] = float.Parse(_s [1]);
			_v[2] = float.Parse(_s [2]);
			_v[3] = float.Parse(_s [3]);
			_v[4] = float.Parse(_s [4]);
			#if UNITY_EDITOR
		} else {
			Debug.LogWarning ("파라미터[" + debugIndex + "] error:" + _src);
			debugError = true;
			#endif
		}
	}
}


[System.Serializable]
public struct EnemyInfo{
	public int enemyKind;
	public GameObject enemyGameObject;
}

[System.Serializable]
public class MaterialData{
	public bool bUsing;
	public Material mat;
	public MaterialData(Material _m){
		bUsing = false;
		mat = _m;
	}

	public void Release(){
		bUsing = false;
	}
}