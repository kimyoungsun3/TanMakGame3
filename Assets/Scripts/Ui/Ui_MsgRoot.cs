using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_MsgRoot : MonoBehaviour {
	public static Ui_MsgRoot ins;
	//public Transform prefabDevTest;
	Camera gameCamera;
	Camera uiCamera;
	Transform trans;

	void Awake(){
		ins = this;

		gameCamera 	= Camera.main;
		uiCamera 	= NGUITools.FindCameraForLayer(gameObject.layer);
		trans 		= transform;

		//prefabDevTest.gameObject.SetActive (false);
	}

	public void InvokeShowMessage(string _prefabName, string _msg, Vector3 _pos, float _duration){
		//3D World -> 3D viewport -> NGUI viewport
		//         -> NGUI 3D world
		//            .position 그대로 넣기...
		Vector3 _vpos = gameCamera.WorldToViewportPoint(_pos);
		Vector3 _wpos = uiCamera.ViewportToWorldPoint(_vpos);
		_wpos.z = transform.position.z;

		//UI Message
		PoolReturnUI _p2 = PoolManager.ins.Instantiate(_prefabName, _wpos, Quaternion.identity).GetComponent<PoolReturnUI>();
		_p2.ShowMsg (_msg);
	}
	
	// Update is called once per frame
	//void Update () {
	//}
}
