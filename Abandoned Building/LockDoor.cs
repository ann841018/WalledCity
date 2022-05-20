using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LockDoor: MonoBehaviour 
{
	public GameObject SerchUI;
	public GameObject Door;
	public Flowchart talkFlowchart;
	public string playerInString;

	bool Open;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void FixedUpdate () {}

	void OnTriggerEnter(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (true);
		}//調查的UI
	}

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
				SerchUI.SetActive (false);//調查的UI
				Block targetBlock = talkFlowchart.FindBlock (playerInString);
				talkFlowchart.ExecuteBlock (targetBlock);
			}
		} else SerchUI.SetActive (false);//調查的UI
	}
	void OnTriggerExit(Collider Other){if (Other.tag == "Player") {SerchUI.SetActive (false);}}
}
