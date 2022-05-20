using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class InsideMenuControl : MonoBehaviour
{
	public Button[] Menu;

	int MenuNumber;
	bool CanInputV,CanInputVJ;

	// Use this for initialization
	void Start () {Player.CanMove = false;}
	
	// Update is called once per frame
	void FixedUpdate () {
		float v = Input.GetAxis ("Vertical");
		float vj = Input.GetAxis ("VerticalJoy");
		if (v == 0)CanInputV = true;
		if (vj == 0)CanInputVJ = true;

		if (Input.GetKeyDown (KeyCode.W)) {MenuNumber = MenuNumber - 1;}
		if (Input.GetKeyDown (KeyCode.S)) {MenuNumber = MenuNumber + 1;}

		if (CanInputV == true) {
			if (v >= 1) {MenuNumber = MenuNumber - 1;CanInputV = false;}
			else if (v <= -1) {MenuNumber = MenuNumber + 1;CanInputV = false;}
		}
		if (CanInputVJ == true) {
			if (vj >= 1) {MenuNumber = MenuNumber + 1;CanInputVJ = false;}
			else if (vj <= -1) {MenuNumber = MenuNumber - 1;CanInputVJ = false;}
		}

		if (MenuNumber <= -1)MenuNumber = 1;if (MenuNumber >= 2)MenuNumber = 0;

		for (int i = 0; i < 2; i++) {
			Menu[i].interactable = false;
			if (MenuNumber == i) {
				Menu[MenuNumber].interactable = true;
				if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.Space)){Player.CanMove = false;Menu[MenuNumber].onClick.Invoke ();Player.CanMove = true;}
			}
		}
	}
}
