using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTool1 : MonoBehaviour {
	public static SpawnTool1 ins;
	public GameObject goSamplePallete;
	public Transform transStartPointPallete;

	public GameObject goSampleTile;
	public Transform transStartPointTile;
	UIGrid gridPallete;
	public Transform transSelectedBoard;

	List<PalletInfo> listBoard = new List<PalletInfo>();
	List<PalletInfo> listPallet = new List<PalletInfo>();
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
		InstancePalleteParent (PalletMode.Modify, -1, _parent, goSamplePallete, _pos);

		//EnemyList
		for(int i = 0, iMax = enemySpawnData.listEnemyKind.Count; i < iMax; i++){
			_go = InstancePalleteParent (PalletMode.Select, enemySpawnData.listEnemyKind [i].enemyKind, _parent, goSamplePallete, _pos);
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
		PalletInfo _palleteInfo = _go.GetComponent<PalletInfo>();
		if(_palleteInfo == null){
			_palleteInfo = _go.AddComponent<PalletInfo> ();
		}
		_palleteInfo.SetInit (_mode, _enemyKind);

		//리스트에 추가.
		listPallet.Add (_palleteInfo);

		return _go;
	}

	void InstancePalleteFly(GameObject _parent, GameObject _prefab, Vector3 _pos){
		GameObject _go = NGUITools.AddChild (_parent, _prefab);
		_go.transform.position = _pos;
		_go.transform.localScale = Vector3.one * 100f;

		Enemy _enemy = _go.GetComponent<Enemy> ();
		if (_enemy != null) {
			_enemy.enabled = false;
		}

		Collider _col = _go.GetComponent<Collider> ();
		if (_col != null) {
			_col.enabled = false;
		}
	}

	//---------------------------
	//선택일때 정보. (Delete, Modify, Select)
	//---------------------------
	public PalletInfo cursorPallet;
	public void InvokeSelectedPallete(PalletInfo _pallet){
		cursorPallet = _pallet;
		for (int i = 0, iMax = listPallet.Count; i < iMax; i++) {
			listPallet [i].SetUISprite (0.4f);
		}
		cursorPallet.SetUISprite (1f);
	}

	public void InvokeSelectedTile(TileInfo _tile){
		//for (int i = 0, iMax = listPallet.Count; i < iMax; i++) {
		//	listPallet [i].SetUISprite (0.4f);
		//}
		_tile.SetUISprite (1f);
	}

	//----------------------------
	//Create Tile -> spawn Point
	//----------------------------
	void CreateTile(){
		//버튼들 생성....
		GameObject _go;
		string _name;
		float disx = 140f;
		float disy = 140f;
		int xMax = 5, yMax = 5;
		Vector3 _pos = transStartPointTile.localPosition;
		UILabel _label;
		for (int x = 0; x < xMax; x++) {
			for (int y = 0; y < yMax; y++) {
				//create point
				_go = NGUITools.AddChild (gameObject, goSampleTile);
				_go.name = _name = "" + x + y;
				_go.transform.localPosition = _pos + new Vector3 (x * disx, y * disy, 0);

				//Simple한 Info Class 추가...
				PalletInfo _palleteScp = _go.GetComponent<PalletInfo>();
				if (_palleteScp == null) {
					_palleteScp = _go.AddComponent<PalletInfo> ();
				}
				//_palleteScp.InitBoardTile (_label, _name);

				//~~~~이걸로 조절.....
				listBoard.Add (_palleteScp);
			}
		}
		DestroyImmediate (goSampleTile);	
	}


	/*
	[SerializeField] List<Wave> waves = new List<Wave>();
	public void InvokeParse(){
		EnemySpawnData.ins.Parse(ref waves);
	}

	public void InvokeCurPosParsing(){
		Transform _t;
		int i, iMax;
		for (i = 0, iMax = transform.childCount; i < iMax; i++) {
			//_t = trans.GetChild (i);
			//dicSpawnPoint.Add (int.Parse (_t.name), _t);
		}
	}
	*/
}
