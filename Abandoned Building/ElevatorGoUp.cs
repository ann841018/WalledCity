using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ElevatorGoUp : MonoBehaviour {
	
	public GameObject SerchUI;
	public Flowchart talkFlowchart;
	public string playerInString;

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			SerchUI.SetActive (true);//調查的UI
			if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
				SerchUI.SetActive (false);//調查的UI
				Player.FromSceneNumber = 3;
				Block targetBlock = talkFlowchart.FindBlock (playerInString);//Fungus
				talkFlowchart.ExecuteBlock (targetBlock);
			} 
		}else SerchUI.SetActive (false);//調查的UI
	}
	void OnTriggerExit(Collider Other){if (Other.tag == "Player") {SerchUI.SetActive (false);}}
}
