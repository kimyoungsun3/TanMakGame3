using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable {
	protected Transform trans;
	public float STARTING_HEALTH;
	protected float health;
	[HideInInspector]
	public bool bDead;
	//public event System.Action callbackDeath;
	public System.Action<LivingEntity> callbackDeath;

	protected virtual void Start () {
		//Debug.Log(this + " S Start:" + transform.position);
		trans = transform;	
	}

	public virtual void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDirection){
		//Debug.Log(this + " S TakeHit:" + transform.position);
		//Hit Particle..

		//Damage...
		TakeDamage(_damage);
	}

	public virtual void TakeDamage(float _damage){
		//Debug.Log(this + " S TakeDamage:" + transform.position);
		health -= _damage;

		if (health <= 0 && !bDead) {
			Die ();
		}
	}

	protected void Die(){
		//Debug.Log(this + " S Die:" + transform.position);
		bDead = true;
		if (callbackDeath != null) {
			callbackDeath (this);
			callbackDeath = null;
		}
		Destroy ();
	}

	public virtual void Destroy(){
		//Debug.Log(this + " S Destroy:" + transform.position);
		callbackDeath = null;
		gameObject.SetActive (false);
	}
}
