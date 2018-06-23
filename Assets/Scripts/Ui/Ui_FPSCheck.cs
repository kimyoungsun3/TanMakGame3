using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_FPSCheck : MonoBehaviour {
	public float UPDATE_INTERVAL = 0.5f;
	float accum, timeleft, fps;
	int frames;
	UILabel label;

	void Start(){
		label = GetComponent<UILabel> ();
		timeleft = UPDATE_INTERVAL;
	}

	void Update () {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;
		if (timeleft < 0) {
			fps = accum / frames;
			label.text = System.String.Format ("{0:F2} FPS", fps);

			timeleft = UPDATE_INTERVAL;
			accum = 0f;
			frames = 0;
		}
	}
}
