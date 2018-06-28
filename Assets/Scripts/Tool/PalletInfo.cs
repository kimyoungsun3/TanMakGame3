using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PalletMode { None, Delete, Modify, Select};
public class PalletInfo : MonoBehaviour {
	public PalletMode mode;
	public int enemyNum;
	public GameObject enemyGO;

	UISprite uiSprite;
	UILabel uiLabel;

	public void SetInit (PalletMode _mode, int _enemyNum){
		mode 		= _mode;
		enemyNum 	= _enemyNum;
		uiSprite 	= GetComponent<UISprite> ();
		uiLabel 	= GetComponentInChildren<UILabel> ();
		if (_mode == PalletMode.Delete || _mode == PalletMode.Modify) {
			uiLabel.text = _mode.ToString ();;
		} else {
			uiLabel.text = "";
		}
		gameObject.name = _mode.ToString ();

		SetBoardAlpha (0.4f);
	}

	public void SetPalleteFly(GameObject _go){
		enemyGO 		= _go;
		gameObject.name = _go.name;
		uiLabel.text 	= _go.name;
	}

	void OnPress(bool _bPress){
		//Debug.Log (this +":"+ _bPress);
		if (_bPress) {
			SpawnTool.ins.InvokeSelectedPallete (this);
		}
	}

	public void SetBoardAlpha(float _alpha){
		if (uiSprite != null) {
			uiSprite.alpha = _alpha;
		}
	}
}
