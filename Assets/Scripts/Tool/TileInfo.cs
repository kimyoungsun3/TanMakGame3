using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {
	UISprite uiSprite;
	UILabel uiLabel;
	public SpawnRowData2 data = new SpawnRowData2();

	void Awake(){
		uiSprite 	= GetComponent<UISprite> ();
		uiLabel 	= GetComponentInChildren<UILabel> ();
	}

	public void SetInit (string _name){
		//data.spawnPoint	= ;
		uiLabel.text = _name;

		SetUISprite (0.4f);
	}

	void OnPress(bool _bPress){
		//Debug.Log (this +":"+ _bPress);
		if (_bPress) {
			SpawnTool.ins.InvokeSelectedTile (this);
		}
	}

	public void SetUISprite(float _alpha){
		if (uiSprite != null) {
			uiSprite.alpha = _alpha;
		}
	}

	//Select -> Input
	public void SetInput(PalletInfo _palletInfo){
		data.enemyKind = _palletInfo.enemyKind;
		SetUISprite (1f);
	}
}
