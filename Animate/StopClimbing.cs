using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopClimbing : MonoBehaviour {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerStay(Collider Other)	{
		if (Other.tag == "Player") {Player.myAnim.SetBool ("CanClimb", false);
			Other.GetComponent<SphereCollider>().enabled = true;}
	}
}
