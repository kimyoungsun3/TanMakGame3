using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour, IDamageable {
	public Vector3 offset;
	public List<Transform> spawns 	= new List<Transform>();
	List<Vector3> spawnsDirV 		= new List<Vector3>();
	List<Quaternion> spawnsDirQ		= new List<Quaternion>();

	public Transform bullet;
	[Range(1, 6)] public int weaponStep = 1;
	public float weaponDeleyTime = 0.02f;
	public SpriteRenderer playerSprite;
	public Collider playerCollider;

	Transform trans;
	Plane plane;
	float z, distance, time, timeMujuk;
	Ray ray;
	RaycastHit hit;
	Vector3 hitPoint;
	Camera cam;

	[Header("유저정보")]
	[HideInInspector] public float health;
	[HideInInspector]public bool bDeath;//player가 감지...
	public float power = 1f;
	bool bMujuk;

	[Header("상수")]
	public float MUJUK_CYCLE_VAR = 25f;
	public float MUJUK_TIME = 2f;
	public float HEALT_MAX = 3f;


	//---------------------------
	void Start () {
		trans 	= transform;
		cam 	= Camera.main;
		z 		= trans.position.z;
		plane 	= new Plane (Camera.main.transform.rotation * Vector3.back, trans.position);
		time 	= Time.time;

		//발사구멍에 위치정보로 저장...
		for (int i = 0, iMax = spawns.Count; i < iMax; i++) {
			spawnsDirV.Add( spawns [i].position - trans.position );
			spawnsDirQ.Add( spawns [i].rotation );
		}
	}

	public void Init(){
		enabled 				= true;
		playerSprite.enabled 	= true;
		playerCollider.enabled 	= true;

		health 	= HEALT_MAX;
		bDeath 	= false;
		bMujuk 	= false;
		timeMujuk = 0;
	}

	//Gaming
	//void Update(){
	void Update(){
		//GameManager.ins.gamestate == GAME_STATE.Gaming
		//if (Input.GetMouseButton (0)) {

		ray = cam.ScreenPointToRay (Input.mousePosition);
		if (plane.Raycast (ray, out distance)) {
			hitPoint = ray.GetPoint (distance);
			hitPoint.z = z;
			hitPoint += offset;
			trans.position = hitPoint;

			//Clocking....
			#if UNITY_EDITOR   		
			if(debugIsClocking){
				trans.position = debugClockPos;
			}
			#endif

			if (Time.time > time) {
				time = Time.time + weaponDeleyTime;
				Shooting ();
			}
		}



		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (1)) {			
			weaponStep++;
			if (weaponStep > 6) {
				weaponStep = 1;
			}
			Debug.Log ("Editor 모드 > 무기변경 (Space)");
			#if UNITY_EDITOR
		} else if(Input.GetKeyDown(KeyCode.C)){
			//Clocking....
			debugIsClocking = !debugIsClocking;
			debugClockPos = trans.position;
		} else if (Input.GetKeyDown (KeyCode.Z)) {
			Time.timeScale *= 0.5f;
			Debug.Log ("Editor 모드 > 1SpeedDown " + Time.timeScale);
		} else if (Input.GetKeyDown (KeyCode.X)) {
			Time.timeScale = 1f;
			Debug.Log ("Editor 모드 > 2SpeedDown " + Time.timeScale);
		} else if (Input.GetKeyDown (KeyCode.P)) {
			EditorApplication.isPaused = !EditorApplication.isPaused;
			Debug.Log ("Editor 모드 > 3SpeedDown " + Time.timeScale);
			#endif
		}

		//Debug.Log ((Mathf.Sin (Time.time) < 0 ? -1:1) +":" +(Mathf.Sin (Time.time*MUJUK_CYCLE_VAR) < 0 ? -1:1));
		//데미지 상태에서 깜박이기...
		if(bMujuk){
			if (Time.time < timeMujuk) {
				if (Mathf.Sin (Time.time * MUJUK_CYCLE_VAR) < 0f) {
					playerSprite.enabled = true;
				} else {
					playerSprite.enabled = false;
				}
			} else {
				bMujuk 					= false;
				playerSprite.enabled 	= true;
				playerCollider.enabled 	= true;
			}
		}
	}

	void Shooting(){
		//Debug.Log(time + ":" + Time.time + ":" + (time > Time.time));
		//SoundManager.ins.Play ("Gun shoot", false);
		switch (weaponStep) {
		case 1:
			Shoot(0);
			break;
		case 2:
			Shoot(1);
			Shoot(2);
			break;
		case 3:
			Shoot(0);
			Shoot(1);
			Shoot(2);
			break;
		case 4:
			Shoot(0);
			Shoot(1);
			Shoot(2);
			Shoot(3);
			Shoot(4);
			break;
		case 5:
			Shoot(0);
			Shoot(1);
			Shoot(2);
			Shoot(3);
			Shoot(4);
			Shoot(5);
			Shoot(6);
			break;
		case 6:

			Shoot(0);
			Shoot(1);
			Shoot(2);
			Shoot(3);
			Shoot(4);
			Shoot(5);
			Shoot(6);
			Shoot(7);
			Shoot(8);
			break;
		}
	}

	//쏘고 총알 세팅...
	void Shoot(int _idx){
		PlayerBullet _scp = PoolManager.ins.Instantiate ("PlayerBullet", trans.position + spawnsDirV [_idx], spawnsDirQ [_idx]).GetComponent<PlayerBullet>();
		_scp.SetInit (power);
	}

	void OnTriggerEnter(Collider _col){
		//Debug.Log (this + " OnTriggerEnter " + _col.name);
		if (_col.CompareTag ("EnemyBullet")) {
			Debug.Log (this + " #### EnemyBullet > Player OnTriggerEnter");
		}else if(_col.CompareTag ("Enemy")) {
			//Debug.Log (this + " Enemy -> Player OnTriggerEnter");
			//Player - Enemy 충돌...
			//Player > 깜박,  에너지감소
			TakeDamage(1);

			//Enemy  > 에너지 감소.
			_col.GetComponent<Enemy>().TakeDamage(1);

			//Shake
			CameraShake2D.ins.Shaking();

		}else if(_col.CompareTag ("Item")) {
			Debug.Log (this + " #### Item > Player OnTriggerEnter");
		}
	}

	public void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDirection){

	}

	//Boss 			-> Player
	//Enemy			-> Player
	//EnemyBullet 	-> Player
	public void TakeDamage(float _damage){
		//데미지, 깜박이기, Collider off(잠시무적)
		health -= _damage;

		bMujuk 		= true;
		timeMujuk 	= Time.time + MUJUK_TIME;
		playerSprite.enabled = false;
		playerCollider.enabled = false;
		//Debug.Log (health + ":" + bDeath);

		if (health <= 0 && !bDeath) {
			Dead ();
		}
	}

	void Dead(){	
		//폭발 Effect, Sound
		PoolMaster _p = PoolManager.ins.Instantiate("EffectPlayerDeath", trans.position, Quaternion.identity).GetComponent<PoolMaster>();
		_p.Play ();

		//Animation Auto > 파티클이 약간 빠름...
		//PoolReturnAnimation _a = PoolManager.ins.Instantiate ("HitEffectAnim", hit.point, Quaternion.identity).GetComponent<PoolReturnAnimation>();
		//_a.Play ("HitEffect");

		SoundManager.ins.Play ("Player death", false);

		bDeath = true;
	}

	public void DisableControl(){
		playerSprite.enabled 	= false;
		playerCollider.enabled 	= false;

		enabled 				= false;
	}


	#if UNITY_EDITOR
	bool debugIsClocking = false;
	Vector3 debugClockPos;
	#endif
}
