using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class OpenDoorAgain: MonoBehaviour 
{
	public GameObject Door;

	public int DoorRot;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void FixedUpdate () {
		Quaternion NewDoor0Rot = Quaternion.Euler (0, DoorRot, 0);
		Door.transform.rotation = Quaternion.Slerp(Door.transform.rotation,NewDoor0Rot,Time.deltaTime*2);

	}
}
