using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using cakeslice;

public class Item : MonoBehaviour {

	public GameObject SerchUI;
	public GameObject Object;
	public Flowchart talkFlowchart;
	public string playerInString;

	void FixedUpdate ()
	{
		Object.gameObject.GetComponentInChildren<Outline>().enabled = false;
	}

	void OnTriggerEnter(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (true);//調查的UI
			Object.gameObject.GetComponentInChildren<Outline>().enabled = true;
			CameraControlInside.OutlineCamera.GetComponentInChildren<OutlineEffect>().enabled = true;
		}
	}

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			
			if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
				SerchUI.SetActive (false);//調查的UI
				Block targetBlock = talkFlowchart.FindBlock (playerInString);//Fungus
				talkFlowchart.ExecuteBlock (targetBlock);
			} 
		} else {SerchUI.SetActive (false);CameraControlInside.OutlineCamera.GetComponentInChildren<OutlineEffect>().enabled = false;}//調查的UI
	}
	void OnTriggerExit(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (false);//調查的UI
			Object.gameObject.GetComponentInChildren<Outline>().enabled = false;
			CameraControlInside.OutlineCamera.GetComponentInChildren<OutlineEffect>().enabled = false;
		}
	}
}
