using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

[System.Serializable]
public class Wave{
	public bool bInfinite;
	public int enemyCount;
	public int enemyAliveMax;
	public float nextTime;

	//Enmey Info...
	//public Enemy enemy;
	public float speed;
	public float damage;
	public float health;
	public Color color;
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