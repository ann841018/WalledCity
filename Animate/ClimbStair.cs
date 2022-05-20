using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbStair : MonoBehaviour {

	public GameObject SerchUI;
	public GameObject SerchText;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter(Collider Other)	{
		if (Other.tag == "Player") {SerchUI.SetActive (true);SerchText.SetActive (false);}
	}
	void OnTriggerStay(Collider Other)	{
		if (Other.tag == "Player") {
			if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {
				Player.Climb = true;Player.myAnim.SetBool ("CanClimb", true);
				SerchUI.SetActive (false);SerchText.SetActive (true);} 
		}else {SerchUI.SetActive (false);SerchText.SetActive (true);}
	}
	void OnTriggerExit(Collider Other)	{
		if (Other.tag == "Player") {Player.Climb = false;SerchUI.SetActive (false);SerchText.SetActive (true);}//被梅打
	}
}
