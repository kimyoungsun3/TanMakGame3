using UnityEngine;
using System.Collections;

public class PoolReturnTime : MonoBehaviour {
	//지정된 시간이 되면 풀에 돌려주기.
	public float lifeTime;

	void OnEnable(){
		CancelInvoke ();
		Invoke ("Destroy", lifeTime);
	}

	void Destroy(){
		gameObject.SetActive (false);
	}

	void OnDisalbe(){
		CancelInvoke ();
	}
}
