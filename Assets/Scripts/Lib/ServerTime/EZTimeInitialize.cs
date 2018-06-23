using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZTimeInitialize : MonoBehaviour {
	[HideInInspector] public string url = "https://script.google.com/macros/s/AKfycbwnUNuHBYbWODhy6wQ8H4JxRgGHqE-LPpk_CZhEuBkLMvfAd8U/exec";
	public static bool bInitialized = false;

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
	}

	void OnApplicationFocus(bool _bFocusStatus){
		if (_bFocusStatus) {
			Debug.LogWarning ("EZTime Game focus");
			StopAllCoroutines ();
			StartCoroutine (Init ());
		} else {
			bInitialized = false;
		}
	}

	IEnumerator Init(){
		yield return StartCoroutine(EZTime.GetServerTime(url));
		Debug.Log ("EZTime.GetServerTime Completed.");
		bInitialized = true;
	}
}
