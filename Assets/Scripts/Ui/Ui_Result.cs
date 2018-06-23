using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Result : MonoBehaviour {
	GameObject go;
	
	void Awake(){
		go = gameObject;
		go.SetActive (false);
	}

	public void SetActive2(bool _b){
		if (go == null) {
			go = gameObject;
			go.SetActive (true);
		}
		gameObject.SetActive (_b);
	}

	public void InvokeAgain(){
		go.SetActive (false);
		GameManager.ins.Restart ();
	}
}
