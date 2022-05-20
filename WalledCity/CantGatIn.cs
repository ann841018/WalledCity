using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CantGatIn : MonoBehaviour {

	public GameObject SerchUI;

	public Flowchart talkFlowchart;
	public string playerInString;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {}


	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (true);
			if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
				SerchUI.SetActive (false);
				Block targetBlock = talkFlowchart.FindBlock (playerInString);//Fungus
				talkFlowchart.ExecuteBlock (targetBlock);
			}
		}
	}
	void OnTriggerExit(Collider Other){if (Other.tag == "Player") {SerchUI.SetActive (false);}}
}
