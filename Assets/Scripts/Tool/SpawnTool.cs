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

	List<PalletInfo> listPallet = new List<PalletInfo>();
	List<TileInfo> listTile 	= new List<TileInfo>();
	EnemySpawnData enemySpawnData;

	void Awake(){ 
		ins = this;
		enemySpawnData 	= GetComponent<EnemySpawnData> ();
		gridPallete 	= transStartPointPallete.GetComponent<UIGrid> ();

		CreatePallete ();

		CreateTile ();
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
		//InstancePalleteParent (PalletMode.Modify, -1, _parent, goSamplePallete, _pos);

		//EnemyList
		for(int i = 0, iMax = enemySpawnData.listEnemyKind.Count; i < iMax; i++){
			_go = InstancePalleteParent (PalletMode.Select, enemySpawnData.listEnemyKind [i].enemyNum, _parent, goSamplePallete, _pos);
			InstancePalleteFly (_go, enemySpawnData.listEnemyKind [i].enemyPrefab, _pos);
		}

		//Reposition
		gridPallete.Reposition ();
		DestroyImmediate (goSamplePallete);	
	}

	GameObject InstancePalleteParent(PalletMode _mode, int _enemyKind, GameObject _parent, GameObject _prefab, Vector3 _pos){
		GameObject _go = NGUITools.AddChild (_parent, _prefab);
		_go.transform.position = _pos;

		//Simple한 Info Class 추가...
		PalletInfo _palleteInfo = _go.AddComponent<PalletInfo> ();
		_palleteInfo.SetInit (_mode, _enemyKind);

		//리스트에 추가.
		listPallet.Add (_palleteInfo);

		return _go;
	}

	void InstancePalleteFly(GameObject _parent, GameObject _enemyPrefab, Vector3 _pos){
		GameObject _enmeyGO = NGUITools.AddChild (_parent, _enemyPrefab);
		_enmeyGO.transform.position = _pos;
		_enmeyGO.transform.localScale = Vector3.one * 100f;

		//Life된 것호출...
		_parent.GetComponent<PalletInfo>().SetPalleteFly(_enmeyGO);

		Enemy _enemy = _enmeyGO.GetComponent<Enemy> ();
		if (_enemy != null) {
			_enemy.enabled = false;
		}

		Collider _col = _enmeyGO.GetComponent<Collider> ();
		if (_col != null) {
			_col.enabled = false;
		}
	}

	//---------------------------
	//선택일때 정보. (Delete, Modify, Select)
	//---------------------------
	public PalletInfo cursorPallet;
	public TileInfo cursorTile, beforeTile;
	public void InvokeSelectedPallete(PalletInfo _pallet){
		cursorPallet = _pallet;
		for (int i = 0, iMax = listPallet.Count; i < iMax; i++) {
			listPallet [i].SetBoardAlpha (0.4f);
		}
		cursorPallet.SetBoardAlpha (1f);
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
			cursorTile.SetSelect (cursorPallet);
			break;
		case PalletMode.Delete:
			cursorTile.SetDelete (cursorPallet);
			break;
		case PalletMode.Modify:
			break;
		}
	}

	//----------------------------
	//Create Tile -> spawn Point
	//----------------------------
	void CreateTile(){
		//버튼들 생성....
		GameObject _go;
		string _strSpawnPoint;
		float disx = 140f;
		float disy = 140f;
		int xMax = 5, yMax = 5;
		Vector3 _pos = transStartPointTile.localPosition;
		UILabel _label;
		for (int x = 0; x < xMax; x++) {
			for (int y = 0; y < yMax; y++) {
				//create point
				_go = NGUITools.AddChild (gameObject, goSampleTile);
				_go.name = _strSpawnPoint = "" + x + y;
				_go.transform.localPosition = _pos + new Vector3 (x * disx, y * disy, 0);

				//Simple한 Info Class 추가...
				TileInfo _tileInfo = _go.GetComponent<TileInfo> ();
				_tileInfo.SetInit(_strSpawnPoint);

				//~~~~이걸로 조절.....
				listTile.Add (_tileInfo);
			}
		}
		DestroyImmediate (goSampleTile);	
	}
}
