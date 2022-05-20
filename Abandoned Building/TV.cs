using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using cakeslice;

public class TV : MonoBehaviour 
{
	public GameObject OriginalCamera,CameraForBox,Mei;
	public GameObject SerchUI,Canvas,Key;
	public GameObject Object;
	public Transform NewPos;
	public Flowchart talkFlowchart;
	public string playerInString0;
	public string playerInString;
	public static bool HaveMei;

	float time;
	bool CanInputPassNumber,Open;
	bool [] CanInput = new bool[13];

	// Use this for initialization
	void Start () {CameraForBox.SetActive (false);Canvas.SetActive (false);}//UI關起來

	void FixedUpdate(){
		float h = Input.GetAxis ("Horizontal");float v = Input.GetAxis ("Vertical");//左邊的箭頭
		Object.gameObject.GetComponentInChildren<cakeslice.Outline>().enabled = false;

		if (CanInputPassNumber == true) {CanInput [1] = true;
			if (Open == false) {
				if(v>=1)Key.transform.rotation = Quaternion.Euler (0, 180, 0);if(v >= 0.5f && h >= 0.5f)Key.transform.rotation = Quaternion.Euler (0, 180, 45);
				if(h>=1)Key.transform.rotation = Quaternion.Euler (0, 180, 90);if(v <= -0.5f && h >= 0.5f)Key.transform.rotation = Quaternion.Euler (0, 180, 135);
				if(v<=-1)Key.transform.rotation = Quaternion.Euler (0, 180, 180);if(v <= -0.5f && h <= -0.5f)Key.transform.rotation = Quaternion.Euler (0, 180, 225);
				if(h<=-1)Key.transform.rotation = Quaternion.Euler (0, 180, 270);if(v >= 0.5f && h <= -0.5f)Key.transform.rotation = Quaternion.Euler (0, 180, 315);
			}

			if (CanInput [1] == true){if (h >= 0.5f) {CanInput [2] = true;CanInput [1] = false;}else if(h <= -0.5f)CanInput [1] = false;}//2
			if (CanInput [2] == true){if (h >= 1) {CanInput [3] = true;CanInput [2] = false;}else if (v >= 1)CanInput [2] = false;}//3
			if (CanInput [3] == true){if (h <= 0.5f||v <= -0.5f) {CanInput [4] = true;CanInput [3] = false;}else if(v >= 0.5f)CanInput [3] = false;}//4
			if (CanInput [4] == true) {if (h >= 1) {CanInput [5] = true;CanInput [4] = false;}else if (v <= -1)CanInput [4] = false;}//3
			if (CanInput [5] == true) {if (h <= 0.5f) {CanInput [6] = true;CanInput [5] = false;}else if (v <= -1)CanInput [5] = false;}//2
			if (CanInput [6] == true) {if (v >= 1) {CanInput [7] = true;CanInput [6] = false;}else if (h >= 1)CanInput [6] = false;}//1
			if (CanInput [7] == true) {if (h <= -0.5f) {CanInput [8] = true;CanInput [7] = false;}else if (h >= 0.5f)CanInput [7] = false;}//8
			if (CanInput [8] == true) {if (v >= 1) {CanInput [9] = true;CanInput [8] = false;}else if (h <= -1)CanInput [7] = false;}//1
			if (CanInput [9] == true){if (h >= 0.5f) {CanInput [10] = true;CanInput [9] = false;}else if(h <= -0.5f)CanInput [9] = false;}//2
			if (CanInput [10] == true){if (h >= 1) {CanInput [11] = true;CanInput [10] = false;}else if (v >= 1)CanInput [10] = false;}//3
			if (CanInput [11] == true) {if (h <= 0.5f) {CanInput [12] = true;CanInput [11] = false;}else if (v <= -1)CanInput [11] = false;}//2
			if (CanInput [12] == true) {if (v >= 1) {Open = true;CanInput [12] = false;}}//1

			if (Open == true) {
				time = time + Time.deltaTime;Canvas.SetActive (false);SerchUI.SetActive (true);//調查的UI
				Key.transform.position = Vector3.Slerp (Key.transform.position, NewPos.position, Time.deltaTime);
				if (time > 1) {
					Quaternion NewRot = Quaternion.Euler (30, 60, -30);
					Key.transform.rotation = Quaternion.Lerp (Key.transform.rotation,NewRot,Time.deltaTime*0.5f);
					Key.transform.position = Vector3.Slerp (Key.transform.position, NewPos.position, Time.deltaTime);
				}
				if (time > 2) {
					Block targetBlock = talkFlowchart.FindBlock (playerInString);
					talkFlowchart.ExecuteBlock (targetBlock);
					CanInputPassNumber = false;
				}
			}

			if (Input.GetKeyDown (KeyCode.JoystickButton1)||Input.GetKeyDown(KeyCode.Escape)) {HaveMei = true;CanInputPassNumber = false;}//離開解密碼
		}else if (CanInputPassNumber == false) {if(PassWord.HaveMei == true && Hints.HaveMei == true){Mei.SetActive (true);OriginalCamera.SetActive (true);CameraForBox.SetActive (false);}Canvas.SetActive (false);}
	}

	void OnTriggerEnter(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (true);//調查的UI
			Object.gameObject.GetComponentInChildren<cakeslice.Outline>().enabled = true;
			CameraControlInside.OutlineCamera.GetComponentInChildren<OutlineEffect>().enabled = true;
		}
	}
	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			if (Open == false) {
				if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)){
					SerchUI.SetActive (false);
					Block targetBlock = talkFlowchart.FindBlock (playerInString0);
					talkFlowchart.ExecuteBlock (targetBlock);
					Mei.SetActive(false);OriginalCamera.SetActive (false);CameraForBox.SetActive (true);
					Canvas.SetActive (true);CanInputPassNumber = true;HaveMei = false;
				}
			} else {
				CameraControlInside.OutlineCamera.GetComponentInChildren<OutlineEffect>().enabled = false;
				CameraForBox.SetActive (false);Canvas.SetActive (false);}
		}
	}

	void OnTriggerExit(Collider Other){
		if (Other.tag == "Player") {
			SerchUI.SetActive (false);CanInputPassNumber = false;
			Object.gameObject.GetComponentInChildren<cakeslice.Outline>().enabled = false;
		}
	}//調查的UI
}