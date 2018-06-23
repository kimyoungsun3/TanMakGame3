using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour {
	[Serializable]
	public class SoundData{
		public string name;  
		public AudioClip[] clips;
		public AudioSource[] sources;
		public bool pitchRandom = true;
		public int index, indexBefore;
		[HideInInspector] public int hashID;

		public void Init(){
			hashID = Animator.StringToHash (name);
		}

		public void Play(bool _bLoop, float _pitch, int _clipIdx = -1){
			//Debug.Log (name + ", " + index);
			if (_clipIdx == -1) {
				_clipIdx = Random.Range (0, clips.Length);
			}
			//Debug.Log ("play:" + _clipIdx + ":" + index);

			sources [index].Stop ();
			sources [index].pitch 	= _pitch;
			sources [index].loop 	= _bLoop;
			sources [index].clip 	= clips[_clipIdx];
			sources [index].Play ();

			indexBefore = index;
			index = ( index + 1 ) % sources.Length; 
		}

		public void Stop(){
			sources [indexBefore].Stop ();
		}

		public void AllStop(){
			for (int i = 0, iMax = sources.Length; i < iMax; i++) {
				sources [i].Stop ();
			}
		}			
	}

	public static SoundManager ins;
	public float pitchLower = 0.95f;
	public float pitchUp = 1.05f;
	//public AudioSource[] music;
	public List<SoundData> soundDataList = new List<SoundData> ();
	SoundData beforeData, currentData;
	//bool bInit = false;
	//int nameNum = 0;
	//int channel;

	void Awake(){
		if (ins == null) {
			ins = this;
		} else if (ins != this) {
			//전것존재 -> 또다른것 -> 삭제. 이후는 실행안됨(Start, OnEnable)...
			//Debug.Log ("또생성? 음... 삭제(지금것)");
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject);
	}

	void Start(){
		for (int i = 0, iMax = soundDataList.Count; i < iMax; i++) {
			soundDataList[i].Init ();
		}
	}

	//Play("xxx", true)   -> xxx 1번 loop
	//Play("xxx", false)  -> xxx 1번 one shoot
	//Play("xxx", -1) -> xxx 1번 one shoot
	public void Play(string  _name, bool _bLoop, int _clipIdx = -1){
		//Debug.Log ("name:" + _name + " loop:" + _bLoop);
		int _hashID = Animator.StringToHash (_name);
		Play (_hashID, _bLoop, _clipIdx);
	}

	public void Play(int _hashID){
		Play (_hashID, false);
	}

	public void Play(int _hashID, bool _bLoop, int _clipIdx = -1){
		//Debug.Log ("SoundManager Play _bLoop:" + _bLoop);
		bool _rtn = false;
		if (beforeData != null && beforeData.hashID == _hashID) {
			//before data reuse.
			currentData = beforeData;
		}else{
			//find
			currentData = FindSound (_hashID, string.Empty);
		}
		beforeData = currentData;

		if (currentData != null) {
			currentData.Play (_bLoop, Random.Range (pitchLower, pitchUp), _clipIdx);
		}
	}

	public void Stop(string  _name){
		//Debug.Log ("SoundManager Stop _name:" + _name);
		int _hashID = Animator.StringToHash (_name);
		SoundData _data = FindSound (_hashID, _name);
		if (_data != null) {
			_data.Stop ();
		}
	}

	SoundData FindSound(int _hashID, string _name){
		//Debug.Log ("SoundManager FindSound _hashID:" + _hashID);
		SoundData _data = null;
		for (int i = 0, iMax = soundDataList.Count; i < iMax; i++) {
			if (soundDataList [i].hashID == _hashID) {
				_data = soundDataList[i];
				break;
			}
		} 

		#if UNITY_EDITOR
		if(_data == null){
			Debug.LogError ("사운드 이름(hashID):" + _name + ":" + _hashID);
		}
		#endif

		return _data;
	}


	#if UNITY_EDITOR
	[Space]
	[Header("Debug Data")]
	public int debugIndex = 0; 
	public int debugMusicIndex = -1;
	public bool bDebugPlay = false;
	public string debugName;
	void Update(){
		if (bDebugPlay) {
			if (debugIndex >= 0 && debugIndex < soundDataList.Count) {
				SoundData _s = soundDataList [debugIndex];
				bDebugPlay = false;
				if (debugMusicIndex < -1) {
					debugMusicIndex = -1;
				} else if (debugMusicIndex >= _s.clips.Length) {
					debugMusicIndex = _s.clips.Length - 1;
				}
				SoundManager.ins.Play (_s.name, false, debugMusicIndex);
				debugName = _s.name + " "+debugMusicIndex+"번 사운드";
			} else {
				debugName = "rang out";
			}
		}
	}
	#endif
}
