using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CloseDoor: MonoBehaviour 
{
	public GameObject Door;
	public GameObject LockDoor;

	public int DoorRot;

	bool Open = true;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void FixedUpdate () {
		if (Open == false) {
			Door.GetComponent<OpenDoor> ().enabled = false;
			Door.GetComponent<BoxCollider> ().enabled = true;
			LockDoor.SetActive (true);
			Quaternion NewDoor0Rot = Quaternion.Euler (0, DoorRot, 0);
			Door.transform.rotation = Quaternion.Slerp(Door.transform.rotation,NewDoor0Rot,Time.deltaTime*2);
			Door.GetComponent<BoxCollider> ().enabled = true;
		}
	}

	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			if (Open == true) {//沒裝上
				Open = false;//裝上
			} 
		}
	}
}
