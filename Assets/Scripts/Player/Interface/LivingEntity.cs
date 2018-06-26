using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : PoolMaster, IDamageable {
	//public float STARTING_HEALTH = 3;
	[SerializeField] protected float health;
	protected bool bDead;
	public System.Action<LivingEntity> callbackDeath;
	protected Vector3 hitPoint, hitDir;

	public virtual void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDir){
		health 		-= _damage;
		hitPoint 	= _hitPoint;
		hitDir 		= _hitDir;

		if (health <= 0f && !bDead) {
			Die ();
		}
	}

	public virtual void TakeDamage(float _damage){
		//health -= _damage;
		//
		//if (health <= 0f && !bDead) {
		//	Die ();
		//}
	}

	protected virtual void Die(){
		bDead = true;
		if (callbackDeath != null) {
			callbackDeath (this);
			callbackDeath = null;
		}
		Destroy ();
	}

	public virtual void Destroy(){
		callbackDeath = null;
		gameObject.SetActive (false);
	}
}
