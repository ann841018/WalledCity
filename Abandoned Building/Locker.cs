using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Locker: MonoBehaviour 
{
	public GameObject SerchUI;
	public GameObject Door0, Door1;

	public int DoorRot0, DoorRot1;

	bool Open;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void FixedUpdate () {
		if (Open == true) {
			Quaternion NewDoor0Rot = Quaternion.Euler (0, DoorRot0, 0);
			Quaternion NewDoor1Rot = Quaternion.Euler (0, DoorRot1, 0);
			Door0.transform.rotation = Quaternion.Slerp(Door0.transform.rotation,NewDoor0Rot,Time.deltaTime*2);
			Door1.transform.rotation = Quaternion.Slerp(Door1.transform.rotation,NewDoor1Rot,Time.deltaTime*2);
		}
	}

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			if (Open == false) {//沒裝上
				SerchUI.SetActive (true);//調查的UI
				if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {//按圈圈或E
					SerchUI.SetActive (false);//調查的UI
					Open = true;//裝上
				}
			} else SerchUI.SetActive (false);//調查的UI
		}
	}
	void OnTriggerExit(Collider Other){if (Other.tag == "Player") {SerchUI.SetActive (false);}}
}
