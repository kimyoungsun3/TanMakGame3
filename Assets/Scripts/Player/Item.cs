using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PoolMaster {
	public ITEM_KIND kind = ITEM_KIND.PLUS_BULLET;
	public int count = 1;
	public float power = .5f;
	public Sprite[] sprites;
	SpriteRenderer renderer;

	//떨어지는 관련 변수....
	public float maxJumpHeight 	= 3.5f;	//최대높이.
	public float minJumpHeight 	= .7f;	//최소높이.
	public float timeToJumpApex = .35f;	//체공시간 -> 중력, 점프중력.
	float gravity, maxJumpVelocity, minJumpVelocity;
	public float discountValue = 4f;
	public float yMaxSpeed = -20f;
	Vector3 velocity;

	//--------------------------------
	public override void Awake(){
		base.Awake ();
		renderer = GetComponent<SpriteRenderer> ();


		gravity 		= -((2 * maxJumpHeight) / (timeToJumpApex * timeToJumpApex));
		gravity 		= gravity / discountValue;
		maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs(gravity) * minJumpHeight);

		//Debug.Log (gravity + ":" + maxJumpVelocity + ":" + minJumpVelocity);
	}


	//--------------------------------
	// temporary Start()
	//--------------------------------
	//void Start (){
	//	InitReuse (ITEM_KIND.PLUS_BULLET, 1, 0);
	//}

	public void InitReuse(ITEM_KIND _k, int _count, float _power){
		//Debug.Log ((int)kind);
		kind 	= _k;
		count 	= _count;
		power 	= _power;
		renderer.sprite = sprites [(int)kind];

		velocity.Set ( 
			Random.Range (-1f, 1f), //좌우 약간 튀게..
			maxJumpVelocity, 		//떨어지게...
			0f);
	}

	//--------------------------------
	// 
	//--------------------------------
	void Update(){
		//중력에 의해서 떨어진다... > 너무 빨리 떨어져서 강제로 잡아준다...
		velocity.y += gravity * Time.deltaTime;
		if (velocity.y < yMaxSpeed) {
			velocity.y = yMaxSpeed;
		}
		//Debug.Log (gravity + ":" + velocity.y);
		trans.Translate (velocity * Time.deltaTime, Space.World);
	}

	//-----------------------------------------
	//Item -> Player Collision
	//-----------------------------------------
	void OnTriggerEnter(Collider _col){
		if (_col.CompareTag ("Player")) {
			//item hit -> Player Setting item...
			//Debug.Log (" > item Player ");
			Player _scp = _col.GetComponent<Player> ();
			if (_scp != null) {
				//Debug.Log (" > item Player > Give");
				_scp.TakeSetItem (kind, count, power);
			}

			//Destroy
			Destroy ();
		}else if (_col.CompareTag ("Wall")) {
			//Pool에 린턴...

			//Debug.Log(" > item wall > pool return");
			Destroy ();
		}
	}
}
