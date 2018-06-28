using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {
	[Header("그냥 보기용")]
	UISprite uiSprite;
	UILabel uiLabel;
	public SpawnRowData data = new SpawnRowData();
	public GameObject enemyGO;

	[Space]
	[Header("비행기 정보")]
	public UILabel uiETC;

	void Awake(){
		uiSprite 	= GetComponent<UISprite> ();
		uiLabel 	= GetComponentInChildren<UILabel> ();
	}

	public void SetInit (string _name){
		uiLabel.text 		= _name;
		data.spawnPointStr 	= _name;
		uiETC.enabled 		= false;
		SetBoardAlpha (0.4f);
	}

	public void SetBoardAlpha(float _alpha){
		if (uiSprite != null) {
			uiSprite.alpha = _alpha;
		}
	}

	//-----------------------------------
	// 버튼 클릭 ->....
	//-----------------------------------
	void OnPress(bool _bPress){
		//Debug.Log (this +":"+ _bPress);
		if (_bPress) {
			SpawnTool.ins.InvokeSelectedTile (this);
		}
	}

	//Select, under Enemy GameObject Create...
	public void SetSelect(PalletInfo _palletInfo){
		if (data.enemyNum == _palletInfo.enemyNum) {
			//Debug.Log (1);
			return;
		}

		data.bSelect 		= true;
		data.enemyNum		= _palletInfo.enemyNum;
		SetBoardAlpha (1f);

		//비행기 오브젝트 소환....
		if (enemyGO != null) {
			DestroyImmediate (enemyGO);
		}
		enemyGO = NGUITools.AddChild (gameObject, _palletInfo.enemyGO);
		enemyGO.transform.localScale = Vector3.one * 100f;

		uiETC.enabled 		= true;
		InvokeRefreshETC ();
	}

	//Delete
	public void SetDelete(PalletInfo _palletInfo){
		data.bSelect 		= false;
		data.enemyNum		= -1;
		uiETC.enabled 		= false;
		SetBoardAlpha (.5f);

		//비행기 오브젝트 소환....
		if (enemyGO != null) {
			DestroyImmediate (enemyGO);
		}
	}

	//-----------------------------------
	//hp, speed, damage, ai
	//-----------------------------------
	public void InvokePopupUiETC(){
		//Debug.Log(this + "InvokePopupUiETC");
		Ui_ETC.ins.InvokePopup (this);
	}

	public void InvokeRefreshETC(){
		//Debug.Log (1);
		uiETC.text = data.enemyHealth + "\n"
		+ data.enemySpeed + "\n"
		+ data.enemyDamage + "\n"
		+ data.enemyAiType;
	}
}
