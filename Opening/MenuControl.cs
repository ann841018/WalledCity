using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MenuControl : MonoBehaviour {
	public GameObject MenuUI,ManyMenuUI,MenuPictureUI;
	public Button[] Button,VerMenu,Menu;
	int ButtonNumber,VerMenuNumber,MenuNumber;
	bool CanInputH,CanInputV;
	bool CanInputHJ,CanInputVJ;

	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");float v = Input.GetAxis ("Vertical");
		float hj = Input.GetAxis ("HorizontalJoy");float vj = Input.GetAxis ("VerticalJoy");
		if (h == 0)CanInputH = true;if (hj == 0)CanInputHJ = true;
		if (v == 0)CanInputV = true;if (vj == 0)CanInputVJ = true;

		if (Input.GetKeyDown (KeyCode.W)) {ButtonNumber = ButtonNumber - 1;VerMenuNumber = VerMenuNumber - 1;MenuSound.bagchack = true;if (ButtonNumber == 2)ButtonNumber = 5;if (ButtonNumber == -1)ButtonNumber = 2;}
		if (Input.GetKeyDown (KeyCode.S)) {ButtonNumber = ButtonNumber + 1;VerMenuNumber = VerMenuNumber + 1;MenuSound.bagchack = true;if (ButtonNumber == 3)ButtonNumber = 0;if (ButtonNumber == 6)ButtonNumber = 3;}
		if (Input.GetKeyDown (KeyCode.A)) {ButtonNumber = ButtonNumber - 3;MenuNumber = MenuNumber - 1;MenuSound.bagchack = true;}
		if (Input.GetKeyDown (KeyCode.D)) {ButtonNumber = ButtonNumber + 3;MenuNumber = MenuNumber + 1;MenuSound.bagchack = true;}

		if (CanInputV == true) {
			if (v >= 1) {ButtonNumber = ButtonNumber - 1;VerMenuNumber = VerMenuNumber - 1;MenuSound.bagchack = true;
				if (ButtonNumber == 2)ButtonNumber = 5;if (ButtonNumber == -1)ButtonNumber = 2;CanInputV = false;
			}
			else if (v <= -1) {ButtonNumber = ButtonNumber + 1;VerMenuNumber = VerMenuNumber + 1;MenuSound.bagchack = true;
				if (ButtonNumber == 3)ButtonNumber = 0;if (ButtonNumber == 6)ButtonNumber = 2;CanInputV = false;
			}
		}
		if (CanInputVJ == true) {
			if (vj >= 1) {ButtonNumber = ButtonNumber + 1;VerMenuNumber = VerMenuNumber + 1;MenuSound.bagchack = true;
				if (ButtonNumber == 3)ButtonNumber = 0;if (ButtonNumber == 6)ButtonNumber = 3;CanInputVJ = false;
			}
			else if (vj <= -1) {ButtonNumber = ButtonNumber - 1;VerMenuNumber = VerMenuNumber - 1;MenuSound.bagchack = true;
				if (ButtonNumber == 2)ButtonNumber = 5;if (ButtonNumber == -1)ButtonNumber = 2;CanInputVJ = false;
			}
		}	
		if (CanInputH == true) {
			if (h >= 1) {MenuNumber = MenuNumber - 1;ButtonNumber = ButtonNumber - 3;CanInputH = false;MenuSound.bagchack = true;}
			else if (h <= -1) {MenuNumber = MenuNumber + 1;ButtonNumber = ButtonNumber + 3;CanInputH = false;MenuSound.bagchack = true;}
		}
		if (CanInputHJ == true) {
			if (hj >= 1) {MenuNumber = MenuNumber + 1;ButtonNumber = ButtonNumber + 3;CanInputHJ = false;MenuSound.bagchack = true;}
			else if (hj <= -1) {MenuNumber = MenuNumber - 1;ButtonNumber = ButtonNumber - 3;CanInputHJ = false;MenuSound.bagchack = true;}
		}

		if (MenuNumber >= 2)MenuNumber = 0;if (MenuNumber <= -1)MenuNumber = 1;
		if (ButtonNumber == -3)ButtonNumber = 3;if (ButtonNumber == -2)ButtonNumber = 4;if (ButtonNumber == -1)ButtonNumber = 5;
		if (ButtonNumber == 6)ButtonNumber = 0;if (ButtonNumber == 7)ButtonNumber = 1;if (ButtonNumber == 8)ButtonNumber = 2;
		if (VerMenuNumber >= 4)VerMenuNumber = 0;if (VerMenuNumber <= -1)VerMenuNumber = 3;
		if (MenuUI.active == true){
			for (int i = 0; i < 4; i++) {
				VerMenu[i].interactable = false;
				if (VerMenuNumber == i) {
					VerMenu[VerMenuNumber].interactable = true;
					if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.Space)){VerMenu [VerMenuNumber].onClick.Invoke ();}

				}
			}
			ButtonNumber = 0;MenuNumber = 0;
		}
		if(ManyMenuUI.active == true){
			for (int i = 0; i < 6; i++) {
				Button[i].interactable = false;
				if (ButtonNumber == i) {
					Button[ButtonNumber].interactable = true;
					if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.Space)){Button [ButtonNumber].onClick.Invoke ();}
				}
			}
			VerMenuNumber = 0;MenuNumber = 0;
		}
		if(MenuPictureUI.active == true){
			for (int i = 0; i < 2; i++) {
				Menu[i].interactable = false;
				if (MenuNumber == i) {
					Menu[MenuNumber].interactable = true;
					if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.Space)){Menu[MenuNumber].onClick.Invoke ();}
				}
			}
			VerMenuNumber = 0;ButtonNumber = 0;
		}
	}
}