using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using cakeslice;

public class Handle : MonoBehaviour
{
	public GameObject SerchUI;
	public GameObject SwichHandle;
	public Flowchart talkFlowchart;
	public string playerInString;

	bool Catch;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void FixedUpdate () {}

	void OnTriggerEnter(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (true);//調查的UI
			CameraControlInside.OutlineCamera.GetComponentInChildren<OutlineEffect>().enabled = true;
		}
	}

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			if (Catch == false) {
				SerchUI.SetActive (true);
				if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
					SerchUI.SetActive (false);SwichHandle.SetActive(false);
					CameraControlInside.OutlineCamera.GetComponentInChildren<OutlineEffect>().enabled = true;
					Block targetBlock = talkFlowchart.FindBlock (playerInString);//Fungus
					talkFlowchart.ExecuteBlock (targetBlock);
					Catch = true;
				}
			} else SerchUI.SetActive (false);
		}
	}

	void OnTriggerExit(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (false);//調查的UI
			CameraControlInside.OutlineCamera.GetComponentInChildren<OutlineEffect>().enabled = false;
		}
	}
}
