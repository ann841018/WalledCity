using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class PassWord : MonoBehaviour 
{
	public GameObject OriginalCamera,CameraForBox,NewCameraPos;
	public GameObject SerchUI,Canvas;
	public GameObject Mei,Cover,Key;
	public GameObject[] Dies;
	public Toggle[] PassWordToggle;
	public Flowchart talkFlowchart;
	public string playerInString;
	public static bool HaveMei;

	int[] PassWordNumber = new int[4],PassWordAnswer = new int[4];
	int ChoosePassWordToggle,OpenNumber;
	bool [] CheckPassWordNumber = new bool[4];//判斷答案對不對
	bool CanInputPassNumber,CanInputH,CanInputV,CanInputHJ,CanInputVJ,Open,IsOpen;

	// Use this for initialization
	void Start () {
		CameraForBox.SetActive (false);Canvas.SetActive (false);//UI關起來
		PassWordAnswer[0] = 0;PassWordAnswer[1] = 5;PassWordAnswer[2] = 2;PassWordAnswer[3] = 9;//密碼的答案
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");float v = Input.GetAxis ("Vertical");//左邊的箭頭
		float hj = Input.GetAxis ("HorizontalJoy");float vj = Input.GetAxis ("VerticalJoy");//左邊的箭頭
		if (h == 0)CanInputH = true;if (v == 0)CanInputV = true;//不能重複按著
		if (hj == 0)CanInputHJ = true;if (vj == 0)CanInputVJ = true;//不能重複按著
		for (int i = 0; i < 4; i++) {
			if (PassWordNumber [i] == PassWordAnswer [i])CheckPassWordNumber [i] = true;//選的那格打開
			PassWordToggle [i].interactable = false;PassWordToggle [i].isOn = false;//其他關起來
			Dies [i].gameObject.transform.rotation = Quaternion.Euler (0, -90, -36 * PassWordNumber [i]);//箱子上的轉輪跟著改
		}

		PassWordToggle [ChoosePassWordToggle].interactable = true;PassWordToggle [ChoosePassWordToggle].isOn = true;

		if (CanInputPassNumber == true) {
			if (CanInputH == true) {
				if (h >= 1) {ChoosePassWordToggle = ChoosePassWordToggle + 1;if (ChoosePassWordToggle >= 4) {ChoosePassWordToggle = 0;}CanInputH = false;} 
				else if (h <= -1) {ChoosePassWordToggle = ChoosePassWordToggle - 1;if (ChoosePassWordToggle <= -1) {ChoosePassWordToggle = 3;}CanInputH = false;}
			}
			if (CanInputV == true) {
				if (v >= 1) {PassWordNumber [ChoosePassWordToggle] = PassWordNumber [ChoosePassWordToggle] - 1;if (PassWordNumber [ChoosePassWordToggle] <= -1) {PassWordNumber [ChoosePassWordToggle] = 9;}CanInputV = false;}//上
				else if (v <= -1) {PassWordNumber [ChoosePassWordToggle] = PassWordNumber [ChoosePassWordToggle]+ 1; if (PassWordNumber [ChoosePassWordToggle] >= 10) {PassWordNumber [ChoosePassWordToggle] = 0;}CanInputV = false;}//下
			}
			if (CanInputHJ == true) {
				if (hj >= 1 || Input.GetKeyDown (KeyCode.D)) {ChoosePassWordToggle = ChoosePassWordToggle + 1;if (ChoosePassWordToggle >= 4) {ChoosePassWordToggle = 0;}CanInputHJ = false;} 
				else if (hj <= -1 || Input.GetKeyDown (KeyCode.A)) {ChoosePassWordToggle = ChoosePassWordToggle - 1;if (ChoosePassWordToggle <= -1) {ChoosePassWordToggle = 3;}CanInputHJ = false;}
			}
			if (CanInputVJ == true) {
				if (vj >= 1 || Input.GetKeyDown (KeyCode.W)) {PassWordNumber [ChoosePassWordToggle] = PassWordNumber [ChoosePassWordToggle] + 1;if (PassWordNumber [ChoosePassWordToggle] >= 10) {PassWordNumber [ChoosePassWordToggle] = 0;}CanInputVJ = false;}//下
				else if (vj <= -1 || Input.GetKeyDown (KeyCode.S)) {PassWordNumber [ChoosePassWordToggle] = PassWordNumber [ChoosePassWordToggle] - 1;if (PassWordNumber [ChoosePassWordToggle] <= -1) {PassWordNumber [ChoosePassWordToggle] = 9;}CanInputVJ = false;}//上
			}

			if (OpenNumber == 0) {
				if (Input.GetKeyDown (KeyCode.JoystickButton1)||Input.GetKeyDown(KeyCode.Escape)) {CanInputPassNumber = false;}//離開解密碼
				if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)) {//按圈圈或E
					if (CheckPassWordNumber [0] == true && CheckPassWordNumber [1] == true && CheckPassWordNumber [2] == true && CheckPassWordNumber [3] == true) {Canvas.SetActive (false);OpenNumber = 1;Open = true;}
				}
			}else if (OpenNumber == 1) {
				if (Open == true) {
					Cover.transform.rotation = Quaternion.Slerp (Cover.transform.rotation, Quaternion.Euler (0, -90, -90), Time.deltaTime);
					CameraForBox.transform.position = Vector3.Lerp (CameraForBox.transform.position, NewCameraPos.transform.position, Time.deltaTime);
					if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.E)) {//按圈圈或E
						OpenNumber = 2;IsOpen = true;Key.SetActive (false);
						Block targetBlock = talkFlowchart.FindBlock (playerInString);
						talkFlowchart.ExecuteBlock (targetBlock);
					}
				}
			} else if (OpenNumber == 2) {if (IsOpen == true) {if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)) {Mei.SetActive (true);CameraForBox.SetActive (false);OriginalCamera.SetActive (true);HaveMei = true;Destroy (this);}}}
		}else if (CanInputPassNumber == false) {if(TV.HaveMei ==true && Hints.HaveMei == true){Mei.SetActive (true);OriginalCamera.SetActive (true);CameraForBox.SetActive (false);}Canvas.SetActive (false);}
	}

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			if (Open == false) {
				SerchUI.SetActive (true);//調查的UI
				if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)){
					SerchUI.SetActive (false);
					Mei.SetActive(false);OriginalCamera.SetActive (false);CameraForBox.SetActive (true);
					Canvas.SetActive (true);CanInputPassNumber = true;HaveMei = false;
				}
			} else {CameraForBox.SetActive (false);Canvas.SetActive (false);}
		}
	}
	void OnTriggerExit(Collider Other){if (Other.tag == "Player") {SerchUI.SetActive (false);}}//調查的UI
}