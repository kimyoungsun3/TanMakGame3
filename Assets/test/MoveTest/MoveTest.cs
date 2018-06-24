using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveTest
{
    public class MoveTest : MonoBehaviour
    {
		public MOVE_TYPE mode = MOVE_TYPE.UpdateType;
		public float speed = 6f;
		Transform trans;
		public float limitY = -10f;

		void Start(){
			trans = transform;
		}


		public void MoveMove(MOVE_TYPE _mode, float _speed){
			mode 	= _mode;
			speed 	= _speed;
		}

        // Update is called once per frame
        void Update()
        {
			if (mode == MOVE_TYPE.UpdateType) {
				trans.Translate (Vector3.down * speed * Time.deltaTime);
				if (trans.position.y < limitY) {
					gameObject.SetActive (false);
				}
			}
        }

		void FixedUpdate(){
			if (mode == MOVE_TYPE.FixedType) {
				trans.Translate (Vector3.down * speed * Time.fixedDeltaTime);
				if (trans.position.y < limitY) {
					gameObject.SetActive (false);
				}
			}
		}
    }
}
