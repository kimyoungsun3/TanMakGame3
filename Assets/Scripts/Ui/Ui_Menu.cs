using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Menu : MonoBehaviour {
	public static Ui_Menu ins;
	[HideInInspector]
	public bool bFire = false;

	void Awake(){
		ins = this;
		#if UNITY_EDITOR
			gameObject.SetActive(false);
		#endif
	}

	public void InvokeFire(){
		bFire = !bFire;
	}

	public void InvokeChange(){

	}
}
