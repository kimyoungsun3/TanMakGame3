using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PalletMode { None, Delete, Modify, Select};
public class PalletInfo : MonoBehaviour {
	public PalletMode mode;
	public int enemyKind;
	public GameObject goFly;

	UISprite uiSprite;
	UILabel uiLabel;

	public void SetInit (PalletMode _mode, int _enemyKind){
		mode 		= _mode;
		enemyKind 	= _enemyKind;
		uiSprite 	= GetComponent<UISprite> ();
		uiLabel 	= GetComponentInChildren<UILabel> ();
		if (_mode == PalletMode.Delete || _mode == PalletMode.Modify) {
			ActiveDefault (_mode);
		}

		SetUISprite (0.4f);
	}
	//@@@@@@@@
	public void SetAddMemoryGoFly(GameObject _goFly){
		goFly 		= _goFly;
	}

	void ActiveDefault(PalletMode _mode){
		if (uiLabel != null) {
			uiLabel.text = _mode.ToString ();;
		}
	}

	void OnPress(bool _bPress){
		//Debug.Log (this +":"+ _bPress);
		SpawnTool.ins.InvokeSelectedPallete (this);
	}

	public void SetUISprite(float _alpha){
		if (uiSprite != null) {
			uiSprite.alpha = _alpha;
		}
	}
}
