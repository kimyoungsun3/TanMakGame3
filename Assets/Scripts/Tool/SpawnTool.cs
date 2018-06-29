using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTool : MonoBehaviour {
	public static SpawnTool ins;
	public GameObject goSamplePallete;
	public Transform transStartPointPallete;

	public GameObject goSampleTile;
	public Transform transStartPointTile;
	UIGrid gridPallete;
	public UIInput uiiIntervalCount, uiiIntervalDelayTime, uiiSpawnDelayTime;
	public UIInput uiiDisplayInfo;
	public UIInput uiiHealth, uiiSpeed, uiiDamage, uiiAI;

	List<PalletInfo> listPallet = new List<PalletInfo>();
	List<TileInfo> listTile 	= new List<TileInfo>();
	EnemySpawnData enemySpawnData;

	void Awake(){ 
		ins = this;
		enemySpawnData 	= GetComponent<EnemySpawnData> ();
		gridPallete 	= transStartPointPallete.GetComponent<UIGrid> ();

		CreatePallete ();

		CreateTile ();

		InitFirst ();
	}

	void InitFirst(){
		uiiIntervalCount.value 		= 1+"";
		uiiIntervalDelayTime.value 	= 1f+"";
		uiiSpawnDelayTime.value 	= -1f+"";
	}

	public void InvokeSpawnDelayTime(){
		uiiSpawnDelayTime.value = "" + float.Parse (uiiIntervalCount.value) * float.Parse (uiiIntervalDelayTime.value);
	}

	//----------------------------
	//Create Pallete -> Empty and Enemy
	//----------------------------
	//Create Enemy Prefab list
	void CreatePallete(){
		GameObject _go;
		GameObject _parent 	= transStartPointPallete.gameObject;
		Vector3 _pos 		= transStartPointPallete.transform.position;

		//Delete Pallete
		InstancePalleteParent (PalletMode.Delete, -1, _parent, goSamplePallete, _pos);
		InstancePalleteParent (PalletMode.Modify, -1, _parent, goSamplePallete, _pos);

		//Pallete + Enemy GameObject
		for(int i = 0, iMax = enemySpawnData.listEnemyKind.Count; i < iMax; i++){
			_go = InstancePalleteParent (PalletMode.Select, enemySpawnData.listEnemyKind [i].enemyNum, _parent, goSamplePallete, _pos);
			InstancePalleteEnemy (_go, enemySpawnData.listEnemyKind [i].enemyPrefab, _pos);
		}

		//Reposition
		gridPallete.Reposition ();
		DestroyImmediate (goSamplePallete);	
	}

	//SamplePallete
	GameObject InstancePalleteParent(PalletMode _mode, int _enemyNum, GameObject _parent, GameObject _prefabSamplePallet, Vector3 _pos){
		GameObject _goPallet = NGUITools.AddChild (_parent, _prefabSamplePallet);
		_goPallet.transform.position = _pos;

		//Simple한 Info Class 추가...
		PalletInfo _scpPalletInfo = _goPallet.AddComponent<PalletInfo> ();
		_scpPalletInfo.InitFirst (_mode, _enemyNum);

		//리스트에 추가.
		listPallet.Add (_scpPalletInfo);

		return _goPallet;
	}

	//Pallet 밑에 Enemy GameObject만들기...
	void InstancePalleteEnemy(GameObject _parentPallet, GameObject _enemyPrefab, Vector3 _pos){
		GameObject _goEnemy = NGUITools.AddChild (_parentPallet, _enemyPrefab);
		_goEnemy.transform.position = _pos;
		_goEnemy.transform.localScale = Vector3.one * 100f;

		//Life된 것호출...
		_parentPallet.GetComponent<PalletInfo>().SetPalletEnemy(_goEnemy);

		Enemy _scpEnemy = _goEnemy.GetComponent<Enemy> ();
		if (_scpEnemy != null) {
			_scpEnemy.enabled = false;
		}

		Collider _col = _goEnemy.GetComponent<Collider> ();
		if (_col != null) {
			_col.enabled = false;
		}
	}

	//---------------------------
	//선택일때 정보. (Delete, Modify, Select)
	//---------------------------
	public PalletInfo cursorPallet;
	public TileInfo cursorTile, beforeTile;
	bool bTileVisibleETC;
	public void InvokeSelectedPallete(PalletInfo _pallet){
		cursorPallet = _pallet;
		for (int i = 0, iMax = listPallet.Count; i < iMax; i++) {
			listPallet [i].SetBoardAlpha (Constant.ALPHA_NOSELECT);
		}
		cursorPallet.SetBoardAlpha (Constant.ALPHA_SELECT);

		switch (cursorPallet.mode) {
		case PalletMode.Select:
		case PalletMode.Delete:
			break;
		case PalletMode.Modify:
			for (int i = 0, iMax = listTile.Count; i < iMax; i++) {
				listTile [i].SetVisibleETC (bTileVisibleETC);
			}
			bTileVisibleETC = !bTileVisibleETC;
			break;
		}
	}

	public void InvokeSelectedTile(TileInfo _tile){
		if (cursorPallet == null) {
			Debug.Log (" > 비행기, 삭제, 수정을 선택하세요");
			return;
		}
		beforeTile = cursorTile;
		cursorTile = _tile;

		switch(cursorPallet.mode){
		case PalletMode.Select:
			cursorTile.SetSelect (cursorPallet, this);
			InvokeCalculData ();
			break;
		case PalletMode.Delete:
			cursorTile.SetDelete (cursorPallet);
			InvokeCalculData ();
			break;
		case PalletMode.Modify:
			break;
		}

	}

	//--------------------------------------
	// 정보 취합.....
	//--------------------------------------
	System.Text.StringBuilder msg = new System.Text.StringBuilder();
	public void InvokeCalculData(){
		//intervalCount : intervalDelayTime : spawnDelayTime
		//"4:1.5:-1
		msg.Length = 0;
		msg.Append (uiiIntervalCount.value);
		msg.Append (':');
		msg.Append (uiiIntervalDelayTime.value);
		msg.Append (':');
		msg.Append (uiiSpawnDelayTime.value);

		//spawnPoint : enemyNum : health : speed : damage : AItype
		//@00:1:4:8:1:1@10:2:4:8:1:1
		SpawnRowData _data;
		int _count = 0;
		for (int i = 0, iMax = listTile.Count; i < iMax; i++) {
			_data = listTile [i].data;
			if (!_data.bSelect) {
				continue;
			}

			msg.Append ('@');
			msg.Append (_data.spawnPointStr);
			msg.Append (':');
			msg.Append (_data.enemyNum);
			msg.Append (':');
			msg.Append (_data.enemyHealth);
			msg.Append (':');
			msg.Append (_data.enemySpeed);
			msg.Append (':');
			msg.Append (_data.enemyDamage);
			msg.Append (':');
			msg.Append (_data.enemyAiType);
			_count++;
		}

		if (_count <= 0) {
			msg.Length = 0;
			msg.Append ("Pallet Click -> Tile Click");
		}

		uiiDisplayInfo.value = msg.ToString ();
		Debug.Log (msg.ToString ());
	}

	public void InvokeAllClearTile(){
		for (int i = 0, iMax = listTile.Count; i < iMax; i++) {
			listTile [i].SetDelete (null);
		}
		InvokeCalculData ();
	}

	//----------------------------
	//Create Tile -> spawn Point
	//----------------------------
	void CreateTile(){
		//버튼들 생성....
		GameObject _goTile;
		string _strSpawnPoint;
		float disx = 140f;
		float disy = 140f;
		int xMax = 5, yMax = 5;
		Vector3 _pos = transStartPointTile.localPosition;
		UILabel _label;
		for (int x = 0; x < xMax; x++) {
			for (int y = 0; y < yMax; y++) {
				//create point
				_goTile = NGUITools.AddChild (gameObject, goSampleTile);
				_goTile.name = _strSpawnPoint = "" + x + y;
				_goTile.transform.localPosition = _pos + new Vector3 (x * disx, y * disy, 0);

				//Simple한 Info Class 추가...
				TileInfo _tileInfo = _goTile.GetComponent<TileInfo> ();
				_tileInfo.InitFirst(_strSpawnPoint);

				//~~~~이걸로 조절.....
				listTile.Add (_tileInfo);
			}
		}
		DestroyImmediate (goSampleTile);	
	}
}
