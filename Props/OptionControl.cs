using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

public class OptionControl : MonoBehaviour {
	public Text VolumeText,BackgroundmusicText;//音量的數字
	public Text[] UIOnHeadText;
	public GameObject Camera,MeiHead,MeiHand,MeiFront,UseItem,NotUseItem;
	public GameObject OutsideUI,OptionUI,BackpackUI,BackgroundUI,IllustratedUI,ImformationUI,ChooseUI,MapUI;//相機物件 //UI
	public GameObject[] IllustratedToggleFather,ItemObject,ItemInfo;//道具複製出來的地方//被複製的道具//道具資訊
	public Toggle[] OutsideToggle,OptionToggle,BackpackToggle,IllustratedToggle = new Toggle[ItemCount];//大項目的Toggle
	public Toggle[] People,Object,Scene,GroupOfPeople,CameraRotate,Text,Joystick,Volume;//小項目的Toggle
	public Flowchart talkFlowchart;
	public MusicPlayer musicoutside;//室外音樂控制
	GameObject ItemClone;//被複製出來的道具
	Block targetBlock;

	public static float CameraRotateSpeedSet = 2.5f,DialogSpeed = 60;//相機旋轉速度設定//打字速度設定
	public static int Xn = 1,Yn = 1,ItemCount = 1,MakeItemNumber,UseItemPanel,UIOnHeadNumber;//XY軸翻轉
	public static bool HaveDialog,CanCopy;//是否有字幕

	int OutsideNumber,OptionNumber,ChooseNumber,PanelNumber,BackpackNumber,VolumeSize = 80,BackgroundmusicSize = 80;//選項編號 //音量大小
	bool OutSideOpen,OptionOpen,BackpackOpen,MapOpen,IllustratedOpen,CanOpenOutside;//是否開啟選單或背包
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
		for (int i = 0; i < ItemCount; i++){IllustratedToggle [i] = IllustratedToggleFather [i].GetComponentInChildren<Toggle>();
			if(IllustratedToggle [i] == null){if (IllustratedToggle [i + 1] != null) {IllustratedToggle [i + 1].transform.parent = IllustratedToggleFather [i].transform;}else MakeItemNumber = i;}
			else MakeItemNumber = ItemCount;}//背包物品

