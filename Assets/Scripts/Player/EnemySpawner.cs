using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public static EnemySpawner ins;
	[SerializeField] int waveIndex;
	[SerializeField] List<Wave> waves = new List<Wave>();

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
		//.Log (this + " Start");
		enabled = false;

		//Wave 정보 파싱.
		waves.Clear();
		EnemySpawnData.ins.Parse(ref waves);
		//Debug.Log(waves.Count);
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
			int _index = waveIndex;
			waveCurrent 	= waves [waveIndex];
			waveNextTime 	= Time.time + waveCurrent.spawnDelayTime;
			waveIndex++;

			if (waveIndex >= waves.Count) {
				//Debug.Log ("  > End and loop");
				bEnd = true;
				waveIndex = waves.Count - 1;
			}
			StartCoroutine (SpawnEnemy(_index, waveCurrent, bEnd));
		}
	}

	//------------------------------------
	//EnemySpawner
	//------------------------------------
	IEnumerator SpawnEnemy(int _index, Wave _wave, bool _bEnd){
		//Debug.Log (this + " SpawnEnemy");
		Quaternion _q 		= Quaternion.identity;
		int _intervalCount 	= _wave.intervalCount;
		WaitForSeconds _wait= new WaitForSeconds (waveCurrent.intervalDelayTime);
		int i, _count;
		SpawnRowData _enemyData;
		Enemy _enemyScp;

		while (_intervalCount > 0 || _bEnd) {	
			_count = _wave.listSpawnRowData.Count;
			for (i = 0; i < _count; i++) {
				_enemyData = _wave.listSpawnRowData [i];

				_enemyScp = PoolManager.ins.Instantiate (_enemyData.enemyName, _enemyData.spawnPoint.position, _q).GetComponent<Enemy>();
				_enemyScp.RestartEnemy ( _enemyData.enemyHealth, _enemyData.enemyDamage, _enemyData.enemySpeed, _enemyData.enemyAiType);

				//Enemy list를 관리하기 위해서 등록해둔다...
				//_enemyScp.callbackDeath = OnEnemyDeath;
				//listAliveEnemy.Add (_enemy);
			}
			_intervalCount--;
			yield return _wait;
		}

		//#if UNITY_EDITOR		
		//Debug.Log (_index + " > 끝");
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
