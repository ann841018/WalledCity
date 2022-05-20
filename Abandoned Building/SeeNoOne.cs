using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeNoOne : MonoBehaviour {

	public GameObject SeeCamera;

	float time;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {
		time = time + Time.deltaTime;
		Quaternion NewRot0 = Quaternion.Euler (0, -90, 0);
		Quaternion NewRot1 = Quaternion.Euler (0, -60, 0);
		if (time < 0.5f) {SeeCamera.transform.rotation = Quaternion.Euler (0, 90, 0);
		}else if (time >= 0.5f && time <= 3) {
			SeeCamera.transform.rotation = Quaternion.Slerp (SeeCamera.transform.rotation, NewRot0, Time.deltaTime);
		}else if (time >= 3) {
			SeeCamera.transform.rotation = Quaternion.Slerp (SeeCamera.transform.rotation, NewRot1, Time.deltaTime*0.5f);
		}
	}
}
