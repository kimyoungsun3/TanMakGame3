//#define DEBUG_VIEW
using UnityEngine;
using System.Collections;
#if DEBUG_VIEW
	using UnityEditor;
#endif

public class PlayerBullet : PoolMaster {
	public LayerMask mask;
	public float speed 	= 40f;
	Ray ray;
	RaycastHit hit;
	TrailRenderer trailRenderer;
	float moveDistance, nextTime, damage = 1f;
	Vector3 oldPosition, nextPoint;
	float skinWidth = .01f;
	public override void Awake(){
		//Debug.Log (this + "Awake");
		base.Awake ();
		trailRenderer 	= GetComponent<TrailRenderer> ();
	}

	//int nEnableCount = 0;
	void OnEnable(){
		//Debug.Log (this + "OnEnable");
		trailRenderer.Clear ();
		nextTime 	= Time.time + Constant.BULLET_ALIVE_TIME;
		damage 		= 1f;

		//소멸된 자리에서 다시 생성된다. ㅠㅠ > 충돌 > 파괴... 
		// reuse, current position > this area is boss and exist > reuse and collision => result is Expire.
		//if (nEnableCount++ > 0) {
		//	//Debug.Log (" > ");
		//	moveDistance = speed * Time.deltaTime;
		//	Collider[] _cols = Physics.OverlapSphere (trans.position, moveDistance, mask);
		//	if (_cols.Length > 0) {
		//		//Debug.Log (">" + gameObject.name + ":"  + _cols[0].gameObject.name);
		//		OnHitObject (_cols [0], trans.position);
		//	}
		//}
	}

	public void SetInit(float _damage){
		damage 		= _damage;

		moveDistance = speed * Time.deltaTime;
		Collider[] _cols = Physics.OverlapSphere (trans.position, moveDistance, mask);
		if (_cols.Length > 0) {
			OnHitObject (_cols [0], trans.position);
		}
	}

	void Update(){
		//Debug.Log (this + "Update");
		//moveDistance = speed * Time.deltaTime;
		//ray.origin = trans.position;
		//ray.direction = trans.up;

		////현시점에서 매순간 지속적인 검사를 실행한다...
		////애도 통과 된 후에는 검사 안함....
		//if (Physics.Raycast (ray, out hit, moveDistance + skinWidth, mask, QueryTriggerInteraction.Collide)) {
		//	OnHitObject (hit.collider);
		//	return;
		//}	

		moveDistance 	= speed * Time.deltaTime;
		nextPoint 		= trans.position + trans.up * (moveDistance + skinWidth);
		if(Physics.Linecast(trans.position, nextPoint, out hit, mask, QueryTriggerInteraction.Collide)){
			OnHitObject (hit.collider, hit.point);
			return;
		}		
		trans.Translate (Constant.up * moveDistance);

		if(Time.time > nextTime){
			Destroy ();
		}

		//#if DEBUG_VIEW
		//Debug.DrawRay(trans.position, trans.up * moveDistance);
		//if (Input.GetMouseButtonUp (0)) {
		//	EditorApplication.isPaused = true;
		//}
		//#endif
	}


	void OnHitObject(Collider _col, Vector3 _hitPoint){
		//Debug.Log (this + "OnHitObject" + hit.collider.gameObject.name);
		IDamageable _scp = _col.GetComponent<IDamageable>();
		if (_scp != null) {
			//_scp.TakeHit (damage, _hitPoint, trans.forward);
			_scp.TakeDamage(damage);
		}

		//EnemyHealth _scp = hit.collider.GetComponent<EnemyHealth> ();
		//if (_scp != null) {
		//	_scp.TakeDamage (damage);
		//}	

		//Sound, Particle
		PoolMaster _p = PoolManager.ins.Instantiate("EffectEnemyHit", hit.point, Quaternion.identity).GetComponent<PoolMaster>();
		_p.Play ();

		//Animation Auto > 파티클이 약간 빠름...
		//PoolReturnAnimation _a = PoolManager.ins.Instantiate ("HitEffectAnim", hit.point, Quaternion.identity).GetComponent<PoolReturnAnimation>();
		//_a.Play ("HitEffect");

		SoundManager.ins.Play ("Enemy death", false);
		Destroy ();
	}

	public override void Destroy(){
		//Debug.Log (this + "Destroy");
		trailRenderer.Clear ();
		base.Destroy ();
	}

	public void OnBecameInvisible(){
		//Debug.Log (this + "OnBecameInvisible" + go.activeSelf);
		if (go.activeSelf) {
			Destroy ();
		}
	}

	//-----------------------------
	#if DEBUG_VIEW
	void OnDrawGizmos(){
		moveDistance 	= speed * Time.deltaTime;
		Gizmos.DrawWireSphere(transform.position, moveDistance);
	}
	#endif

}