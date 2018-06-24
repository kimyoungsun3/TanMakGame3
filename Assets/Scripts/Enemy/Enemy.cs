//#define DEBUG_VIEW
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolMaster, IDamageable {
	public float health = 3f;
	public LayerMask mask;
	public float speed 	= 2f;
	Vector3 moveDir;
	float damage = 1;
	bool bDead = false;
	//Vector3 hitPoint, hitDir;
	public override void Awake(){
		//Debug.Log (this + "Awake");
		base.Awake ();
	}

	//int nEnableCount = 0;
	void OnEnable(){
		//Debug.Log (this + "OnEnable");
		damage 	= 1f;
		bDead 	= false;
	}

	//public void SetInit(float _damage){
	//	damage 		= _damage;
	//}

	void FixedUpdate(){
		trans.Translate (-Constant.up * speed * Time.deltaTime);
	}

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

	public void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDirection){
		//Debug.Log ("Enemy 데미지");
		health 		-= _damage;

		if (!bDead && health <= 0f) {
			Die (_hitPoint);
		}
	}

	//총알 		-> Enemy (Ray)
	//Player 	-> Enemy (no Damage)?
	public void TakeDamage(float _damage){
		//Debug.Log ("Enemy 데미지");
		//health -= _damage;

		//if (!bDead && health <= 0f) {
		//	Die ();
		//}
	}

	void Die(Vector3 _hitPoint){
		bDead = true;

		//Expire Effect
		PoolMaster _p = PoolManager.ins.Instantiate("EffectEnemyDeath", _hitPoint, Quaternion.identity).GetComponent<PoolMaster>();
		_p.Play ();

		//Sound
		SoundManager.ins.Play ("Enemy attack", false);

		//Message Show
		Ui_MsgRoot.ins.InvokeShowMessage("UiHitMessage", "[00ff00]Hit[-]", _hitPoint, 5f);

		Destroy ();
	}

	public override void Destroy(){
		//Debug.Log (this + "Destroy");
		base.Destroy ();
	}

	//안보이는 곳에서 생성해서 올 수있다... > 사용불가...
	//public void OnBecameInvisible(){
	//	//Debug.Log (this + "OnBecameInvisible" + go.activeSelf);
	//	if (go.activeSelf) {
	//		Destroy ();
	//	}
	//}

}