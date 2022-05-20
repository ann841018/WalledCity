using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningOption : MonoBehaviour 
{
	public GameObject BackpackUI,BackgroundUI,ImformationUI;//UI
	public Toggle[] BackpackToggle,People,Object,Scene,GroupOfPeople;//小項目的Toggle
	int PanelNumber,BackpackNumber;//選項編號
	float time;//背包開關的CD時間
	bool CanOpenBackpack,BackpackOpen;//是否開起選單或背包
	bool CanInputH,CanInputV,CanInputHJ,CanInputVJ;//按鍵
	bool CanInputW = true,CanInputS = true,CanInputA = true,CanInputD = true;

	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");float v = Input.GetAxis ("Vertical");
		float hj = Input.GetAxis ("HorizontalJoy");float vj = Input.GetAxis ("VerticalJoy");
		float AxisAim = Input.GetAxis ("AxisAim");
		if (h == 0)CanInputH = true;if (v == 0)CanInputV = true;
		if (hj == 0)CanInputHJ = true;if (vj == 0)CanInputVJ = true;
		if (Input.GetKeyUp (KeyCode.W))CanInputW = true;if (Input.GetKeyUp (KeyCode.S))CanInputS = true;
		if (Input.GetKeyUp (KeyCode.A))CanInputA = true;if (Input.GetKeyUp (KeyCode.D))CanInputD = true;
		//if (BackpackOpen == true) {
			time = time+Time.deltaTime;MusicPlayer.bag = true;if(time>1)CanOpenBackpack = true;
			BackgroundUI.SetActive (true);BackpackUI.SetActive (true);ImformationUI.SetActive (true);
			if (CanInputW == true) {if (Input.GetKeyDown (KeyCode.W)) {BackpackNumber = BackpackNumber - 1;PlayerSound.bagchack = true;CanInputW = false;}}//按W鍵向上
			if (CanInputS == true) {if (Input.GetKeyDown (KeyCode.S)) {BackpackNumber = BackpackNumber + 1;PlayerSound.bagchack = true;CanInputS = false;}}//按S鍵向下
			if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {PanelNumber = PanelNumber - 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputA = false;}}//按A鍵向左
			if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {PanelNumber = PanelNumber + 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputD = false;}}//按D鍵向右
			if (CanInputV == true) {if (v >= 1) {BackpackNumber = BackpackNumber - 1;PlayerSound.bagchack = true;CanInputV = false;}else if (v <= -1) {BackpackNumber = BackpackNumber + 1;PlayerSound.bagchack = true;CanInputV = false;}}
			if (CanInputVJ == true){if (vj >= 1) {BackpackNumber = BackpackNumber + 1;PlayerSound.bagchack = true;CanInputVJ = false;}else if (vj <= -1) {BackpackNumber = BackpackNumber - 1;PlayerSound.bagchack = true;CanInputVJ = false;}}//按搖桿的上下鍵
			if (CanInputH == true) {if (h >= 1) {PanelNumber = PanelNumber + 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputH = false;}else if (h <= -1) {PanelNumber = PanelNumber - 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputH = false;}}
			if (CanInputHJ == true){if (hj >= 1) {PanelNumber = PanelNumber + 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputHJ = false;}else if (hj <= -1) {PanelNumber = PanelNumber - 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputHJ = false;}}//按搖桿的左右鍵
			//if (CanOpenBackpack==true) {if (Input.GetKey (KeyCode.JoystickButton1)||Input.GetKey(KeyCode.Q)) {BackpackOpen = false;PanelNumber = 0;BackpackNumber = 0;time = 0;PlayerSound.Bagop = true;}}//關背包
			if (PanelNumber <= -1)PanelNumber = 3;if (PanelNumber >= 4)PanelNumber = 0;
			for (int i = 0; i < 4; i++) {if (i == PanelNumber) {BackpackToggle [i].interactable = true;BackpackToggle [i].isOn = true;} else {BackpackToggle [i].interactable = false;BackpackToggle [i].isOn = false;}}
			switch (PanelNumber) {
			case 0:
				if (BackpackNumber <= -1)BackpackNumber = 4;if (BackpackNumber >= 5)BackpackNumber = 0;
				for (int i = 0; i < 5; i++) {
					if(i==BackpackNumber)People[i].isOn = true;else People[i].isOn = false;
					if(i<2)Object[i].isOn = false;if(i<3)Scene[i].isOn = false;if(i<4)GroupOfPeople[i].isOn = false;
				}
				break;
			case 1:
				if (BackpackNumber <= -1)BackpackNumber = 1;if (BackpackNumber >= 2)BackpackNumber = 0;
				for (int i = 0; i < 5; i++) {People[i].isOn = false;
					if(i<3)Scene[i].isOn = false;if(i<4)GroupOfPeople[i].isOn = false;
					if(i<2) {if(i==BackpackNumber)Object[i].isOn = true;else Object[i].isOn = false;}
				}
				break;
			case 2:
				if (BackpackNumber <= -1)BackpackNumber = 2;if (BackpackNumber >= 3)BackpackNumber = 0;
				for (int i = 0; i < 5; i++) {People[i].isOn = false;
					if(i<2)Object[i].isOn = false;if(i<4)GroupOfPeople[i].isOn = false;
					if(i<3){if(i==BackpackNumber)Scene[i].isOn = true;else Scene[i].isOn = false;}
				}
				break;
			case 3:
				if (BackpackNumber <= -1)BackpackNumber = 3;if (BackpackNumber >= 4)BackpackNumber = 0;
				for (int i = 0; i < 5; i++) {People[i].isOn = false;
					if(i<2)Object[i].isOn = false;if(i<3)Scene[i].isOn = false;
					if(i<4){if(i==BackpackNumber)GroupOfPeople[i].isOn = true;else GroupOfPeople[i].isOn = false;}
				}
				break;
			}
		//}//else if (BackpackOpen == false) {time = time+Time.deltaTime;BackpackUI.SetActive (false);ImformationUI.SetActive (false);if(time>1)CanOpenBackpack = true;MusicPlayer.bag = false;}
	}
} 