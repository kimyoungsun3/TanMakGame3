using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

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