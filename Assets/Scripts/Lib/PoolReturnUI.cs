using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturnUI : PoolMaster {
	UILabel label;

	public override void Awake(){
		base.Awake ();
		label = GetComponent<UILabel> ();
	}

	public IEnumerator ShowMsg(string _msg, float _duration = 2f){
		label.text = _msg;
		float _waitTime = Time.time + _duration;
		while (Time.time < _waitTime) {
			yield return null;
		}

		Destroy ();
	}
}
