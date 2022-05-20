using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Props : MonoBehaviour {

	public GameObject CatchUI,PropInfo;
	public GameObject[] ToggleFather = new GameObject[7];//道具複製出的地方
	public Toggle ItemToggle;//被複製的道具本人
	Toggle ItemToggleClone;//被複製出來的道具Toggle

	bool CanInputO,CanInputE,UIOpen;//可以按按鍵

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Input.GetKeyUp (KeyCode.JoystickButton2))CanInputO = true;//可以按按鍵
		if (Input.GetKeyUp (KeyCode.E))CanInputE = true;//可以按按鍵
	}

	void OnTriggerEnter(Collider Other){if (Other.tag == "Player"){CatchUI.SetActive (true);UIOpen = true;}}//打開UI
	void OnTriggerExit(Collider Other) {if (Other.tag == "Player"){CatchUI.SetActive (false);}}//關掉UI
	void OnTriggerStay(Collider Other) {if (Other.tag == "Player"){
		if(CanInputO==true){
			if (Input.GetKeyDown (KeyCode.JoystickButton2)) {//按圈
				if (UIOpen == true) {CatchUI.SetActive (false);UIOpen = false;}//關掉UI
				Player.myAnim.Play ("PickUp");//檢取動畫
				ItemToggleClone = Instantiate (ItemToggle, ToggleFather [OptionControl.MakeItemNumber].transform);//道具生成
				OptionControl.ItemCount = OptionControl.ItemCount + 1;//背包道具數+1
				PropInfo.SetActive (true);CanInputO = false;Destroy (this.gameObject);}//刪掉物件
			}
		}
		if(CanInputE==true){
			if (Input.GetKeyDown (KeyCode.E)) {//按E
				if (UIOpen == true) {CatchUI.SetActive (false);UIOpen = false;}//關掉UI
				Player.myAnim.Play("PickUp");//檢取動畫
				ItemToggleClone = Instantiate (ItemToggle, ToggleFather [OptionControl.MakeItemNumber].transform);//道具生成
				OptionControl.ItemCount = OptionControl.ItemCount+1;//背包道具數+1
				PropInfo.SetActive (true);CanInputE = false;Destroy (this.gameObject);//刪掉物件
			}
		}
	}
}