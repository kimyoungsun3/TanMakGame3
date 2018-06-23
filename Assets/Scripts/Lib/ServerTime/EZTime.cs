using UnityEngine;
using System;
using System.Collections;

public static class EZTime{
	public static float timeout = 5;
	public static DateTime myDateTime;
	public static bool bNoInternet;
	public static bool bReady;

	public static TimeSpan localTimeMinusServerTime;

	public static IEnumerator GetServerTime(string _url){
		Debug.Log ("Try getting server time");
		bReady = false;

		WWW _w = new WWW (_url);
		yield return _w;

		float startTime = Time.unscaledTime;
		while (_w.error != null) {
			Debug.Log ("!link.isDone....");
			if ((Time.unscaledTime - timeout) > startTime) {
				Debug.Log ("it's taking too long. i am giving up...");
				bNoInternet = true;
				break;
			}
			yield return new WaitForEndOfFrame ();
		}

		if (!bNoInternet) {
			string _strTime = _w.text;
			myDateTime = DateTime.Parse (_strTime);
			localTimeMinusServerTime = DateTime.Now - myDateTime;
			bReady = true;
			Debug.Log ("connect:" + localTimeMinusServerTime);
		} else {
			myDateTime = DateTime.Now;
			localTimeMinusServerTime = TimeSpan.Zero;
			bReady = true;

			Debug.Log ("not connect:" + localTimeMinusServerTime);
		}
		yield return new WaitForEndOfFrame ();
	}

	public static DateTime ConvertLocalToServerTime(DateTime _localTime){
		return _localTime - localTimeMinusServerTime;
	}
}
