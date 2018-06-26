using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[SerializeField] int waveIndex;
	public static EnemySpawner ins;
	public List<Transform> spawnPoints = new List<Transform> ();
	public List<EnemyInfo> enemyInfos = new List<EnemyInfo> ();
	Dictionary<int, GameObject> enemyDic = new Dictionary<int, GameObject>();
	public List<Wave> waves = new List<Wave>();

	Transform player;
	Wave waveCurrent;
	float waveNextTime;
	bool bEnd;

	//------------------------------------
	//	
	//------------------------------------
	void Awake(){
		//Debug.Log (this + " Awake");
		ins 	= this;
	}

	void Start(){
		//Debug.Log (this + " Start");
		enabled = false;

		ParsingData ();
	}

	void ParsingData(){
		//Debug.Log (this + " ParsingData");
	
		//1 - Enemey1 Prefab Parseing
		int i, iMax, j;
		for (i = 0, iMax = enemyInfos.Count; i < iMax; i++) {
			//Debug.Log ("Dic[" + i +"]" + enemyInfos [i].enemyKind + ":" + enemyInfos [i].enemyGameObject.name);
			enemyDic.Add (enemyInfos [i].enemyKind, enemyInfos [i].enemyGameObject);
		}
		//Debug.Log (enemyDic [100]);

		//Wave 정보 파싱   string -> Vector5, 적군 Prefab있는가 검사..
		for (i = 0, iMax = waves.Count; i < iMax; i++) {
			waves [i].Parse (i, enemyDic);
		}
	}

	//------------------------------------
	//GameManager -> EnemySpawner 
	//------------------------------------
	public void EnableControl(int _index, Transform _player){
		//Debug.Log (this + " EnableControl");
		enabled 	= true;

		//살아 있는 적들 클리어..
		waveIndex 	= _index;
		player 		= _player;
		bEnd 		= false;
		NextWave ();
	}

	public void DisableControl(){
		//Debug.Log (this + " DisableControl");
		StopAllCoroutines ();
		enabled = false;
	}

	//------------------------------------
	//EnemySpawner
	//------------------------------------
	void NextWave(){
		//Debug.Log (this + " NextWave " + waveIndex + ":" + waves.Count + ":" + bEnd);
		waveNextTime 	= 0;
	}

	void Update () {
		if(GameManager.ins.gamestate != GAME_STATE.Gaming)
			return;
		
		if ( Time.time > waveNextTime && !bEnd){
			//Debug.Log (" > StartCoroutine " + waveIndex);
			waveCurrent 	= waves [waveIndex];
			waveNextTime 	= Time.time + waveCurrent.spawnDelayTime;
			waveIndex++;

			if (waveIndex >= waves.Count) {
				//Debug.Log ("  > End and loop");
				bEnd = true;
				waveIndex = waves.Count - 1;
			}
			StartCoroutine (SpawnEnemy(waveCurrent, bEnd));
		}
	}

	//------------------------------------
	//EnemySpawner
	//------------------------------------
	IEnumerator SpawnEnemy(Wave _wave, bool _bEnd){
		//Debug.Log (this + " SpawnEnemy");
		Quaternion _q 		= Quaternion.identity;
		int _intervalCount 	= _wave.intervalCount;
		WaitForSeconds _w 	= new WaitForSeconds (waveCurrent.intervalDelayTime);
		int _kind, i;
		Enemy _enemy;
		int _count = 5;

		while (_intervalCount > 0 || _bEnd) {	
			//#if UNITY_EDITOR		
			//Debug.Log(_wave.debugIndex + ":" + _intervalCount);
			//_wave.DisplayParseInfo ();
			//#endif

			for (i = 0; i < _count; i++) {
				_kind = _wave.enemyKindVal[i];
				_enemy = PoolManager.ins.Instantiate (enemyDic[_kind], spawnPoints[i].position, _q).GetComponent<Enemy>();
				_enemy.RestartEnemy ( _wave.enemyHealthVal[i], _wave.enemyDamageVal[i], _wave.enemySpeedVal[i]);

				//_enemy.callbackDeath = OnEnemyDeath;
				//Enemy list를 관리하기 위해서 등록해둔다...
				//listAliveEnemy.Add (_enemy);
			}
			_intervalCount--;
			yield return _w;
		}

		//#if UNITY_EDITOR		
		//Debug.Log (_wave.debugIndex + " > 끝");
		//#endif
	}

	void OnEnemyDeath(LivingEntity _enemy){
		////die enemy...
		//RemoveEnemy (_enemy);
		//waveRemainAlive--;
		//waveCurrentCount--;
		//
		////all clear and next wave...
		//if (waveRemainAlive <= 0) {
		//	NextWave ();
		//}
	}



	public void NextSkip(){
		StopAllCoroutines ();
		NextWave();
	}
}