		if (OutSideOpen == true) {time = time+Time.deltaTime;//選單打開
			Player.CanMove = false;Enemy.canMove = false;EnemyMelee.canMove = false;EnemyGuard.canMove = false;EnemyGuardMelee.canMove = false;MusicPlayer.bag = true;//角色敵人相機不能動
			if(Player.FromSceneNumber == 1)Camera.GetComponent<CameraControl>().enabled = false;
			if(Player.FromSceneNumber == 2)Camera.GetComponent<CameraControlInside>().enabled = false;
			BackgroundUI.SetActive (true);OutsideUI.SetActive(true);if(time>1)CanOpenOutside = true;//背包關上
			if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {OutsideNumber = OutsideNumber - Xn;CanInputA = false;}}//按A鍵向左
			if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {OutsideNumber = OutsideNumber + Xn;CanInputD = false;}}//按D鍵向右
			if (CanInputH == true) {if (h >= 1) {OutsideNumber = OutsideNumber + Xn;CanInputH = false;}else if (h <= -1) {OutsideNumber = OutsideNumber - Xn;CanInputH = false;}}//按搖桿的左右鍵
			if (CanInputHJ == true){if (hj >= 1) {OutsideNumber = OutsideNumber + Xn;CanInputHJ = false;}else if (hj <= -1) {OutsideNumber = OutsideNumber - Xn;CanInputHJ = false;}}//按搖桿的左右鍵
			if (CanOpenOutside == true) {if (Input.GetKey (KeyCode.JoystickButton1) || Input.GetKey (KeyCode.Q)) {OutSideOpen = false;OutsideNumber = 0;PlayerSound.Bagop = true;CanOpenOutside = false;}}//開關選單
			if (OutsideNumber <= -1)OutsideNumber = 4;if (OutsideNumber >= 5)OutsideNumber = 0;//OutsideNumber在0-4之間
			for (int i = 0; i < 5; i++) {if (i == OutsideNumber) {OutsideToggle [i].isOn = true;} else {OutsideToggle [i].isOn = false;}}//被選到的Toggle打開//其他關上
			if (Input.GetKey (KeyCode.JoystickButton2)||Input.GetKey(KeyCode.E)){
			switch (OutsideNumber) {
			case 0: IllustratedOpen = true;BackpackNumber = 0;PlayerSound.Bagop = true;CanInputE = false;CanInputO = false;break;//開背包
			case 1: BackpackOpen = true;PanelNumber = 0;BackpackNumber = 0;PlayerSound.Bagop = true;break;//開背包
			case 2: MapOpen = true;PlayerSound.Bagop = true;break;//開地圖
			case 3: OptionOpen = true;PlayerSound.Bagop = true;break;//開選單
			case 4:	SceneManager.LoadScene (0);musicoutside.BGMStop ();break;}}//回到主畫面
		} else if (OutSideOpen == false) {
			if (StartStory.JustStart == false && Player.IsFlash == false) {Camera.GetComponent<CameraControl> ().enabled = true;//相機能動
				Player.CanMove = true;Enemy.canMove = true;EnemyMelee.canMove = true;EnemyGuard.canMove = true;EnemyGuardMelee.canMove = true;}//腳色敵人能動
			time = time+Time.deltaTime;OutsideUI.SetActive (false);if(time>1)CanOpenOutside = true;MusicPlayer.bag = false;
		}

		if(OptionOpen == true){//設定打開
			Player.CanMove = false;Enemy.canMove = false;EnemyMelee.canMove = false;EnemyGuard.canMove = false;EnemyGuardMelee.canMove = false;//角色敵人相機不能動
			MusicPlayer.bag = true;Camera.GetComponent<CameraControl>().enabled = false;CanOpenOutside = false;//背包關上
			OutSideOpen = false;BackpackOpen = false;IllustratedOpen = false;MapOpen = false;BackgroundUI.SetActive (true);OptionUI.SetActive (true);
			VolumeText.text = VolumeSize.ToString();BackgroundmusicText.text = BackgroundmusicSize.ToString();//音量的數字顯示
			if (CanInputW == true) {if (Input.GetKeyDown (KeyCode.W)) {OptionNumber = OptionNumber - Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputW = false;}}//按W鍵向上
			if (CanInputS == true) {if (Input.GetKeyDown (KeyCode.S)) {OptionNumber = OptionNumber + Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputS = false;}}//按S鍵向下
			if (CanInputV == true) {if (v >= 1) {OptionNumber = OptionNumber - Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputV = false;}else if (v <= -1) {OptionNumber = OptionNumber + Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputV = false;}}//按搖桿的上下鍵
			if (CanInputVJ == true){if (vj >= 1) {OptionNumber = OptionNumber + Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputVJ = false;}else if (vj <= -1) {OptionNumber = OptionNumber - Yn;ChooseNumber = 0;PlayerSound.bagchack = true;CanInputVJ = false;}}if (OptionNumber < 5){
			if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {ChooseNumber = ChooseNumber - Xn;CanInputA = false;}}//按A鍵向左
			if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {ChooseNumber = ChooseNumber + Xn;CanInputD = false;}}//按D鍵向右
			if (CanInputH == true) {if (h >= 1) {ChooseNumber = ChooseNumber + Xn;CanInputH = false;}else if (h <= -1) {ChooseNumber = ChooseNumber - Xn;CanInputH = false;}}//按搖桿的左右鍵
			if (CanInputHJ == true){if (hj >= 1) {ChooseNumber = ChooseNumber + Xn;CanInputHJ = false;}else if (hj <= -1) {ChooseNumber = ChooseNumber - Xn;CanInputHJ = false;}}}//按搖桿的左右鍵
			if (Input.GetKey (KeyCode.JoystickButton1) || Input.GetKey (KeyCode.Q)) {OptionOpen = false;OutSideOpen = true;OutsideNumber = 3;time = 0;PlayerSound.Bagop = true;CanOpenOutside = false;}//關選單
			if (OptionNumber == 0||OptionNumber == 1) {if(ChooseNumber<=-1)ChooseNumber=2;if(ChooseNumber>=3)ChooseNumber=0;}
			if (OptionNumber >= 2 && OptionNumber <= 4) {if(ChooseNumber<=-1)ChooseNumber=1;if(ChooseNumber>=2)ChooseNumber=0;}
			if (OptionNumber <= -1)OptionNumber = 6;if (OptionNumber >= 7)OptionNumber = 0;//選單編號在0-6之間
			for (int i = 0; i < 7; i++) {if (i == OptionNumber) {OptionToggle [i].isOn = true;} else {OptionToggle [i].isOn = false;}}//被選到的Toggle打開//其他關上
			switch (OptionNumber) {
			case 0:
				for (int i = 0; i < 3; i++) {if (i == ChooseNumber){CameraRotate[ChooseNumber].isOn = true;}else {CameraRotate[i].isOn = false;}}
				if (ChooseNumber == 0) {CameraRotateSpeedSet = 2.5f;}else if (ChooseNumber == 1) {CameraRotateSpeedSet = 2f;}else if (ChooseNumber == 2) {CameraRotateSpeedSet = 1.5f;}break;//旋轉鏡頭速度設定慢
			case 1:
				for (int i = 0; i < 3; i++) {if (i == ChooseNumber){Text[ChooseNumber].isOn = true;}else {Text[i].isOn = false;}} 
				if (ChooseNumber == 0) {DialogSpeed = 60;}else if (ChooseNumber == 1) {DialogSpeed = 10;}else if (ChooseNumber == 2) {DialogSpeed = 0;}break;//字幕速度
			case 2:
				for (int i = 0; i < 2; i++) {if (i == ChooseNumber){Text[ChooseNumber+3].isOn = true;}else {Text[i+3].isOn = false;}}
				if (ChooseNumber == 0) {HaveDialog = true;}else if (ChooseNumber == 1) {HaveDialog = false;}break;//字幕
			case 3:
				for (int i = 0; i < 2; i++) {if (i == ChooseNumber){Joystick[ChooseNumber].isOn = true;}
				else {Joystick[i].isOn = false;}} if (ChooseNumber == 0) {Xn = 1;}else if (ChooseNumber == 1) {Xn = -1;}break;//鏡射
			case 4:
				for (int i = 0; i < 2; i++) {if (i == ChooseNumber){Joystick[ChooseNumber+2].isOn = true;}else {Joystick[i+2].isOn = false;}}
				if (ChooseNumber == 0) {Yn = 1;}else if (ChooseNumber == 1) {Yn = -1;}break;//鏡射
			case 5:
				if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {Volume[0].isOn = true;Volume[1].isOn = false;VolumeSize = VolumeSize - 10*Xn;CanInputA = false;}}//音量大小設定
				if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {Volume[1].isOn = true;Volume[0].isOn = false;VolumeSize = VolumeSize + 10*Xn;CanInputD = false;}}//音量大小設定    
				if (CanInputH == true) {if (h >= 1) {Volume[1].isOn = true;Volume[0].isOn = false;VolumeSize = VolumeSize + 10*Xn;CanInputH = false;}else if (h <= -1) {Volume[0].isOn = true;Volume[1].isOn = false;VolumeSize = VolumeSize - 10*Xn;CanInputH = false;}}
				if (CanInputHJ == true){if (hj >= 1) {Volume[1].isOn = true;Volume[0].isOn = false;VolumeSize = VolumeSize + 10*Xn;CanInputHJ = false;}else if (hj <= -1) {Volume[0].isOn = true;Volume[1].isOn = false;VolumeSize = VolumeSize - 10*Xn;CanInputHJ = false;}}
				if (VolumeSize > 100)VolumeSize = 100;if (VolumeSize < 0)VolumeSize = 0;break;//音量在0-100之間
			case 6:
				if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {Volume[2].isOn = true;Volume[3].isOn = false;BackgroundmusicSize = BackgroundmusicSize - 10*Xn;CanInputA = false;}}//背景音量設定
				if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {Volume[3].isOn = true;Volume[2].isOn = false;BackgroundmusicSize = BackgroundmusicSize + 10*Xn;CanInputD = false;}}//背景音量設定
				if (CanInputH == true) {if (h >= 1) {Volume[3].isOn = true;Volume[2].isOn = false;BackgroundmusicSize = BackgroundmusicSize + 10*Xn;CanInputH = false;}else if (h <= -1) {Volume[2].isOn = true;Volume[3].isOn = false;BackgroundmusicSize = BackgroundmusicSize - 10*Xn;CanInputH = false;}}
				if (CanInputHJ == true){if (hj >= 1) {Volume[3].isOn = true;Volume[2].isOn = false;BackgroundmusicSize = BackgroundmusicSize + 10*Xn;CanInputHJ = false;}else if (hj <= -1) {Volume[2].isOn = true;Volume[3].isOn = false;BackgroundmusicSize = BackgroundmusicSize - 10*Xn;CanInputHJ = false;}}
				if(BackgroundmusicSize > 100)BackgroundmusicSize = 100;if(BackgroundmusicSize < 0)BackgroundmusicSize = 0;break;//音量在0-100之間
			}
		}else if (OptionOpen == false) {OptionUI.SetActive (false);MusicPlayer.bag = false;}

		if (BackpackOpen == true) {//圖鑑打開
			Player.CanMove = false;Enemy.canMove = false;EnemyMelee.canMove = false;EnemyGuard.canMove = false;EnemyGuardMelee.canMove = false;MusicPlayer.bag = true;Camera.GetComponent<CameraControl>().enabled = false;CanOpenOutside = false;//角色敵人相機不能動
			OutSideOpen = false;OptionOpen = false;IllustratedOpen = false;MapOpen = false;BackgroundUI.SetActive (true);BackpackUI.SetActive (true);ImformationUI.SetActive (true);//其他關掉
			if (CanInputW == true) {if (Input.GetKeyDown (KeyCode.W)) {BackpackNumber = BackpackNumber - 1;PlayerSound.bagchack = true;CanInputW = false;}}//按W鍵向上
			if (CanInputS == true) {if (Input.GetKeyDown (KeyCode.S)) {BackpackNumber = BackpackNumber + 1;PlayerSound.bagchack = true;CanInputS = false;}}//按S鍵向下
			if (CanInputA == true) {if (Input.GetKeyDown (KeyCode.A)) {PanelNumber = PanelNumber - 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputA = false;}}//按A鍵向左
			if (CanInputD == true) {if (Input.GetKeyDown (KeyCode.D)) {PanelNumber = PanelNumber + 1;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputD = false;}}//按D鍵向右
			if (CanInputHL == true){if (Input.GetKeyDown (KeyCode.JoystickButton4)) {PanelNumber = PanelNumber - Xn;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputH = false;}}//L1向左
			if (CanInputHR == true){if (Input.GetKeyDown (KeyCode.JoystickButton5)) {PanelNumber = PanelNumber + Xn;BackpackNumber = 0;PlayerSound.bagchack = true;CanInputHL = false;}}//R1向左
			if (CanInputV == true) {if (v >= 1) {BackpackNumber = BackpackNumber - Yn;PlayerSound.bagchack = true;CanInputV = false;}else if (v <= -1) {BackpackNumber = BackpackNumber + Yn;PlayerSound.bagchack = true;CanInputV = false;}}//按搖桿的上下鍵
			if (CanInputVJ == true){if (vj >= 1) {BackpackNumber = BackpackNumber + Yn;PlayerSound.bagchack = true;CanInputVJ = false;}else if (vj <= -1) {BackpackNumber = BackpackNumber - Yn;PlayerSound.bagchack = true;CanInputVJ = false;}}//按搖桿的上下鍵
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

		if (IllustratedOpen == true) {//背包打開
			Player.CanMove = false;Enemy.canMove = false;EnemyMelee.canMove = false;EnemyGuard.canMove = false;EnemyGuardMelee.canMove = false;	MusicPlayer.bag = true;Camera.GetComponent<CameraControl>().enabled = false;//角色敵人相機不能動
			OutSideOpen = false;OptionOpen = false;BackpackOpen = false;MapOpen = false;BackgroundUI.SetActive (true);IllustratedUI.SetActive (true);ChooseUI.SetActive(true);CanOpenOutside = false;//其他關掉
			if (CanInputW == true) {if (Input.GetKeyDown (KeyCode.W)) {BackpackNumber = BackpackNumber - 1;PlayerSound.bagchack = true;CanInputW = false;}}//按W鍵向上
			if (CanInputS == true) {if (Input.GetKeyDown (KeyCode.S)) {BackpackNumber = BackpackNumber + 1;PlayerSound.bagchack = true;CanInputS = false;}}//按S鍵向下
			if (CanInputV == true) {if (v >= 1) {BackpackNumber = BackpackNumber - Yn;PlayerSound.bagchack = true;CanInputV = false;}else if (v <= -1) {BackpackNumber = BackpackNumber + Yn;PlayerSound.bagchack = true;CanInputV = false;}}
			if (CanInputVJ == true){if (vj >= 1) {BackpackNumber = BackpackNumber + Yn;PlayerSound.bagchack = true;CanInputVJ = false;}else if (vj <= -1) {BackpackNumber = BackpackNumber - Yn;PlayerSound.bagchack = true;CanInputVJ = false;}}//按搖桿的上下鍵
			if (UseItemPanel == 0) {if (Input.GetKey (KeyCode.JoystickButton1)||Input.GetKey(KeyCode.Q)) {IllustratedOpen = false;OutSideOpen = true;BackpackNumber = 0;OutsideNumber = 0;time = 0;PlayerSound.Bagop = true;CanOpenOutside = false;}}//關背包
			if (BackpackNumber <= -1)BackpackNumber = 5;if (BackpackNumber >= 6)BackpackNumber = 0;if (CanCopy == true) {CopyItem (BackpackNumber);}
			for (int i = 0; i < 6; i++) {if (i == BackpackNumber) {if(IllustratedToggle [i] != null){IllustratedToggle [i].interactable = true;IllustratedToggle [i].isOn = true;
				if(UseItemPanel == 0){
					if (CanInputO == true) {if (Input.GetKeyDown (KeyCode.JoystickButton2)) {
						if (IllustratedToggle [i].tag == "0") {targetBlock = talkFlowchart.FindBlock ("Gun");}//Fungus	
						else if (IllustratedToggle [i].tag == "1") {targetBlock = talkFlowchart.FindBlock ("Billboard");}//Fungus	
						else if (IllustratedToggle [i].tag == "2") {targetBlock = talkFlowchart.FindBlock ("Bowling");}//Fungus	
						else if (IllustratedToggle [i].tag == "3") {targetBlock = talkFlowchart.FindBlock ("Gang");}//Fungus	
						else if (IllustratedToggle [i].tag == "4") {targetBlock = talkFlowchart.FindBlock ("Camera");}//Fungus	
						else if (IllustratedToggle [i].tag == "5") {targetBlock = talkFlowchart.FindBlock ("Bahamutor");}//Fungus			
						if (targetBlock != null) {talkFlowchart.ExecuteBlock (targetBlock);}
						CanInputO = false;}
					}
					if (CanInputE == true) {if (Input.GetKeyDown (KeyCode.E)) {
						if (IllustratedToggle [i].tag == "0") {targetBlock = talkFlowchart.FindBlock ("Gun");}//Fungus	
						else if (IllustratedToggle [i].tag == "1") {targetBlock = talkFlowchart.FindBlock ("Billboard");}//Fungus	
						else if (IllustratedToggle [i].tag == "2") {targetBlock = talkFlowchart.FindBlock ("Bowling");}//Fungus	
						else if (IllustratedToggle [i].tag == "3") {targetBlock = talkFlowchart.FindBlock ("Gang");}//Fungus	
						else if (IllustratedToggle [i].tag == "4") {targetBlock = talkFlowchart.FindBlock ("Camera");}//Fungus	
						else if (IllustratedToggle [i].tag == "5") {targetBlock = talkFlowchart.FindBlock ("Bahamutor");}//Fungus	
						if (targetBlock != null) {talkFlowchart.ExecuteBlock (targetBlock);}CanInputE = false;
					}
				}}}//道具生成
			}else {if(IllustratedToggle [i] != null){IllustratedToggle [i].interactable = false;IllustratedToggle [i].isOn = false;}}}
		}else if (IllustratedOpen == false) {//背包關閉
			IllustratedUI.SetActive (false);ChooseUI.SetActive(false);MusicPlayer.bag = false;
			if (CanInputZ == true) {if (Input.GetKeyDown (KeyCode.Z)) {UIOnHeadNumber = UIOnHeadNumber - Xn;CanInputZ= false;}}//按Z鍵向左
			if (CanInputC == true) {if (Input.GetKeyDown (KeyCode.C)) {UIOnHeadNumber = UIOnHeadNumber + Xn;CanInputC = false;}}//按C鍵向右
			if (CanInputHJ == true){if (hj >= 1) {UIOnHeadNumber = UIOnHeadNumber + Xn;CanInputHJ = false;}else if (hj <= -1) {UIOnHeadNumber = UIOnHeadNumber - Xn;CanInputHJ = false;}}//按搖桿的左右鍵
			if (CanInputSpace == true){if (Input.GetKeyDown (KeyCode.Space)){if (IllustratedToggle [UIOnHeadNumber] != null) CopyItem (UIOnHeadNumber);}}
			if (Input.GetKeyDown (KeyCode.JoystickButton0)){if (IllustratedToggle [UIOnHeadNumber] != null) CopyItem (UIOnHeadNumber);}
			if (UIOnHeadNumber <= -1)UIOnHeadNumber = ItemCount-1;if(UIOnHeadNumber >= ItemCount)UIOnHeadNumber = 0;

			if (ItemCount >= 2) {
				if (UIOnHeadNumber != 0) {
					if (IllustratedToggle [UIOnHeadNumber - 1] != null) {UIOnHeadText [0].text = IllustratedToggle [UIOnHeadNumber - 1].GetComponentInChildren<Text> ().text;} 
					else UIOnHeadText [0].text = " ";
				} else if (UIOnHeadNumber == 0) {
					if (IllustratedToggle [ItemCount-1] != null) {UIOnHeadText [0].text = IllustratedToggle [ItemCount-1].GetComponentInChildren<Text> ().text;}
					else UIOnHeadText [0].text = " ";
				}
			}else UIOnHeadText [0].text = " ";
			if (IllustratedToggle [UIOnHeadNumber] != null) {UIOnHeadText [1].text = IllustratedToggle [UIOnHeadNumber].GetComponentInChildren<Text> ().text;}else UIOnHeadText [1].text = " ";
			if (ItemCount >= 2) {
				if (UIOnHeadNumber != ItemCount-1) {
					if (IllustratedToggle [UIOnHeadNumber + 1] != null) {UIOnHeadText [2].text = IllustratedToggle [UIOnHeadNumber + 1].GetComponentInChildren<Text> ().text;}
					else UIOnHeadText [2].text = " ";
				} else if (UIOnHeadNumber == ItemCount-1) {UIOnHeadText [2].text = IllustratedToggle [0].GetComponentInChildren<Text> ().text;}
			}else UIOnHeadText [2].text = " ";
		}

		if (Input.GetKey (KeyCode.JoystickButton13))MapOpen = true;
		if(MapOpen == true){//地圖打開
			Player.CanMove = false;Enemy.canMove = false;EnemyMelee.canMove = false;EnemyGuard.canMove = false;EnemyGuardMelee.canMove = false;	MusicPlayer.bag = true;Camera.GetComponent<CameraControl>().enabled = false;CanOpenOutside = false;//角色敵人相機不能動
			OutSideOpen = false;OptionOpen = false;BackpackOpen = false;IllustratedOpen = false;MapUI.SetActive (true);BackgroundUI.SetActive (true);//其他關掉
			if (Input.GetKey (KeyCode.JoystickButton13))MapOpen = false;
			if (Input.GetKey (KeyCode.JoystickButton1) || Input.GetKey (KeyCode.Q)) {MapOpen = false;OutSideOpen = true;OutsideNumber = 2;time = 0;PlayerSound.Bagop = true;CanOpenOutside = false;}}//地圖關閉
		else if(MapOpen == false){MapUI.SetActive (false);MusicPlayer.bag = false;}
	}

	void CopyItem(int i){for (int j = 0; j < 6; j++) {if (IllustratedToggle [i].tag == j.ToString ()) {//複製道具
		if (j == 0) {if (Player.UseGun == false) {Player.myAnim.Play ("ChangeBullet");//槍
			if (Player.UseCamera == true||Player.UseBowling == true)Destroy (MeiHand.gameObject.transform.GetChild (0).gameObject);//刪掉手上的道具
			ItemClone = Instantiate (ItemObject [j], MeiHand.transform);//道具生成
			ItemClone.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);UIOnHeadNumber = 0;//槍大小
			Player.UseCamera = false;Player.UseBowling = false;Player.UseGun = true;}
		} else if (j == 1) {Player.myAnim.Play ("Change");//看板
			ItemClone = Instantiate (ItemObject [j], MeiFront.transform.position, Player.Mei.rotation);ItemClone.AddComponent<Rigidbody> ();//道具生成在前面
			ItemCount = ItemCount - 1;BackpackNumber = BackpackNumber - 1;UIOnHeadNumber = 0;ItemInfo [j].SetActive (false);//背包道具數減一
			if (Player.UseGun == false) {
				if (Player.UseCamera == true||Player.UseBowling == true)Destroy (MeiHand.gameObject.transform.GetChild (0).gameObject);//刪掉手上的道具
				ItemClone = Instantiate (ItemObject [0], MeiHand.transform);//道具生成
				ItemClone.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);//槍大小
				Player.UseGun = true;}Player.UseCamera = false;Player.UseBowling = false;
				Destroy (IllustratedToggle [i].gameObject);//刪掉背包裡的道具
		} else if (j == 2) {if (Player.UseBowling == false) {Player.myAnim.Play ("Change");//保齡球
			if (Player.UseGun == true||Player.UseCamera == true)Destroy (MeiHand.gameObject.transform.GetChild (0).gameObject);//刪掉手上的道具
			ItemClone = Instantiate (ItemObject [j],MeiHand.transform);//道具生成
			ItemClone.transform.position = new Vector3 (MeiHand.transform.position.x+0.1f,MeiHand.transform.position.y-0.03f, MeiHand.transform.position.z);
			BackpackNumber = BackpackNumber - 1;ItemInfo [j].SetActive (false);//背包道具數減一
			Player.UseBowling = true;}Player.UseGun = false;Player.UseCamera = false;Destroy (IllustratedToggle [i].gameObject);//刪掉背包裡的道具
		} else if (j == 3) {//面具
			if (Player.UseMask == false) {if (Player.CanUseMask == true) {Player.myAnim.Play ("PutOnMask");ItemClone = Instantiate (ItemObject [j], MeiHead.transform);UIOnHeadNumber = 0;Player.UseMask = true;}}//道具生成在頭上
			else if(Player.UseMask == true){Player.myAnim.Play ("PutOffMask");Destroy (MeiHead.gameObject.transform.GetChild (0).gameObject);UIOnHeadNumber = 0;Player.UseMask = false;}//刪掉手上的道具
			if (Player.UseGun == false) {
				if (Player.UseCamera == true||Player.UseBowling == true)Destroy (MeiHand.gameObject.transform.GetChild (0).gameObject);//刪掉手上的道具
				ItemClone = Instantiate (ItemObject [0], MeiHand.transform);//道具生成
				ItemClone.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);//槍大小
				Player.UseGun = true;}Player.UseCamera = false;Player.UseBowling = false;
		} else if (j == 4) {if (Player.UseCamera == false) {Player.myAnim.Play("Change");//相機
			if (Player.UseGun == true||Player.UseBowling == true)Destroy (MeiHand.gameObject.transform.GetChild (0).gameObject);//刪掉手上的道具
			ItemClone = Instantiate (ItemObject [j], MeiHand.transform);//道具生成
			ItemClone.transform.position = new Vector3 (MeiHand.transform.position.x+0.05f,MeiHand.transform.position.y-0.03f, MeiHand.transform.position.z-0.05f);
			Player.UseCamera = true;}Player.UseGun = false;Player.UseBowling = false;
		} else if (j == 5) {Player.myAnim.Play ("Change");//巴哈姆特
			if (Player.UseGun == false) {
				if (Player.UseCamera == true||Player.UseBowling == true)Destroy (MeiHand.gameObject.transform.GetChild (0).gameObject);//刪掉手上的道具	
				ItemClone = Instantiate (ItemObject [0], MeiHand.transform);//道具生成
				ItemClone.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);//槍大小
				Player.UseGun = true;}Player.UseCamera = false;Player.UseBowling = false;
			ItemClone = Instantiate (ItemObject [j], MeiFront.transform.position, CameraControl.CameraControlTra.rotation);ItemClone.AddComponent<WalkFoward> ();//道具生成在前面
			ItemCount = ItemCount - 1;BackpackNumber = BackpackNumber - 1;UIOnHeadNumber = 0;ItemInfo [j].SetActive (false);//背包道具數減一
			Destroy (IllustratedToggle [i].gameObject);//刪掉背包裡的道具
		}
		if (ItemClone != null)ItemClone.tag = j.ToString ();
		IllustratedOpen = false;OutSideOpen = false;UseItem.SetActive (false);NotUseItem.SetActive (false);
		CanInputE = false;CanInputSpace = false;CanCopy = false;targetBlock = null;	UseItemPanel = 0;}}//關掉背包
	}
} 