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

	public void InitFirst (string _name){
		uiLabel.text 		= _name;
		data.spawnPointStr 	= _name;
		uiETC.enabled 		= false;
		SetBoardAlpha (Constant.ALPHA_NOSELECT);
	}

	public void SetBoardAlpha(float _alpha){
		if (uiSprite != null) {
			uiSprite.alpha = _alpha;
		}
	}

	public void SetVisibleETC(bool _b){
		if(uiETC != null){
			uiETC.gameObject.SetActive (_b);
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
	string strBeforeDefaultValue = "";
	public void SetSelect(PalletInfo _palletInfo, string _strDefaultValue){
		if (data.enemyNum == _palletInfo.enemyNum && strBeforeDefaultValue == _strDefaultValue) {
			//Debug.Log (1);
			return;
		}

		//default value...
		//5:8:1:1
		string[] _v = _strDefaultValue.Split(':');
		if (_v.Length != 4) {
			Debug.LogWarning ("Default Health:Speed:Damage:AI");
			return;
		}
		data.enemyHealth = float.Parse(_v [0]);
		data.enemySpeed = float.Parse(_v [1]);
		data.enemyDamage = float.Parse(_v [2]);
		data.enemyAiType = int.Parse(_v [3]);
		strBeforeDefaultValue = _strDefaultValue;

		data.bSelect 		= true;
		data.enemyNum		= _palletInfo.enemyNum;
		SetBoardAlpha (Constant.ALPHA_SELECT);

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
		SetBoardAlpha (Constant.ALPHA_NOSELECT);

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
