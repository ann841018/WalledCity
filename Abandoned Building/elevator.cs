using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class elevator : MonoBehaviour {

	public GameObject Serch;
	public GameObject SerchText;
	public GameObject ElevatorOpen;
	bool DoorOpen;

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			if (DoorOpen == false) {//沒打開
				Serch.SetActive (true);SerchText.SetActive (false);//調查的UI
				if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
					Serch.SetActive (false);SerchText.SetActive (true);//調查的UI
					ElevatorOpen.SetActive(true);
					DoorOpen = true;//打開
				}
			}else {Serch.SetActive (false);SerchText.SetActive (true);}//調查的UI
		} 
	}
	void OnTriggerExit(Collider other){if (other.tag == "Player")Serch.SetActive(false);SerchText.SetActive (true);}//調查的UI
}
