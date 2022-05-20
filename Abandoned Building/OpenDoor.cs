using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class OpenDoor: MonoBehaviour 
{
	public GameObject SerchUI;
	public GameObject Door;
	public AudioSource DoorSound;

	public int DoorRot;

	bool Open;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void FixedUpdate () {
		if (Open == true) {
			Quaternion NewDoor0Rot = Quaternion.Euler (0, DoorRot, 0);
			Door.transform.rotation = Quaternion.Slerp(Door.transform.rotation,NewDoor0Rot,Time.deltaTime*2);
			Door.GetComponent<BoxCollider> ().enabled = false;
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
					DoorSound.Play ();
				}
			} else SerchUI.SetActive (false); //調查的UI
		}
	}
	void OnTriggerExit(Collider Other){if (Other.tag == "Player") {SerchUI.SetActive (false);}}
}
