using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
using UnityEngine;

public class FPSCheckNGUI : MonoBehaviour {
	float updateInterval = .5f;
	float accum, timeleft, fps;
	int frames;
	//string strFps;
	UILabel lvFps;

	void Start () {
		lvFps 		= GetComponent<UILabel> ();
		timeleft 	= updateInterval;
	}

	//void OnGUI(){
	//	GUI.Label(new Rect(0, 0, 100, 100), strFps);
	//}

	void Update () {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;
		if (timeleft < 0) {
			fps = accum / frames;
			lvFps.text = System.String.Format ("{0:F2} FPS", fps);

			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
		
	}
}
