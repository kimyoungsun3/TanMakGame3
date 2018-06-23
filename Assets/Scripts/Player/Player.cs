using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	void Update(){
		//GameManager.ins.gamestate == GAME_STATE.Gaming

		if (Input.GetMouseButton (0)) {
			ray = cam.ScreenPointToRay (Input.mousePosition);
			if (plane.Raycast (ray, out distance)) {
				hitPoint = ray.GetPoint (distance);
				hitPoint.z = z;
				hitPoint += offset;
				trans.position = hitPoint;

				Shooting ();
			}
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
		if (Time.time > time) {
			time = Time.time + weaponDeleyTime;
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
			case 5:
				PoolManager.ins.Instantiate ("PlayerBullet", spawn [0].transform.position, spawn [0].transform.rotation);
				PoolManager.ins.Instantiate ("PlayerBullet", spawn [1].transform.position, spawn [1].transform.rotation);
				PoolManager.ins.Instantiate ("PlayerBullet", spawn [2].transform.position, spawn [2].transform.rotation);
				PoolManager.ins.Instantiate ("PlayerBullet", spawn [1].transform.position+ Vector3.left, spawn [1].transform.rotation);
				PoolManager.ins.Instantiate ("PlayerBullet", spawn [2].transform.position+ Vector3.right, spawn [2].transform.rotation);
				break;
			}
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
