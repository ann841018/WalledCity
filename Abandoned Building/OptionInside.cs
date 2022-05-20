using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionInside : MonoBehaviour {
	public Text VolumeText,BackgroundmusicText;//音量的數字
	public GameObject Camera,OutsideUI,OptionUI,BackpackUI,BackgroundUI,ImformationUI,CantUseUI;//相機物件 //UI
	public Toggle[] OutsideToggle,OptionToggle,BackpackToggle;//大項目的Toggle
	public Toggle[] People,Object,Scene,GroupOfPeople,CameraRotate,Text,Joystick,Volume;//小項目的Toggle
	public MusicPlayerInside musicinside;//室內音樂控制

	int OutsideNumber,OptionNumber,ChooseNumber,PanelNumber,BackpackNumber,VolumeSize = 80,BackgroundmusicSize = 80;//選項編號 //音量大小
	bool OutSideOpen,OptionOpen,BackpackOpen,CanOpenOutside;//是否開啟選單或背包
	bool CanInputH,CanInputV,CanInputHJ,CanInputVJ,CanInputHL,CanInputHR,CanInputO,CanInputE,CanInputSpace;//按鍵
	bool CanInputW = true,CanInputS = true,CanInputA = true,CanInputD = true,CanInputC = true,CanInputZ = true;//按鍵
	float time;//背包開關的CD時間

	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");float v = Input.GetAxis ("Vertical");float hj = Input.GetAxis ("HorizontalJoy");float vj = Input.GetAxis ("VerticalJoy");float AxisAim = Input.GetAxis ("AxisAim");//搖桿數值
		if (h == 0)CanInputH = true;if (v == 0)CanInputV = true;if (hj == 0)CanInputHJ = true;if (vj == 0)CanInputVJ = true;//可以按按鍵
		if (Input.GetKeyUp (KeyCode.W))CanInputW = true;if (Input.GetKeyUp (KeyCode.S))CanInputS = true;//可以按鍵盤
		if (Input.GetKeyUp (KeyCode.A))CanInputA = true;if (Input.GetKeyUp (KeyCode.D))CanInputD = true;//可以按鍵盤
		if (Input.GetKeyUp (KeyCode.C))CanInputC = true;if (Input.GetKeyUp (KeyCode.Z))CanInputZ = true;//可以按鍵盤
		if (Input.GetKeyUp (KeyCode.E))CanInputE = true;if (Input.GetKeyUp (KeyCode.Space))CanInputSpace = true;
		if (Input.GetKeyUp (KeyCode.JoystickButton2))CanInputO = true;//可以按按鍵
		if (Input.GetKeyUp (KeyCode.JoystickButton4))CanInputHL = true;if (Input.GetKeyUp (KeyCode.JoystickButton5))CanInputHR = true;//可以按按鍵
		if (CanOpenOutside==true) {if (Input.GetKey (KeyCode.JoystickButton3) || Input.GetKey (KeyCode.Escape)) {OutSideOpen = !OutSideOpen;OutsideNumber = 0;time = 0;PlayerSound.Bagop = true;CanOpenOutside = false;}}//開關選單

		if (OutSideOpen == true) {time = time+Time.deltaTime;MusicPlayer.bag = true;//選單打開
			Player.CanMove = false;Camera.GetComponent<CameraControlInside>().enabled = false;
			BackgroundUI.SetActive (true);OutsideUI.SetActive(true);if(time>1)CanOpenOutside = true;//背包關上
			if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {OutsideNumber = OutsideNumber - OptionControl.Xn;CanInputA = false;}}//按A鍵向左
			if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {OutsideNumber = OutsideNumber + OptionControl.Xn;CanInputD = false;}}//按D鍵向右
			if (CanInputH == true) {if (h >= 1) {OutsideNumber = OutsideNumber + OptionControl.Xn;CanInputH = false;}else if (h <= -1) {OutsideNumber = OutsideNumber - OptionControl.Xn;CanInputH = false;}}//按搖桿的左右鍵
			if (CanInputHJ == true){if (hj >= 1) {OutsideNumber = OutsideNumber + OptionControl.Xn;CanInputHJ = false;}else if (hj <= -1) {OutsideNumber = OutsideNumber - OptionControl.Xn;CanInputHJ = false;}}//按搖桿的左右鍵
			if (CanOpenOutside == true) {if (Input.GetKey (KeyCode.JoystickButton1) || Input.GetKey (KeyCode.Q)) {OutSideOpen = false;OutsideNumber = 0;PlayerSound.Bagop = true;CanOpenOutside = false;}}//開關選單
			if (OutsideNumber <= -1)OutsideNumber = 4;if (OutsideNumber >= 5)OutsideNumber = 0;//OutsideNumber在0-4之間
			for (int i = 0; i < 5; i++) {if (i == OutsideNumber) {OutsideToggle [i].isOn = true;} else {OutsideToggle [i].isOn = false;}}//被選到的Toggle打開//其他關上
			if (Input.GetKey (KeyCode.JoystickButton2)||Input.GetKey(KeyCode.E)){
				switch (OutsideNumber) {
				case 0:	CantUseUI.SetActive (true);break;//開背包
				case 1: BackpackOpen = true;PanelNumber = 0;BackpackNumber = 0;PlayerSound.Bagop = true;break;//開背包
				case 2: CantUseUI.SetActive (true);break;//開地圖
				case 3: OptionOpen = true;PlayerSound.Bagop = true;break;//開選單
				case 4:	SceneManager.LoadScene (0);musicinside.BGMFStop ();break;}}//回到主畫面
			else CantUseUI.SetActive (false);
		} else if (OutSideOpen == false) {
			Player.CanMove = true;Camera.GetComponent<CameraControlInside> ().enabled = true;//相機能動
			time = time + Time.deltaTime;OutsideUI.SetActive (false);
			if (time > 1)CanOpenOutside = true;MusicPlayer.bag = false;
		}

		if(OptionOpen == true){//設定打開
			Player.CanMove = false;MusicPlayer.bag = true;Camera.GetComponent<CameraControlInside>().enabled = false;CanOpenOutside = false;//背包關上
			OutSideOpen = false;BackpackOpen = false;BackgroundUI.SetActive (true);OptionUI.SetActive (true);
			VolumeText.text = VolumeSize.ToString();BackgroundmusicText.text = BackgroundmusicSize.ToString();//音量的數字顯示
			if (CanInputW == true) {if (Input.GetKeyDown (KeyCode.W)) {OptionNumber = OptionNumber - OptionControl.Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputW = false;}}//按W鍵向上
			if (CanInputS == true) {if (Input.GetKeyDown (KeyCode.S)) {OptionNumber = OptionNumber + OptionControl.Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputS = false;}}//按S鍵向下
			if (CanInputV == true) {if (v >= 1) {OptionNumber = OptionNumber - OptionControl.Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputV = false;}else if (v <= -1) {OptionNumber = OptionNumber + OptionControl.Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputV = false;}}//按搖桿的上下鍵
			if (CanInputVJ == true){if (vj >= 1) {OptionNumber = OptionNumber + OptionControl.Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputVJ = false;}else if (vj <= -1) {OptionNumber = OptionNumber - OptionControl.Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputVJ = false;}}if (OptionNumber < 5){
				if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {ChooseNumber = ChooseNumber - OptionControl.Xn;CanInputA = false;}}//按A鍵向左
				if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {ChooseNumber = ChooseNumber + OptionControl.Xn;CanInputD = false;}}//按D鍵向右
				if (CanInputH == true) {if (h >= 1) {ChooseNumber = ChooseNumber + OptionControl.Xn;CanInputH = false;}else if (h <= -1) {ChooseNumber = ChooseNumber - OptionControl.Xn;CanInputH = false;}}//按搖桿的左右鍵
				if (CanInputHJ == true){if (hj >= 1) {ChooseNumber = ChooseNumber + OptionControl.Xn;CanInputHJ = false;}else if (hj <= -1) {ChooseNumber = ChooseNumber - OptionControl.Xn;CanInputHJ = false;}}}//按搖桿的左右鍵
			if (Input.GetKey (KeyCode.JoystickButton1) || Input.GetKey (KeyCode.Q)) {OptionOpen = false;OutSideOpen = true;OutsideNumber = 3;time = 0;PlayerSound.Bagop = true;CanOpenOutside = false;}//關選單
			if (OptionNumber == 0||OptionNumber == 1) {if(ChooseNumber<=-1)ChooseNumber=2;if(ChooseNumber>=3)ChooseNumber=0;}
			if (OptionNumber >= 2 && OptionNumber <= 4) {if(ChooseNumber<=-1)ChooseNumber=1;if(ChooseNumber>=2)ChooseNumber=0;}
			if (OptionNumber <= -1)OptionNumber = 6;if (OptionNumber >= 7)OptionNumber = 0;//選單編號在0-6之間
			for (int i = 0; i < 7; i++) {if (i == OptionNumber) {OptionToggle [i].isOn = true;} else {OptionToggle [i].isOn = false;}}//被選到的Toggle打開//其他關上
			switch (OptionNumber) {
			case 0:
				for (int i = 0; i < 3; i++) {if (i == ChooseNumber){CameraRotate[ChooseNumber].isOn = true;}else {CameraRotate[i].isOn = false;}}
				if (ChooseNumber == 0) {OptionControl.CameraRotateSpeedSet = 2.5f;}else if (ChooseNumber == 1) {OptionControl.CameraRotateSpeedSet = 2f;}else if (ChooseNumber == 2) {OptionControl.CameraRotateSpeedSet = 1.5f;}break;//旋轉鏡頭速度設定慢
			case 1:
				for (int i = 0; i < 3; i++) {if (i == ChooseNumber){Text[ChooseNumber].isOn = true;}else {Text[i].isOn = false;}} 
				if (ChooseNumber == 0) {OptionControl.DialogSpeed = 60;}else if (ChooseNumber == 1) {OptionControl.DialogSpeed = 10;}else if (ChooseNumber == 2) {OptionControl.DialogSpeed = 0;}break;//字幕速度
			case 2:
				for (int i = 0; i < 2; i++) {if (i == ChooseNumber){Text[ChooseNumber+3].isOn = true;}else {Text[i+3].isOn = false;}}
				if (ChooseNumber == 0) {OptionControl.HaveDialog = true;}else if (ChooseNumber == 1) {OptionControl.HaveDialog = false;}break;//字幕
			case 3:
				for (int i = 0; i < 2; i++) {if (i == ChooseNumber){Joystick[ChooseNumber].isOn = true;}
				else {Joystick[i].isOn = false;}} if (ChooseNumber == 0) {OptionControl.Xn = 1;}else if (ChooseNumber == 1) {OptionControl.Xn = -1;}break;//鏡射
			case 4:
				for (int i = 0; i < 2; i++) {if (i == ChooseNumber){Joystick[ChooseNumber+2].isOn = true;}else {Joystick[i+2].isOn = false;}}
				if (ChooseNumber == 0) {OptionControl.Yn = 1;}else if (ChooseNumber == 1) {OptionControl.Yn = -1;}break;//鏡射
			case 5:
				if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {Volume[0].isOn = true;Volume[1].isOn = false;VolumeSize = VolumeSize - 10*OptionControl.Xn;CanInputA = false;}}//音量大小設定
				if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {Volume[1].isOn = true;Volume[0].isOn = false;VolumeSize = VolumeSize + 10*OptionControl.Xn;CanInputD = false;}}//音量大小設定    
				if (CanInputH == true) {if (h >= 1) {Volume[1].isOn = true;Volume[0].isOn = false;VolumeSize = VolumeSize + 10*OptionControl.Xn;CanInputH = false;}else if (h <= -1) {Volume[0].isOn = true;Volume[1].isOn = false;VolumeSize = VolumeSize - 10*OptionControl.Xn;CanInputH = false;}}
				if (CanInputHJ == true){if (hj >= 1) {Volume[1].isOn = true;Volume[0].isOn = false;VolumeSize = VolumeSize + 10*OptionControl.Xn;CanInputHJ = false;}else if (hj <= -1) {Volume[0].isOn = true;Volume[1].isOn = false;VolumeSize = VolumeSize - 10*OptionControl.Xn;CanInputHJ = false;}}
				if (VolumeSize > 100)VolumeSize = 100;if (VolumeSize < 0)VolumeSize = 0;break;//音量在0-100之間
			case 6:
				if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {Volume[2].isOn = true;Volume[3].isOn = false;BackgroundmusicSize = BackgroundmusicSize - 10*OptionControl.Xn;CanInputA = false;}}//背景音量設定
				if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {Volume[3].isOn = true;Volume[2].isOn = false;BackgroundmusicSize = BackgroundmusicSize + 10*OptionControl.Xn;CanInputD = false;}}//背景音量設定
				if (CanInputH == true) {if (h >= 1) {Volume[3].isOn = true;Volume[2].isOn = false;BackgroundmusicSize = BackgroundmusicSize + 10*OptionControl.Xn;CanInputH = false;}else if (h <= -1) {Volume[2].isOn = true;Volume[3].isOn = false;BackgroundmusicSize = BackgroundmusicSize - 10*OptionControl.Xn;CanInputH = false;}}
				if (CanInputHJ == true){if (hj >= 1) {Volume[3].isOn = true;Volume[2].isOn = false;BackgroundmusicSize = BackgroundmusicSize + 10*OptionControl.Xn;CanInputHJ = false;}else if (hj <= -1) {Volume[2].isOn = true;Volume[3].isOn = false;BackgroundmusicSize = BackgroundmusicSize - 10*OptionControl.Xn;CanInputHJ = false;}}
				if(BackgroundmusicSize > 100)BackgroundmusicSize = 100;if(BackgroundmusicSize < 0)BackgroundmusicSize = 0;break;//音量在0-100之間
			}
		}else if (OptionOpen == false) {OptionUI.SetActive (false);MusicPlayer.bag = false;}

		if (BackpackOpen == true) {//圖鑑打開
			Player.CanMove = false;MusicPlayer.bag = true;Camera.GetComponent<CameraControlInside>().enabled = false;CanOpenOutside = false;//角色敵人相機不能動
			OutSideOpen = false;OptionOpen = false;BackgroundUI.SetActive (true);BackpackUI.SetActive (true);ImformationUI.SetActive (true);//其他關掉
			if (CanInputW == true) {if (Input.GetKeyDown (KeyCode.W)) {BackpackNumber = BackpackNumber - 1;PlayerSound.bagchack = true;CanInputW = false;}}//按W鍵向上
			if (CanInputS == true) {if (Input.GetKeyDown (KeyCode.S)) {BackpackNumber = BackpackNumber + 1;PlayerSound.bagchack = true;CanInputS = false;}}//按S鍵向下
			if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {PanelNumber = PanelNumber - 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputA = false;}}//按A鍵向左
			if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {PanelNumber = PanelNumber + 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputD = false;}}//按D鍵向右
			if (CanInputHL == true){if (Input.GetKeyDown (KeyCode.JoystickButton4)) {PanelNumber = PanelNumber - OptionControl.Xn;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputH = false;}}//L1向左
			if (CanInputHR == true){if (Input.GetKeyDown (KeyCode.JoystickButton5)) {PanelNumber = PanelNumber + OptionControl.Xn;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputHL = false;}}//R1向左
			if (CanInputV == true) {if (v >= 1) {BackpackNumber = BackpackNumber - OptionControl.Yn;PlayerSound.bagchack = true;CanInputV = false;}else if (v <= -1) {BackpackNumber = BackpackNumber + OptionControl.Yn;PlayerSound.bagchack = true;CanInputV = false;}}//按搖桿的上下鍵
			if (CanInputVJ == true){if (vj >= 1) {BackpackNumber = BackpackNumber + OptionControl.Yn;PlayerSound.bagchack = true;CanInputVJ = false;}else if (vj <= -1) {BackpackNumber = BackpackNumber - OptionControl.Yn;PlayerSound.bagchack = true;CanInputVJ = false;}}//按搖桿的上下鍵
			if (Input.GetKey (KeyCode.JoystickButton1)||Input.GetKey(KeyCode.Q)) {BackpackOpen = false;OutSideOpen = true;PanelNumber = 0;BackpackNumber = 0;OutsideNumber = 1;time = 0;PlayerSound.Bagop = true;CanOpenOutside = false;}//關背包
			if (PanelNumber <= -1)PanelNumber = 3;if (PanelNumber >= 4)PanelNumber = 0;
			for (int i = 0; i < 4; i++) {if (i == PanelNumber) {BackpackToggle [i].interactable = true;BackpackToggle [i].isOn = true;} else {BackpackToggle [i].interactable = false;BackpackToggle [i].isOn = false;}}
			switch (PanelNumber) {
			case 0:
				if (BackpackNumber <= -1)BackpackNumber = 4;if (BackpackNumber >= 5)BackpackNumber = 0;
				for (int i = 0; i < 5; i++) {
					if(i==BackpackNumber)People[i].isOn = true;else People[i].isOn = false;
					if(i<2)Object[i].isOn = false;if(i<3)Scene[i].isOn = false;if(i<4)GroupOfPeople[i].isOn = false;}break;
			case 1:
				if (BackpackNumber <= -1)BackpackNumber = 1;if (BackpackNumber >= 2)BackpackNumber = 0;
				for (int i = 0; i < 5; i++) {People[i].isOn = false;
					if(i<3)Scene[i].isOn = false;if(i<4)GroupOfPeople[i].isOn = false;
					if(i<2) {if(i==BackpackNumber)Object[i].isOn = true;else Object[i].isOn = false;}}break;
			case 2:
				if (BackpackNumber <= -1)BackpackNumber = 2;if (BackpackNumber >= 3)BackpackNumber = 0;
				for (int i = 0; i < 5; i++) {People[i].isOn = false;
					if(i<2)Object[i].isOn = false;if(i<4)GroupOfPeople[i].isOn = false;
					if(i<3){if(i==BackpackNumber)Scene[i].isOn = true;else Scene[i].isOn = false;}}break;
			case 3:
				if (BackpackNumber <= -1)BackpackNumber = 3;if (BackpackNumber >= 4)BackpackNumber = 0;
				for (int i = 0; i < 5; i++) {People[i].isOn = false;
					if(i<2)Object[i].isOn = false;if(i<3)Scene[i].isOn = false;
					if(i<4){if(i==BackpackNumber)GroupOfPeople[i].isOn = true;else GroupOfPeople[i].isOn = false;}}break;
			}
		}else if (BackpackOpen == false) {BackpackUI.SetActive (false);ImformationUI.SetActive (false);MusicPlayer.bag = false;}//圖鑑關閉
	}
} 