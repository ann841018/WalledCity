using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSwitchDoor : MonoBehaviour {

	public GameObject SwitchDoor;
	public GameObject Lock;
	public GameObject LockCube;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {
		Quaternion NewRot = Quaternion.Euler (0, 210, 0);
		Quaternion NewCubeRot = Quaternion.Euler (0, 0, 120);
		Lock.SetActive (false);
		LockCube.transform.rotation = Quaternion.Slerp(LockCube.transform.rotation,NewCubeRot,Time.deltaTime);
		SwitchDoor.transform.rotation = Quaternion.Slerp(SwitchDoor.transform.rotation,NewRot,Time.deltaTime);
	}
}
