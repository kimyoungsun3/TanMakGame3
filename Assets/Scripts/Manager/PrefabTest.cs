using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour {
	[System.Serializable]
	public class PrefabObject{
		public GameObject prefab;
		public bool bTesting;
	}
	public List<PrefabObject> list = new List<PrefabObject>();

	void Awake () {
		for (int i = 0, iMax = list.Count; i < iMax; i++) {
			
			if (list [i].bTesting || list[i].prefab == null) { 
				continue;
			} else {
				Destroy (list [i].prefab);
			}
		}
		
	}
}
