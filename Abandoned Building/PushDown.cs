using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDown : MonoBehaviour {

	public GameObject Cabinet;
	 

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		Quaternion Rot = Quaternion.Euler (90, Cabinet.transform.rotation.eulerAngles.y, Cabinet.transform.rotation.eulerAngles.z);
		Cabinet.transform.rotation = Quaternion.Slerp (Cabinet.transform.rotation, Rot, Time.deltaTime*2);
	}
}
