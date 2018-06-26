//#define DEBUG_VIEW
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity {
	public LayerMask mask;
	[SerializeField] protected float speed 	= 2f;
	Vector3 moveDir;
	float damage = 1f;

	public override void Awake(){
		base.Awake ();
	}

	//-----------------------------------------
	//Enemy Spawn
	//-----------------------------------------
	public void RestartEnemy(float _health, float _damage, float _speed){
		health 	= _health;
		damage 	= _damage;
		speed 	= _speed;

		bDead 	= false;
	}

	//-----------------------------------------
	//Enemy Action
	//-----------------------------------------
	void Update(){
		trans.Translate (-Constant.up * speed * Time.deltaTime);
	}

	//-----------------------------------------
	//Enemy -> Player Collision
	//-----------------------------------------
	void OnTriggerEnter(Collider _col){
		//Debug.Log (this + "OnTriggerEnter "+_col.tag);
		//if (_col.CompareTag ("Player")) {
		//	//Player -> Enemy ( 충돌 )
		//	Debug.Log("Player -> Enemy ( 충돌, HP-- )");
		//	TakeDamage(1);
		//} else 
		if (_col.CompareTag ("Wall")) {
			//Pool에 린턴...
			//Debug.Log(" > wall");
			Destroy ();
		}
	}

	//---------------------------------------
	//Player -> PlayerBullet -> Enemy
	//---------------------------------------
	//public virtual void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDir){
	//	health 		-= _damage;
	//	hitPoint 	= _hitPoint;
	//	hitDir 		= _hitDir;
	//
	//	if (health <= 0f && !bDead) {
	//		Die ();
	//	}
	//}

	//총알 		-> Enemy (Ray)
	//Player 	-> Enemy (no Damage)?
	//public virtual void TakeDamage(float _damage){
	//	//health -= _damage;
	//	//
	//	//if (health <= 0f && !bDead) {
	//	//	Die ();
	//	//}
	//}

	protected void Die(){
		bDead = true;

		//Expire Effect
		PoolMaster _p = PoolManager.ins.Instantiate("EffectEnemyDeath", hitPoint, Quaternion.identity).GetComponent<PoolMaster>();
		_p.Play ();

		//Sound
		SoundManager.ins.Play ("Enemy attack", false);

		//Message Show
		Ui_MsgRoot.ins.InvokeShowMessage("UiHitMessage", "[00ff00]Hit[-]", hitPoint, 5f);

		//등록된 콜백사용...
		if (callbackDeath != null) {
			callbackDeath (this);
			callbackDeath = null;
		}

		Destroy ();
	}

	//public virtual void Destroy(){
	//	callbackDeath = null;
	//	gameObject.SetActive (false);
	//}
}