using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnData : MonoBehaviour {
	public static EnemySpawnData ins;
	public List<EnemyInfo> listEnemyKind = new List<EnemyInfo> ();
	Dictionary<int, string> dicEnemyKind = new Dictionary<int, string>();
	Dictionary<int, Transform> dicSpawnPoint = new Dictionary<int, Transform>();
	Transform trans;

	//-----------------------------
	//	Spawn Infomation
	//-----------------------------
	//intervalCount : intervalDelayTime : spawnDelayTime
	//spawnPoint : enemyKind : health : speed : damage : AItype
	[HideInInspector]string[] data = {
		//- 일자형...
		"4:1.5:-1@00:1:4:8:1:1@10:2:4:8:1:1@20:100:4:8:1:1@30:2:4:8:1:1@40:1:4:8:1:1",

		//- 나온후에 V 나오기.. 
		"4:1.5:-1@00:1:4:8:1:1@10:1:4:8:1:1@20:1:4:8:1:1@30:1:4:8:1:1@40:1:4:8:1:1",
		"4:1.5:-1@02:1:4:8:1:1@11:1:4:8:1:1@20:1:4:8:1:1@31:1:4:8:1:1@42:1:4:8:1:1",

		//- + V동시에 나오기... (시간을 좁혀서 코루틴이 2개 이상 생성되어 있다)
		"4:1.5:1@00:1:4:8:1:1@10:1:4:8:1:1@20:1:4:8:1:1@30:1:4:8:1:1@40:1:4:8:1:1",
		"4:1.5:-1@02:1:4:8:1:1@11:1:4:8:1:1@20:1:4:8:1:1@31:1:4:8:1:1@42:1:4:8:1:1",

		//갈매기형
		"4:1.5:-1@02:1:4:8:1:1@11:3:4:8:1:1@20:3:4:8:1:1@31:3:4:8:1:1@42:1:4:8:1:1",

		//갈매기형(더블)
		"4:1.5:-1@02:1:4:8:1:1@11:3:4:8:1:1@20:3:4:8:1:1@31:3:4:8:1:1@42:1:4:8:1:1@03:1:4:8:1:1@12:3:4:8:1:1@21:3:4:8:1:1@32:3:4:8:1:1@43:1:4:8:1:1",

		//역갈매기형
		"4:1.5:-1@00:1:4:8:1:1@11:1:4:8:1:1@22:1:4:8:1:1@31:1:4:8:1:1@40:1:4:8:1:1",

		//갈매기형(속도가 다른형)
		"4:1.5:-1@02:1:4:9:1:1@11:3:4:8.5:1:1@20:3:4:8:1:1@31:3:4:8.5:1:1@42:1:4:9:1:1",

		//5x4직사각각형군집...
		"4:1.5:-1@00:1:4:8:1:1@10:3:4:8:1:1@20:3:4:8:1:1@30:3:4:8:1:1@40:1:4:8:1:1@01:1:4:8:1:1@11:3:4:8:1:1@21:3:4:8:1:1@31:3:4:8:1:1@41:1:4:8:1:1@02:1:4:8:1:1@12:3:4:8:1:1@22:3:4:8:1:1@32:3:4:8:1:1@42:1:4:8:1:1@03:1:4:8:1:1@13:3:4:8:1:1@23:3:4:8:1:1@33:3:4:8:1:1@43:1:4:8:1:1",

		//1x4직사각각형군집... 시간차 공격...
		"2:1.5:1@20:1:4:8:1:1@21:3:4:8:1:1@22:3:4:8:1:1@23:3:4:8:1:1",
		"2:1.5:1@10:1:4:8:1:1@11:3:4:8:1:1@12:3:4:8:1:1@13:3:4:8:1:1",
		"2:1.5:-1@30:1:4:8:1:1@31:3:4:8:1:1@32:3:4:8:1:1@33:3:4:8:1:1",

		//다이아몬드 편대...
		"9999:1.5:-1@11:1:10:8:1:1@20:1:10:8:1:1@31:1:10:8:1:1@22:3:10:8:1:1"
	};


	void Awake(){
		//Debug.Log (this + " Awake");
		ins = this;
		trans = transform;
		int i, iMax;
		Transform _t;

		//SpawnPoint -> Dic<int, Transform>
		for (i = 0, iMax = transform.childCount; i < iMax; i++) {
			_t = trans.GetChild (i);
			dicSpawnPoint.Add (int.Parse (_t.name), _t);
		}

		//EnemyKind -> Dic<int, GameObject>
		for (i = 0, iMax = listEnemyKind.Count; i < iMax; i++) {
			dicEnemyKind.Add (listEnemyKind [i].enemyKind, listEnemyKind [i].enemyPrefab.name);
		}
	}

	//void Start(){
	//	Debug.Log (this + " Start");
	//}

	public void Parse(ref List<Wave> _list){
		Wave _w;
		for(int i = 0, iMax = data.Length; i < iMax; i++){
			_w = new Wave (i, data [i]);
			_list.Add (_w);
		}
	}

	public Transform GetSpawnPoint(int _point){
		return dicSpawnPoint [_point];
	}

	public string GetEnemyName(int _kind){
		return dicEnemyKind [_kind];
	}
}


