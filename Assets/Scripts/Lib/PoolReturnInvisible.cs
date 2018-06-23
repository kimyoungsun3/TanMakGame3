using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Renderer))]
public class PoolReturnInvisible : MonoBehaviour {
	//화면에서 사라지면 돌려주기.
	//renderer가 반드시 있어야한다.

	void OnBecameInvisible(){
		gameObject.SetActive (false);
	}
}
