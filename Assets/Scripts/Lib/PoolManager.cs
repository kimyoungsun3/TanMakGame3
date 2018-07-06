/*
 * 2018.6.01 영선 ( kyssmart@naver.com )....
 * 
 * [미처리 내용입니다.]
 * - 생성을 처음에 개별로 지정하기. > OK
 * - 이름으로 관리가 아니라 GameObject로 관리. > OK
 * - 생성관리를 환영큐.. 
 *   > 이것은 약간 front 는 이상 없는데.... rear부분에 각 오브젝트에서 이동 해줘야 하는 문제가 있어 보류.
 *   > front 만 인식하는 형태로 가면 될듯.... 
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PoolManager : MonoBehaviour {
	[Header("메모리 프리펩")]
	[Tooltip("메모리에 생성후에 메모리에 있는 프리펩을 비활성화 한다.")]
	public Transform transMemoryPrefabRoot;

	[System.Serializable]
	public class GameObjectInfo{
		public GameObject prefab;
		public int count = 10;
		public bool bUI = false;
		public Transform parent;
		//public int index = 0;
	}
	//[System.Serializable]
	public class GameObjectData{
		public List<GameObject> list;
		public int front 	= 0;
		public int max 		= 0;
		//public List<PoolMaster> listScp;

		public GameObjectData(List<GameObject> _list, int _max){
			list 	= _list;
			max 	= _max;
			//listScp = _listScp;
		}
	}

	public static PoolManager ins;
	[Header("풀링정보")]
	public List<GameObjectInfo> objList = new List<GameObjectInfo>();
	public bool willGrow = true;

	//1. 첫번째 Prefab 파일. 
	//2. 두번째 GameObject는 Memory GameObject
	Dictionary<GameObject, GameObjectData> poolList = new Dictionary<GameObject, GameObjectData>();
	Dictionary<int, GameObject> poolListName 		= new Dictionary<int, GameObject>();

	void Awake(){
		if (ins == null) {
			ins = this;
		}

		init ();
		ReleaseMemoryPrefab ();
	}

	//-----------------------------------------
	private void ReleaseMemoryPrefab(){
		Transform _t = transMemoryPrefabRoot;
		GameObject _g;
		for (int i = 0, iMax = _t.childCount; i < iMax; i++) {
			_g = _t.GetChild (i).gameObject;
			if (_g.activeSelf) {
				_g.SetActive (false);
			}
		}		
	}

	private void init(){
		GameObject _go, _prefab;
		int _len = objList.Count;
		List<GameObject> _list;
		GameObjectData _dataList;
		Transform _parent;
		bool _bUI;
		int _count, i, j; 
		for (j = 0; j < _len; j++) {
			_prefab = objList [j].prefab;
			_count 	= objList [j].count;
			_parent = objList [j].parent;
			_bUI	= objList [j].bUI;
			if (_parent == null) {
				_parent = transform;
				objList [j].parent = transform;
			}

			if (_count <= 0) {
				_count = 1;
			}
			//Debug.Log (_prefab.name + ":" + _prefab.name.IndexOf ("Ui") + ":" +_prefab.name.Contains("Ui") + ":" + _prefab.name.Contains("UI"));
			if (poolList.ContainsKey (_prefab)) {
				Debug.LogWarning ((j + 1) + "번째 풀링 프리펩이 동일 GameObject 풀링 : " + _prefab);
				continue;
			} else if (_prefab == null) {
				Debug.LogWarning ((j + 1) + "번째 풀링 프리펩이 활당되지 않음.");
				continue;
			}else if(_bUI && objList [j].parent == null){
				Debug.LogWarning ((j + 1) + "번째 풀링 "+_prefab.name+"는 UiRoot밑에 Ui_MsgRoot가 있는 곳에 넣어 주셔야 합니다.");
				continue;
			}

			_list 		= new List<GameObject> ();
			_dataList 	= new GameObjectData (_list, _count);
			poolList.Add(_prefab, _dataList);
			poolListName.Add (_prefab.name.GetHashCode(), _prefab);
			for (i = 0; i < _count; i++) {
				//Debug.Log ("create > c");
				_go = Instantiate (_prefab) as GameObject;
				_go.transform.SetParent (_parent);
				//Debug.Log ("create > t");
				_go.SetActive (false);
				//Debug.Log ("create > f");
				_list.Add (_go);
				_go.name += i.ToString ();
				//NGUITools.AddChild (_prefab);
				if (_bUI) {
					_go.transform.localScale = Vector3.one;
				}
			}
		}
	}

	//-----------------------------------------
	public GameObject Instantiate(string _name, Vector3 _pos, Quaternion _qua){
		if (!poolListName.ContainsKey (_name.GetHashCode())) {
			Debug.LogError ("풀링에 없음 _name[" + _name + "]");
			return null;
		}

		GameObject _rtnObject = InstantiateInner (poolListName[_name.GetHashCode()], _pos, _qua);
		//_rtnObject.transform.position = _pos;
		//_rtnObject.transform.rotation = _qua;

		return _rtnObject;
	}

	public GameObject Instantiate(GameObject _go, Vector3 _pos, Quaternion _qua){
		GameObject _rtnObject = InstantiateInner (_go, _pos, _qua);
		//_rtnObject.transform.position = _pos;
		//_rtnObject.transform.rotation = _qua;

		return _rtnObject;
	}

	private GameObject InstantiateInner(GameObject _prefab, Vector3 _pos, Quaternion _qua){
		if (!poolList.ContainsKey (_prefab)) {
			Debug.LogError ("풀링에 없음 _name[" + _prefab.name + "]");
			return null;
		}

		GameObject _rtn = null;
		GameObjectData _dataList 	= poolList [_prefab];
		List<GameObject> _list 		= _dataList.list;
		if (!_list [_dataList.front].activeInHierarchy) {
			//Not use gameobject > return data
			_rtn = _list [_dataList.front];
			//Debug.Log ("used > f");
			_rtn.transform.position = _pos;	//순서가 중요.
			_rtn.transform.rotation = _qua;
			_rtn.SetActive (true);
			//Debug.Log ("used > t");

			_dataList.front++;
			if (_dataList.front >= _dataList.max) {
				_dataList.front = 0;
			}
		} else if (willGrow) {
			//not found the pooling gameobject and create gameobject 
			//GameObject _goTemp = Instantiate (_prefab, _pos, _qua) as GameObject; 
			//이상하게 위치 잡고 생성하면 오류나고 이렇게 해야만 한다...

			//이것도 안된다. ㅠㅠ.
			//Debug.Log ("create2 > .");
			//GameObject _goTemp = Instantiate ((GameObject)_prefab, _pos, _qua) as GameObject; 			//유니티 자체가 멈춰버린다. ㅠㅠ
			//GameObject _goTemp = Instantiate (_prefab, _pos, _qua) as GameObject; 						//유니티 자체가 멈춰버린다. ㅠㅠ
			GameObject _goTemp = (Instantiate (_prefab.transform, _pos, _qua) as Transform).gameObject; 	//된다. ㅠㅠ.		
			//Debug.Log ("create2 > I");
			//Debug.Log("create2 > I" + _goTemp.transform.position + ":" + _pos);

			//생성후(Awake, OnEnable) => 위치 음... 문제
			//1. Instantiate 	-> 	Awake()
			//				    	OnEnable() -> 충돌, 체크 할 수 있다....
			//						*위치가 반영되었다고 생각했는데... > 순서 꼬임...
			//2. pos, qut		-> 	이제 위치 반영... 
			//GameObject _goTemp = Instantiate (_prefab);	
			//_goTemp.transform.position = _pos;
			//_goTemp.transform.rotation = _qua;
			//Debug.Log ("create2 > I");
			Transform _parent 	= transform;
			bool _bUI 			= false;
			FindGameObjectInfoToParent(_prefab, ref _parent, ref _bUI);

			_goTemp.transform.SetParent (_parent);
			if (_bUI) {
				_goTemp.transform.localScale = Vector3.one;
			}
			_list.Insert (_dataList.front, _goTemp);
			_goTemp.name += _dataList.max.ToString ();
			_rtn = _goTemp;
            if (!_goTemp.activeInHierarchy)
            {
                _goTemp.SetActive(true);
                //Debug.Log(_goTemp.name + ":" + _goTemp.activeInHierarchy);
            }

            //Debug.Log ("add front:" + _dataList.front);

            _dataList.front++;
			_dataList.max++;
			if (_dataList.front >= _dataList.max) {
				_dataList.front = 0;
			}
			//Debug.Log ("info front:" + _dataList.front + " max:" + _dataList.max);
		} else {
			Debug.LogWarning (" 성장이 아닌데 여유가 없다 > 논리 오류");
		}

		return _rtn;
	}

	//Prefab 정보 리스트에서 검색해서 부모찾기...
	void FindGameObjectInfoToParent(GameObject _go, ref Transform _parent, ref bool _bUI){
		int _len = objList.Count, j;
		for (j = 0; j < _len; j++) {
			if(objList [j].prefab == _go){
				_parent = objList [j].parent;
				_bUI = objList[j].bUI;
				break;
			}
		}
		//return _parent;
	}

}

