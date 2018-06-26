using UnityEngine;
using System.Collections;

//--------------------------------------
//> 0 : 지정된 시간.
// -1 : 파티클 시간.
//Quaternion _r = Quaternion.FromToRotation(Vector3.forward, -transform.forward);
//_p = PoolManager.ins.Instantiate("HitParticles", _hit.point, _r).GetComponent<ParticleSystem>();
//_p.Stop ();
//_p.Play ();
//--------------------------------------

[RequireComponent(typeof(ParticleSystem))]
public class PoolReturnParticleSystem : PoolMaster {
	ParticleSystem particle;
	public float duration = -1;

	public override void Awake(){
		base.Awake ();

		particle = GetComponent<ParticleSystem> ();
		if (duration <= 0) {
			duration = particle.main.startLifetimeMultiplier;
		}
		//Debug.Log (duration);
	}

	void OnEnable(){
		CancelInvoke ();
		Invoke ("Destroy", duration);
	}

	void OnDisalbe(){
		CancelInvoke ();
	}

	public void Play(){
		CancelInvoke ();
		Invoke ("Destroy", duration);

		particle.Stop ();
		particle.Play ();
	}
		
	public override void Destroy(){
		if (go.activeSelf) {
			base.Destroy ();
		}
	}

}
