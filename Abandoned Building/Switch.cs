using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Switch : MonoBehaviour 
{
	public GameObject SerchUI;
	public Flowchart talkFlowchart;
	public string playerInString;

	bool PutOn;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {}

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			if (PutOn == false) {//沒裝上
				SerchUI.SetActive (true);//調查的UI
				if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
					SerchUI.SetActive (false);//調查的UI
					Block targetBlock = talkFlowchart.FindBlock (playerInString);//Fungus
					talkFlowchart.ExecuteBlock (targetBlock);
					PutOn = true;//裝上
				}
			} else
				SerchUI.SetActive (false);//調查的UI
		}
	}
	void OnTriggerExit(Collider Other){if (Other.tag == "Player") {SerchUI.SetActive (false);PutOn = false;}}
}
