using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMaterial : MonoBehaviour {
	public static PoolMaterial ins;
	Queue<MaterialData> queueMat;
	Dictionary<Material, Queue<MaterialData>> dicPools = new Dictionary<Material, Queue<MaterialData>>();
	public int count;
	void Awake(){
		ins = this;
	}

	//Material Init...
	public void RegisterMaterial(Material _srcSharedMaterial, int _srcCount){
		if (dicPools.ContainsKey (_srcSharedMaterial)) {
			Debug.LogError ("Same Material create");
		}else{
			//register queue Material...
			queueMat = new Queue<MaterialData> ();
			dicPools.Add (_srcSharedMaterial, queueMat);

			//create material...
			for(int i = 0; i < _srcCount; i++){
				Material _m = new Material (_srcSharedMaterial);
				queueMat.Enqueue (new MaterialData (_m));
			}
		}
	}

	//public void EndMaterial (MaterialData _md){
	//	_md.bUsing = false;
	//}

	public void ClearMaterila(Material _srcMaterial){
		if (!dicPools.ContainsKey (_srcMaterial)) {
			return;
		}

		queueMat = dicPools[_srcMaterial];
		MaterialData _md = null;
		for (int i = 0, iMax = queueMat.Count; i < iMax; i++) {
			_md = queueMat.Dequeue ();
			queueMat.Enqueue (_md);
			_md.bUsing = false;
		}
	}

	//1f	-> 1개정도로 커버됨...
	//.5f 	-> 3개정도 필요...
	public MaterialData GetMaterial(Material _srcMaterial){
		if (!dicPools.ContainsKey (_srcMaterial)) {
			Debug.LogError ("Material not register...");
			return null;
		}
		queueMat = dicPools[_srcMaterial];
		//Debug.Log ("dicPools:" + dicPools.Count
		//	+ " queueMat:" + queueMat.Count
		//);

		MaterialData _md = null;
		int _len = queueMat.Count;
		bool _bFind = false;

		//큐에서 찾기....
		for (int i = 0; i < _len; i++) {
			_md = queueMat.Dequeue ();
			queueMat.Enqueue (_md);
			if (!_md.bUsing) {
				//Debug.Log ("Find -> " + i + " t:" + queueMat.Count);
				_bFind = true;
				break;
			}
		}

		//못찾으면~~~ 신규로 생성....
		if (!_bFind) {
			//Debug.Log ("not found create");
			_md = new MaterialData (_md.mat);
			queueMat.Enqueue (_md);
		}
		count = queueMat.Count;
		_md.bUsing = true;
		return _md;
	}
}
