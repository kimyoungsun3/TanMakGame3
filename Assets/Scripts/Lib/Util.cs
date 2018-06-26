using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
	//public static T[] ShuffleArray<T>(T[] _array, int _seed){
	//	System.Random _rand = new System.Random (_seed);
	//
	//	int _len = _array.Length;
	//	int _iMax = _len - 1;
	//	int _idx;
	//	T _temp;
	//	for (int i = 0; i < _iMax; i++) {
	//		_idx = _rand.Next (i, _len);
	//
	//		_temp = _array [_idx];
	//		_array [_idx] = _array [i];
	//		_array [i] = _temp;
	//	}
	//
	//	return _array;
	//}

	public static string GetScoreString(int _val){
		return _val.ToString ("000000");
	}

	public static string GetPrice (int _val){
		return _val.ToString ("$0");
	}

	static float intervalTime = 60f;
	static float intervalStartTime = 0;
	public static void SetInterpolation( float _startTime){
		intervalStartTime = _startTime;
	}

	public static float GetInterpolation(){
		return Mathf.Clamp01 ((Time.time - intervalStartTime) / intervalTime);
	}
}
