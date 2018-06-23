using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturnAnimation : PoolMaster {
	Animator anim;

	public override void Awake(){
		anim = GetComponent<Animator> ();
	}

	public void Play(string _name){
		//Debug.Log (this + "Play" + _name);
		anim.Play (_name);
	}

	public override void Destroy(){
		//Debug.Log (this + "Destroy");
		base.Destroy ();
	}
}
