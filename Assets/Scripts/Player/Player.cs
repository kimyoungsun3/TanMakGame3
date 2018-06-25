using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour, IDamageable {
	public Vector3 offset;
	public Transform[] spawn;
	public Transform bullet;
	public int weaponStep = 1;
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

	public float health;
	public bool bDeath;//player가 감지...
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

				if (Time.time > time) {
					time = Time.time + weaponDeleyTime;
					Shooting ();
				}
			}
		//}

		//#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (1)) {			
			weaponStep++;
			if (weaponStep > 6) {
				weaponStep = 1;
			}
			Debug.Log ("Editor 모드 > 무기변경 (Space)");
			#if UNITY_EDITOR
		} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Time.timeScale *= 0.5f;
			Debug.Log ("Editor 모드 > 1SpeedDown " + Time.timeScale);
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Time.timeScale = 1f;
			Debug.Log ("Editor 모드 > 2SpeedDown " + Time.timeScale);

		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
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
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [0].transform.position, spawn [0].transform.rotation);
			break;
		case 2:
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [1].transform.position, spawn [1].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [2].transform.position, spawn [2].transform.rotation);
			break;
		case 3:
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [0].transform.position, spawn [0].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [1].transform.position, spawn [1].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [2].transform.position, spawn [2].transform.rotation);
			break;
		case 4:
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [0].transform.position, spawn [0].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [1].transform.position, spawn [1].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [2].transform.position, spawn [2].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [3].transform.position, spawn [3].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [4].transform.position, spawn [4].transform.rotation);
			break;
		case 5:
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [0].transform.position, spawn [0].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [1].transform.position, spawn [1].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [2].transform.position, spawn [2].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [3].transform.position, spawn [3].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [4].transform.position, spawn [4].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [5].transform.position, spawn [5].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [6].transform.position, spawn [6].transform.rotation);
			break;
		case 6:
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [0].transform.position, spawn [0].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [1].transform.position, spawn [1].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [2].transform.position, spawn [2].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [3].transform.position, spawn [3].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [4].transform.position, spawn [4].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [5].transform.position, spawn [5].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [6].transform.position, spawn [6].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [7].transform.position, spawn [7].transform.rotation);
			PoolManager.ins.Instantiate ("PlayerBullet", spawn [8].transform.position, spawn [8].transform.rotation);
			break;
		}
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

}
