using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SceneInfo : MonoBehaviour {
	public UILabel msg;

	void Awake(){
		gameObject.SetActive (true);
		//Debug.Log (12);
	}

	public void SetActive2(bool _b){
		gameObject.SetActive (_b);
	}

	public void SetMessage(string _msg){
		msg.text = _msg;
	}
}
