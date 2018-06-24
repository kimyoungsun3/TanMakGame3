using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveTest
{
	public enum MOVE_TYPE  { UpdateType, FixedType };
    public class EnemySpawner : MonoBehaviour
	{
		float waitTime;
		public float WAIT_TIME = 2f;
        public List<Transform> spawnPoint 	= new List<Transform>();
		public List<GameObject> prefabObject = new List<GameObject>();


		public MOVE_TYPE mode = MOVE_TYPE.UpdateType;
		public float speed = 6f;
		public UILabel modeLabel, speedLabel;


		public void InvokeChangeMode(){
			switch(mode){
			case MOVE_TYPE.UpdateType:
				mode = MOVE_TYPE.FixedType;
				break;
			case MOVE_TYPE.FixedType:
				mode = MOVE_TYPE.UpdateType;
				break;
			}
			modeLabel.text = mode.ToString ();
		}

		public void InvokeChangeSpeed(){
			speed++;
			if (speed >= 10f) {
				speed = 5f;
			}

			speedLabel.text = speed.ToString ();
		}

        void Update()
        {
            if(Time.time > waitTime)
            {
				waitTime = Time.time + WAIT_TIME;
				for(int i = 0, iMax = prefabObject.Count; i < iMax; i++)
                {
					MoveTest _mt = PoolManager.ins.Instantiate (prefabObject [i], spawnPoint [i].position, Quaternion.identity).GetComponent<MoveTest>();
					_mt.MoveMove (mode, speed);
                }
            }
        }
    }
}