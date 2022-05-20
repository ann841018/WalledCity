using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Drawer: MonoBehaviour 
{
	public GameObject SerchUI;
	public GameObject Drawers;

	bool Open;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void FixedUpdate () {
		if (Open == true) {
			Vector3 NewPos = new Vector3 (2.65f,14.7f,2.1f);
			Drawers.transform.position = Vector3.Slerp (transform.position, NewPos, Time.deltaTime);
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
