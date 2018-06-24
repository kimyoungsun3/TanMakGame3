using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturnUI : PoolMaster {
	UILabel label;
	TweenAlpha tween;
	float waitTime;
	Coroutine co = null;

	public override void Awake(){
		base.Awake ();
		label = GetComponent<UILabel> ();

		tween = GetComponent<TweenAlpha> ();
		tween.enabled = false;
	}

	public void ShowMsg(string _msg){
		//show message
		label.text 	= _msg;
		waitTime 	= Time.time + tween.duration;
		if (co != null) {
			StopCoroutine (co);
		}
		co = StartCoroutine(CoShowMessage (_msg));

		//tween
		tween.enabled = true;
		tween.ResetToBeginning();
		tween.PlayForward();
	}

	IEnumerator CoShowMessage(string _msg){
		while (Time.time < waitTime) {
			yield return null;
		}

		tween.enabled = false;
		Destroy ();
	}
}
