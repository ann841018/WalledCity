using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour {

	public Transform TargetPosition;
	public Transform OriPosition;
	public GameObject Array;
	public int DistanceNumber;
	float Distance,TargetDistance;
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 MoveDistnce = (transform.position - TargetPosition.position).normalized;//距離;
		Distance = Vector3.Distance (OriPosition.position, transform.position);
		TargetDistance = Vector3.Distance (transform.position, TargetPosition.position);

		if(Distance<DistanceNumber)transform.position = transform.position - MoveDistnce/2;
		else transform.position = Vector3.Slerp(transform.position,OriPosition.position,Time.deltaTime*0.1f);
		transform.LookAt (TargetPosition.transform);
		if (TargetDistance <= 1)Array.SetActive (false);else Array.SetActive (true);
	}
}
