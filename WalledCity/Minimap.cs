using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public Transform Target;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		transform.position = Target.position;
		transform.position = new Vector3 (transform.position.x, 45, transform.position.z);
	}
}
