
using UnityEngine;


public class UIFallowManager : MonoBehaviour
{
	
	//public delegate void OnVisibilityChange (bool isVisible);
	//public OnVisibilityChange onChange;

	public Vector3 targetPosition;

	/// <summary>
	/// Game camera to use.
	/// </summary>

	public Camera gameCamera;

	/// <summary>
	/// UI camera to use.
	/// </summary>

	public Camera uiCamera;

	/// <summary>
	/// Whether the children will be disabled when this object is no longer visible.
	/// </summary>

	public bool disableIfInvisible = true;


	public UILabel targetLabel;
	Transform targetTrans;
	//int mIsVisible = -1;

	/// <summary>
	/// Whether the target is currently visible or not.
	/// </summary>

	//public bool isVisible { get { return mIsVisible == 1; } }

	/// <summary>
	/// Cache the transform;
	/// </summary>

	//void Awake () { mTrans = transform; }

	/// <summary>
	/// Find both the UI camera and the game camera so they can be used for the position calculations
	/// </summary>

	void Start()
	{
		gameCamera = Camera.main;
		uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
	}

	float timeShow;
	public void SetPosition(string _msg, Vector3 _worldPos, float _time){
		targetLabel.text 	= _msg;
		targetTrans 		= targetLabel.transform;
		targetPosition 		= _worldPos;
	}

	/// <summary>
	/// Update the position of the HUD object every frame such that is position correctly over top of its real world object.
	/// </summary>

	void Update ()
	{		
		if (Time.time < timeShow && uiCamera != null)
		{
			//3D World -> 3D viewport = NGUI viewport 
			//         -> NGUI 3D World 
			//		   -> 하위위치.
			Vector3 vpos 	= gameCamera.WorldToViewportPoint(targetPosition);
			Vector3 wpos 	= uiCamera.ViewportToWorldPoint(vpos);
			Vector3 lpos2 	= transform.InverseTransformPoint(wpos);
			lpos2.z = 0f;
			targetTrans.localPosition = lpos2;

			Debug.Log (this + " Update " 
				+ " targetPosition:" + targetPosition 
				+ " vpos" + vpos
				+ " wpos" + wpos
				+ " targetTrans.position" + targetTrans.position
				+ " lpos2" + lpos2
			);


			//3D World -> 3D viewport -> NGUI viewport
			//         -> NGUI 3D world
			//            .position 그대로 넣기...
			vpos = gameCamera.WorldToViewportPoint(targetPosition);
			wpos = uiCamera.ViewportToWorldPoint(vpos);
			wpos.z = transform.position.z;
			targetTrans.position = wpos;
			Debug.Log (" targetPosition:" + targetPosition 
				+ " vpos" + vpos
				+ " wpos" + wpos
			);

		}

	}
}
