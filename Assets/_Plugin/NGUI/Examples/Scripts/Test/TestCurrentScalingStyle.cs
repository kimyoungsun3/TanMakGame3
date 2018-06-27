using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestCurrentScalingStyle : MonoBehaviour {
	public UIRoot root;
	public UILabel label;


	void Start () {
		label.text = "" + root.scalingStyle;
	}

	public void InvokeChangeScene0(){
		SceneManager.LoadScene (0);
	}

	public void InvokeChangeScene1(){
		SceneManager.LoadScene (1);
	}

	public void InvokeChangeScene2(){
		SceneManager.LoadScene (2);
	}
}
