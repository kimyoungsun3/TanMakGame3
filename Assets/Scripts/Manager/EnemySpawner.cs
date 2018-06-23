using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public static EnemySpawner ins;
	public List<Transform> points = new List<Transform> ();

	int waveIndex;
	float waveNextTime;
	int waveRemainToSpawn;
	int waveRemainAlive;
	int waveCurrentCount, waveCurrentMax;
	Transform player;

	//public event System.Action<int> callbackNextWave;
	//List<LivingEntity> listAliveEnemy = new List<LivingEntity>();

	void Awake(){
		ins = this;
		//Debug.Log (this + "Awake");
	}

	void Start () {
		//Debug.Log (this + "Start");
		enabled = false;
		//Init (0);
		//NextWave ();
	}

	public void Init(int _index, Transform _player){
		//Debug.Log (this + "Init");
		enabled = true;

		//살아 있는 적들 클리어..
		waveIndex 	= _index;
		player 		= _player;

		//ClearAllEnemy ();
		//NextWave ();
	}

	void NextWave(){
		/*
		//Next wave...
		waveIndex++;
		Debug.Log ("NextWave " + waveIndex);
		if (waveIndex <= waves.Length) {
			waveCurrent = waves [waveIndex - 1];

			waveRemainToSpawn 	= waveCurrent.enemyCount;
			waveRemainAlive 	= waveCurrent.enemyCount;
			waveCurrentMax		= waveCurrent.enemyAliveMax;
			waveCurrentCount 	= 0;

			if (callbackNextWave != null) {
				callbackNextWave (waveIndex - 1);
			}
			//MapGenerator.ins.GenerateMap();

			//새로운 맵에서는 유저가 중앙에서 시작한다...
			player.position = MapGenerator.ins.GetPlyaPosition ();
		} else {
			waveIndex--;
		}
		*/
	}

	//static bool bRegisterMaterial;
	//Material spawnerSharedMaterial;
	int num = 1;
	void Update () {
		//Debug.Log (11);
		if(GameManager.ins.gamestate != GAME_STATE.Gaming)
			return;

		//Debug.Log (12);
		//Debug.Log ("----------");
		//if ( (waveRemainToSpawn > 0 || waveCurrent.bInfinite) && Time.time > waveNextTime && waveCurrentCount < waveCurrentMax) {
		if ( Time.time > waveNextTime ) {
			//Debug.Log (13);
				//스폰할때 반짝이는 부분 머티리얼 생성...
			//if (!bRegisterMaterial) {
			//	spawnerSharedMaterial = MapGenerator.ins.GetRandomOpenTile ().GetComponent<Renderer> ().sharedMaterial;
			//	PoolMaterial.ins.RegisterMaterial (
			//		spawnerSharedMaterial,
			//		Constant.SPAWNER_MATERIAL_POOL_INIT_COUNT
			//	);
			//	bRegisterMaterial = true;
			//}

			waveNextTime = Time.time + 10 * num;//waveCurrent.nextTime;
			waveRemainToSpawn--;
			waveCurrentCount++;

			StartCoroutine ("SpawnEnemy", num);
			//Transform _tileTransform = MapGenerator.ins.GetRandomOpenTile ();
			//Debug.Log ("A:" + _tileTransform.position);
			//Enemy _enemy = PoolManager2.ins.Instantiate(waveCurrent.enemy.gameObject, _tileTransform.position + Vector3.up, Quaternion.identity).GetComponent<Enemy> ();
			//Debug.Log ("B:" + _enemy.transform.position);
			//_enemy.Init (player);
			//Debug.Log ("C:" + _enemy.transform.position);
			//_enemy.callbackDeath = OnEnemyDeath;
			//Debug.Log ("D:" + _enemy.transform.position);
		}

	}

	/*
	#if UNITY_EDITOR
	public void NextSkip(){
		StopCoroutine("SpawnEnemy");

		//Tile material이 중간에 깨지는 현상이 있는데... 
		//타일이 재사용되면 문제인데...
		//타일을 맵이 변경되면 신규로 생성해서 문제 안됨...
		//단, 공유 머티리얼만 문제가 되어서 클리어 해준다....
		PoolMaterial.ins.ClearMaterila (spawnerSharedMaterial);

		ClearAllEnemy();
		NextWave();
	}
	#endif
	*/


	IEnumerator SpawnEnemy(int _num){
		//Transform _point;
		Quaternion _q = Quaternion.identity;
		WaitForSeconds w1 = new WaitForSeconds (2f);
		while (_num > 0) {
			for (int i = 0; i < points.Count; i++) {
				PoolManager.ins.Instantiate ("Boss", points[i].position, _q);
			}
			yield return w1;
		}

		/*
		//타일 정보 읽어보기...
		Transform _tileTransform = MapGenerator.ins.GetRandomOpenTile ();
		//Debug.Log (_tileTransform.position);
		Renderer _tileRenderer = _tileTransform.GetComponent<Renderer> ();
		Material _tileMaterial = _tileRenderer.sharedMaterial;

		//공용 머티리얼 정보 읽어오기...
		MaterialData _md = PoolMaterial.ins.GetMaterial (spawnerSharedMaterial);
		Material _m = _md.mat;
		_tileRenderer.sharedMaterial = _m;
		Color _c1 = _tileMaterial.color;
		Color _c2 = Color.red;
		float _spawnTimer = 0;
		float _spawnDelay = 1f;
		float _tileFlashSpeed = 4;
		_m.color = _c1;

		//Debug.Log (_c1 + ":" + _c2);
		while(_spawnTimer < _spawnDelay){
			_m.color = Color.Lerp(_c1, _c2, Mathf.PingPong(_spawnTimer * _tileFlashSpeed , 1f));
			_spawnTimer += Time.deltaTime;
			//Debug.Log ("CN:" + _num);
			yield return null;
		}
		//머터리얼 돌려주기... <- 코루틴이 깨지면 문제가 되는 부분....
		//                       (새로 생성되니까 아무 문제 안됨)...
		_tileRenderer.sharedMaterial = _tileMaterial;
		_md.Release ();

		Enemy _enemy = PoolManager2.ins.Instantiate(waveCurrent.enemy.gameObject, _tileTransform.position + Vector3.up, Quaternion.identity).GetComponent<Enemy> ();
		_enemy.Init (player, waveCurrent);
		_enemy.callbackDeath = OnEnemyDeath;

		//Enemy list를 관리하기 위해서 등록해둔다...
		listAliveEnemy.Add (_enemy);
		*/
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

	void RemoveEnemy(LivingEntity _enmey){
		//if (listAliveEnemy.Contains (_enmey)) {
		//	listAliveEnemy.Remove (_enmey);
		//}
	}

	public void ClearAllEnemy(){
		//for (int i = 0, iMax = listAliveEnemy.Count; i < iMax; i++) {
		//	listAliveEnemy [i].Destroy ();
		//}
		//listAliveEnemy.Clear ();
	}

	public void DisableControl(){
		//Debug.Log (1);
		StopAllCoroutines ();
		enabled = false;
		//Debug.Log (2);
	}
}
