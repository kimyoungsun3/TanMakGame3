using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Help : MonoBehaviour {
	public UILabel uiHelp;
	public GameObject pointNeck;

	string[] strMsg = {
		"1. 처음 시작할 경우 [전체클리어]를 해주세요.",
		"2. 선택할 비행기를 클릭해주세요.",
		"3. Tile를 찍으시면 그 곳에 비행기가 리스폰 됩니다.",
		"Delete : 삭제모드로 전환, Tile를 클릭하면 삭제",
		"Modify : 수정모드로 변환, Tile에 숫자가 표시, 비표시",
		" > 수정모드에서는 직접 수정을 할 수 있다.",
		" > 파라미터를 변경후 해당 비행기를 직접 입력으로 권장.",
		"",
		"생성개수 : n회 반복 생성",
		"생성간격 : 이번 생성에서 2차 생성간격시간",
		"다음생성시간간격 : -1 자동계산, [클릭하면 생성개수 x 생성간격 > 시간]",
		"",
		"비행기 파라미터",
		"Health : 적군 비행기의 피",
		"Speed : 이동속도",
		"Damage : 데미지 값",
		"AIType : Ai type",
		""
	};

	void Start(){
		//uiHelp = GetComponent<UILabel> ();
		uiHelp.text = "";
		for (int i = 0, iMax = strMsg.Length; i < iMax; i++) {
			uiHelp.text += strMsg[i] + "\n\n";
		}
		InvokeDisable ();
	}

	public void InvokeDisable(){
		pointNeck.SetActive (false);
	}

	public void InvokeEnable(){
		pointNeck.SetActive (true);
	}
}
