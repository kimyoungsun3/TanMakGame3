using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTool : MonoBehaviour {
	public GameObject memoryPrefab;
	List<Transform> list = new List<Transform>();


	void Awake(){
		//버튼들 생성....
		GameObject _go;
		UILabel _label;
		string _name;
		Vector3 _pos;
		float disx = 150f;
		float disy = 150f;
		float _px, _py;
		EnemySpawnData _data = GetComponent<EnemySpawnData> ();
		for (int x = 0; x < 5; x++) {
			for (int y = 0; y < 4; y++) {
				_go = NGUITools.AddChild (gameObject, memoryPrefab);
				_go.name = _name = "" + x + y;
				_go.GetComponentInChildren<UILabel> ().text = _name;

				_px = ((x - (5 - 1) / 2) * disx);
				_py = ((y - (4 - 0) / 2) * disy);
				_go.transform.localPosition = new Vector3 (_px, _py, 0);

				for(int i = 0, iMax = _data.listEnemyKind.Count; i < iMax; i++){
					GameObject _go2 = NGUITools.AddChild (_go, _data.listEnemyKind [i].enemyPrefab);

					_go2.GetComponent<Enemy> ().enabled = false;
					_go2.GetComponent<Collider> ().enabled = false;
					_go2.transform.localScale = Vector3.one * 100;
				}
				list.Add (_go.transform);
			}
		}
		DestroyImmediate (memoryPrefab);




		//for(int i = 0, iMax = list.Count; i < iMax; i++){
	}

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

	[System.Serializable]
	public struct ToolEnemyInfo{
		public int enemyKind;
		public Sprite enemySprite;
	}
}
