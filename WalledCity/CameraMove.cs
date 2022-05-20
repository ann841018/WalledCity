using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public Transform Target;//梅
	public Transform Original;//原始座標

	RaycastHit Hit;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("HorizontalCam");//讀取右手搖桿水平移動值
		float v = Input.GetAxis ("VerticalCam");//讀取右手搖桿垂直移動值

		Vector3 aim = Target.position;
		Vector3 face = (Target.position - transform.position).normalized;//用射線去判斷
		float angle = transform.eulerAngles.y;
		aim = aim - face*angle;
		//Debug.DrawLine (Target.position, aim, Color.blue);

		if(Physics.Linecast(Target.position,aim,out Hit))
		{
			if (Hit.collider.tag == "Ground" || Hit.collider.tag == "Build" || Hit.collider.tag == "buildDoor" || Hit.collider.tag == "Untagged") {transform.position = Hit.point;}
		}
		transform.position = Vector3.Slerp (transform.position, Original.position, Time.deltaTime*2);
	}
}
