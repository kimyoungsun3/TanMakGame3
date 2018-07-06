//#define DEBUG_VIEW
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity {
	public LayerMask mask;
	[SerializeField] protected float speed 	= 2f;
	Vector3 moveDir;
	float damage = 1f;
	int ai;

	public override void Awake(){
		base.Awake ();
	}

	//-----------------------------------------
	//Enemy Spawn
	//-----------------------------------------
	public void RestartEnemy(float _health, float _damage, float _speed, int _ai){
		health 	= _health;
		damage 	= _damage;
		speed 	= _speed;
		ai 		= _ai;

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
		//	Debug.Log("Player -> Enemy -> 충돌 무효과 )");
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
	//public override void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDir){
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
	//public override void TakeDamage(float _damage){
	//	//health -= _damage;
	//	//
	//	//if (health <= 0f && !bDead) {
	//	//	Die ();
	//	//}
	//}

	protected override void Die(){
		//Debug.Log (this + " Die:" + 1);
		bDead = true;

		//Expire Effect
		PoolMaster _p = PoolManager.ins.Instantiate("EffectEnemyDeath", hitPoint, Quaternion.identity).GetComponent<PoolMaster>();
		_p.Play ();

		//Sound
		SoundManager.ins.Play ("Enemy attack", false);

		//Debug.Log (this + ":" + 1);
		//Message Show
		Ui_MsgRoot.ins.InvokeShowMessage("UiHitMessage", "[00ff00]Hit[-]", hitPoint, 5f);

		//등록된 콜백사용...
		if (callbackDeath != null) {
			callbackDeath (this);
			callbackDeath = null;
		}

		//강제로 아이템 생성...
		Debug.Log("#### 아이템 강제로 임의 생성...");
		Item _scpItem = PoolManager.ins.Instantiate("Item", hitPoint, Quaternion.identity).GetComponent<Item>();
		int _k = Random.Range (0, ((int)ITEM_KIND.PLUS_POWER) + 1);
		if(_k <= 0)
			_scpItem.InitReuse (ITEM_KIND.PLUS_BULLET, 1, 0);
		else if(_k <= 1)
			_scpItem.InitReuse (ITEM_KIND.PLUS_HEALTH, 1, 0);
		else if(_k <= 2)
			_scpItem.InitReuse (ITEM_KIND.PLUS_COIN, 1, 0);
		else
			_scpItem.InitReuse (ITEM_KIND.PLUS_POWER, 0, 0.1f);

		Destroy ();
	}

	//public virtual void Destroy(){
	//	callbackDeath = null;
	//	gameObject.SetActive (false);
	//}
}