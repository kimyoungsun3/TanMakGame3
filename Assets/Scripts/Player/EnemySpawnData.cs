using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnData : MonoBehaviour {
	public static EnemySpawnData ins;
	public List<EnemyInfo> listEnemyKind = new List<EnemyInfo> ();
	Dictionary<int, string> dicEnemyName = new Dictionary<int, string>();
	Dictionary<int, Transform> dicSpawnPoint = new Dictionary<int, Transform>();
	Transform trans;

    //-----------------------------
    //	Spawn Infomation
    //-----------------------------
    //intervalCount : intervalDelayTime : spawnDelayTime
    //spawnPoint : enemyNum : health : speed : damage : AItype
    [HideInInspector] string[] data = {
        "10:4:11@01:1:2:7:1:1@11:1:2:7:1:1@21:1:2:7:1:1@31:1:2:7:1:1@41:1:2:7:1:1",
    "2:2:6@00:1:2:7:1:1@02:3:2:7:1:1@03:3:2:7:1:1@10:1:2:7:1:1@11:3:2:7:1:1@14:3:2:7:1:1@20:1:2:7:1:1@21:3:2:7:1:1@24:3:2:7:1:1@30:1:2:7:1:1@31:3:2:7:1:1@34:3:2:7:1:1@40:1:2:7:1:1@42:3:2:7:1:1@43:3:2:7:1:1",
    "70:4:6@01:1:120:4:1:1@10:1:12:4:1:1@20:1:12:4:1:1@30:1:12:4:1:1@41:1:12:4:1:1",
    "20:2:0.7@02:1:3:5:1:1@04:1:3:5:1:1@11:1:3:5:1:1@13:1:3:5:1:1@20:1:3:5:1:1@22:1:3:5:1:1@31:1:3:5:1:1@33:1:3:5:1:1@42:1:3:5:1:1@44:1:3:5:1:1",
    "20:2:-1@04:3:3:5:1:1@11:3:3:5:1:1@12:3:3:5:1:1@13:3:3:5:1:1@20:3:3:5:1:1@31:3:3:5:1:1@32:3:3:5:1:1@33:3:3:5:1:1@44:3:3:5:1:1", 
    
    "6:1.5:-1@03:2:3:5:1:1@12:2:3:6:1:1@21:2:3:7:1:1@32:2:3:6:1:1@43:2:3:5:1:1",
    "4:.5:-1@00:1:1:6:1:1@10:1:1:6:1:1@20:1:1:6:1:1@30:1:1:6:1:1@40:1:1:6:1:1",
    "2:2.5:-1@02:1:1:6:1:1@11:1:1:6:1:1@12:2:1:6:1:1@20:1:1:6:1:1@21:3:1:6:1:1@31:1:1:6:1:1@32:2:1:6:1:1@42:1:1:6:1:1",
    "10:2.5:-1@02:1:1:6:1:1@11:1:1:6:1:1@20:1:1:6:1:1@31:1:1:6:1:1@42:1:1:6:1:1",
    "10:2.5:-1@01:1:1:6:1:1@02:1:1:6:1:1@03:1:1:6:1:1@10:1:1:6:1:1@12:3:1:6:1:1@14:1:1:6:1:1@20:1:1:6:1:1@21:3:1:6:1:1@22:2:1:6:1:1@23:3:1:6:1:1@24:1:1:6:1:1@30:1:1:6:1:1@32:3:1:6:1:1@34:1:1:6:1:1@41:1:1:6:1:1@42:1:1:6:1:1@43:1:1:6:1:1",
    "10:1:-1@00:1:6:7:1:1@10:1:5:6:1:1@20:1:4:5:1:1@30:1:5:4:1:1@40:1:5:3:1:1",
    "10:1:-1@03:2:20:6:1:1@11:2:20:6:1:1@12:2:20:6:1:1@14:2:20:6:1:1@20:2:20:6:1:1@23:2:20:6:1:1@31:2:20:6:1:1@32:2:20:6:1:1@34:2:20:6:1:1@43:2:20:6:1:1", 
        //갈매기 속도변형...
        "100:.3:-1@02:1:5:10:1:1@11:1:5:9:1:1@20:1:5:8:1:1@31:1:5:9:1:1@42:1:5:10:1:1",
        "10:2:-1@02:1:5:10:1:1@11:1:5:9:1:1@20:1:5:8:1:1@31:1:5:9:1:1@42:1:5:10:1:1",

        //다이아몬드.
        "10:2:-1@02:1:9:8:1:1@11:1:9:8:1:1@13:1:9:8:1:1@20:1:9:8:1:1@24:1:9:8:1:1@31:1:9:8:1:1@33:1:9:8:1:1@42:1:9:8:1:1",
	    "5:1:-1@00:1:5:8:1:1@10:3:5:8:1:1@20:100:5:8:1:1@30:3:5:8:1:1@40:1:5:8:1:1",
		//- 일자형..."
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
			dicEnemyName.Add (listEnemyKind [i].enemyNum, listEnemyKind [i].enemyPrefab.name);
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

	public string GetEnemyName(int _num){
		return dicEnemyName [_num];
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
	public int enemyNum;
	public GameObject enemyPrefab;
}

[System.Serializable]
public class SpawnRowData{
	public Transform spawnPoint;		//00 01 02  <- Transform
	public string enemyName;			//Enemy1, Enemy2
	public float enemyHealth, enemySpeed, enemyDamage;
	public int enemyAiType;				//AiType int 

	public SpawnRowData(){
	}

	public SpawnRowData(Transform _p, string _k, float _h, float _s, float _d, int _a){
		spawnPoint 	= _p;
		enemyName	= _k;
		enemyHealth = _h;
		enemySpeed 	= _s;
		enemyDamage = _d;
		enemyAiType = _a;
	}

	//Tool에 사용되는것....
	public bool bSelect;
	public string spawnPointStr;
	public int enemyNum;
}
