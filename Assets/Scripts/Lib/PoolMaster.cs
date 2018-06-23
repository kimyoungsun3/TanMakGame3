using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolMaster : MonoBehaviour {
	[HideInInspector] public Transform trans;
	[HideInInspector] public GameObject go;

	public virtual void Awake(){
		trans 	= transform;
		go 		= gameObject;
	}

	public virtual void Init(){
		
	}

	public virtual void Play(){

	}

	public virtual void Destroy(){
		gameObject.SetActive (false);
	}

	//총알들...
	//public virtual void Seek(Transform _target, float _damage, float _radius = 0f){
	//}
}
