using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetIn : MonoBehaviour 
{
	public GameObject Serch;
	public GameObject Loading;
	public GameObject LoadLevelObject;
	public GameObject LoadingImage;

	void OnTriggerEnter(Collider other){if (other.tag == "Player")Serch.SetActive(true);}
	void OnTriggerStay(Collider other) {if (other.tag == "Player") {
		if (Input.GetKey (KeyCode.JoystickButton2) || Input.GetKey (KeyCode.E))
		{
			Serch.SetActive (false);
			Loading.SetActive(true);
			LoadLevelObject.SetActive (true);
			LoadingImage.SetActive(true);}
		}
	}
	void OnTriggerExit(Collider other){if (other.tag == "Player")Serch.SetActive(false);}
}