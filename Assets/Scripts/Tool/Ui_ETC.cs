using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_ETC : MonoBehaviour {
	public static Ui_ETC ins;
	public UIInput uiHealth, uiSpeed, uiDamage, uiAi;
	public GameObject child;

	TileInfo tile;

	void Awake(){
		ins = this;
		child.SetActive (false);
	}

	//Popup <- Tile (Class reference)
	public void InvokePopup(TileInfo _tile){
		//Debug.Log(this + "InvokePopupUiETC");
		child.SetActive (true);

		tile 			= _tile;
		uiHealth.value 	= tile.data.enemyHealth.ToString();
		uiSpeed.value 	= tile.data.enemySpeed.ToString();
		uiDamage.value 	= tile.data.enemyDamage.ToString();
		uiAi.value		= tile.data.enemyAiType.ToString();
	}

	//Class reference 와서 값만 넣어주면된다....
	public void InvokeOk(){
		child.SetActive (false);

		tile.data.enemyHealth 	= float.Parse(uiHealth.value);
		tile.data.enemySpeed 	= float.Parse(uiSpeed.value);
		tile.data.enemyDamage 	= float.Parse(uiDamage.value);
		tile.data.enemyAiType 	= int.Parse(uiAi.value);
		tile.InvokeRefreshETC ();
	}
}
