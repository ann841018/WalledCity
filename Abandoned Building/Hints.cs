using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Hints: MonoBehaviour
{
	public GameObject SerchUI;
	public GameObject Mei;
	public GameObject OriginalCamera;
	public GameObject CameraForHint;
	public Flowchart talkFlowchart;
	public string playerInString;

	public static bool HaveMei;

	bool LookHint;

	// Use this for initialization
	void Start () {CameraForHint.SetActive (false);}

	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.JoystickButton1)||Input.GetKeyDown(KeyCode.Escape)) {
			Block targetBlock = talkFlowchart.FindBlock (playerInString);
			talkFlowchart.ExecuteBlock (targetBlock);
			LookHint = false;
			HaveMei = true;
		}//離開解密碼

		if (LookHint == false) {
			if(PassWord.HaveMei == true && TV.HaveMei == true){CameraForHint.SetActive (false);Mei.SetActive (true);OriginalCamera.SetActive (true);HaveMei = true;}
		}
	}

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (true);
			if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
				SerchUI.SetActive (false);
				Mei.SetActive (false);OriginalCamera.SetActive (false);CameraForHint.SetActive (true);
				HaveMei = false;LookHint = true;
			}
		} else SerchUI.SetActive (false);
	}
	void OnTriggerExit(Collider Other){if (Other.tag == "Player") {SerchUI.SetActive (false);}}
}
