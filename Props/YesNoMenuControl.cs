using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class YesNoMenuControl : MonoBehaviour
{
	public Button[] Menu;

	int MenuNumber;
	bool CanInputH,CanInputHJ;

	// Use this for initialization
	void Start () {Player.CanMove = false;}

	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float hj = Input.GetAxis ("HorizontalJoy");
		if (h == 0)CanInputH = true;
		if (hj == 0)CanInputHJ = true;

		if (Input.GetKeyDown (KeyCode.A)) {MenuNumber = MenuNumber - 1;}
		if (Input.GetKeyDown (KeyCode.D)) {MenuNumber = MenuNumber + 1;}

		if (CanInputH == true) {
			if (h >= 1) {MenuNumber = MenuNumber - 1;CanInputH = false;}
			else if (h <= -1) {MenuNumber = MenuNumber + 1;CanInputH = false;}
		}
		if (CanInputHJ == true) {
			if (hj >= 1) {MenuNumber = MenuNumber + 1;CanInputHJ = false;}
			else if (hj <= -1) {MenuNumber = MenuNumber - 1;CanInputHJ = false;}
		}

		if (MenuNumber <= -1)MenuNumber = 1;if (MenuNumber >= 2)MenuNumber = 0;
		OptionControl.UseItemPanel = 1;
		for (int i = 0; i < 2; i++) {
			Menu[i].interactable = false;
			if (MenuNumber == i) {
				Menu[MenuNumber].interactable = true;
				if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.Space)){Player.CanMove = false;Menu[MenuNumber].onClick.Invoke ();Player.CanMove = true;}
			}
		}
	}
}