[System.Serializable]
public class Wave{
	public int intervalCount		= 1;
	public float intervalDelayTime	= 1f;
	public float spawnDelayTime 	= 1.5f;
	public List<SpawnRowData> listSpawnRowData = new List<SpawnRowData> ();

	public Wave(int _index, string _src){	
		string[] _arr = _src.Split ('@');	

		//intervalCount : intervalDelayTime : spawnDelayTime
		//8:1.5:-1
		float[] _h = Util.ParseFloatArray (_arr [0], ':');
		intervalCount 		= (int)_h [0];
		intervalDelayTime 	= _h [1];
		spawnDelayTime 		= _h [2];
		if (spawnDelayTime < 0f) {
			spawnDelayTime = intervalCount * intervalDelayTime;
		}
		//Debug.Log (intervalCount + ":" + intervalDelayTime + ":" + spawnDelayTime);

		//spawnPoint : enemyKind : health : speed : damage : AItype
		//@00:1:1:8:1:1@10:1:1:8:1:1@20:1:1:8:1:1@30:1:1:8:1:1@40:1:1:8:1:1
		float[] _row;
		SpawnRowData _data;
		Transform _spawnPoint;
		string _enmeyName;
		float _health, _speed, _damage;
		int _ai;
		for (int i = 1, iMax = _arr.Length; i < iMax; i++) {
			//Debug.Log (i + " > " + _arr [i]);
			//00:1:1:8:1:1
			_row = Util.ParseFloatArray (_arr [i], ':');	

			_spawnPoint = EnemySpawnData.ins.GetSpawnPoint ((int)_row [0]);
			_enmeyName 	= EnemySpawnData.ins.GetEnemyName ((int)_row [1]);
			_health 	= _row [2];
			_speed 		= _row [3];
			_damage 	= _row [4];
			_ai 		= (int)_row [5];
			_data = new SpawnRowData (_spawnPoint, _enmeyName, _health, _speed, _damage, _ai);

			listSpawnRowData.Add (_data);
		}
	}
}

[System.Serializable]
public struct EnemyInfo{
	public int enemyKind;
	public GameObject enemyPrefab;
}

[System.Serializable]
public class SpawnRowData{
	public Transform spawnPoint;
	public string enemyName;
	public float enemyHealth, enemySpeed, enemyDamage;
	public int enemyAiType;

	public SpawnRowData(){
	}

	public SpawnRowData(Transform _p, string _k, float _h, float _s, float _d, int _a){
		spawnPoint 	= _p;
		enemyName 	= _k;
		enemyHealth = _h;
		enemySpeed 	= _s;
		enemyDamage = _d;
		enemyAiType = _a;
	}
}

[System.Serializable]
public class SpawnRowData2{
	public string spawnPoint;
	public int enemyKind;//enemyName;
	public float enemyHealth, enemySpeed, enemyDamage;
	public int enemyAiType;
}