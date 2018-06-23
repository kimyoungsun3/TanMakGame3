using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour {
	float x = 2f;
	float y = 2f;
	public GameObject prefab;
	public List<GameObjectInfo> list = new List<GameObjectInfo> ();


	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Vector3 _pos = new Vector3 (Random.Range (-x, y), Random.Range (-x, y), 0);

			Instantiate (prefab, _pos, Quaternion.identity);
		}else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Vector3 _pos = new Vector3 (Random.Range (-x, y), Random.Range (-x, y), 0);

			Instantiate (list[0].prefab, _pos, Quaternion.identity);
		}else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			Vector3 _pos = new Vector3 (Random.Range (-x, y), Random.Range (-x, y), 0);

			PoolMaster _scp = PoolManager.ins.Instantiate ("HitEffectParticle", _pos, Quaternion.identity).GetComponent<PoolMaster>();
			_scp.Play ();
		}else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			Vector3 _pos = Vector3.zero;

			PoolMaster _scp = PoolManager.ins.Instantiate ("HitEffectParticle", _pos, Quaternion.identity).GetComponent<PoolMaster>();
			_scp.Play ();
		}else if (Input.GetKey (KeyCode.Alpha5)) {
			Vector3 _pos = new Vector3 (Random.Range (-x, y), Random.Range (-x, y), 0);

			PoolMaster _scp = PoolManager.ins.Instantiate ("HitEffectParticle", _pos, Quaternion.identity).GetComponent<PoolMaster>();
			_scp.Play ();
		}
	}

	[System.Serializable]
	public class GameObjectInfo{
		public GameObject prefab;
	}
}
