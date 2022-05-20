using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {

	public GameObject Serch;
	public GameObject SerchText;
	public GameObject Door;//外門
	public GameObject Gate;//鐵門
	public GameObject CubeCollider;//碰撞
	public GameObject PlayAudio;
	public Transform NewPos;
	bool OpenGate;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {
		Door.transform.rotation = Quaternion.Slerp(Door.transform.rotation, Quaternion.Euler (0, -90, 0), Time.deltaTime);//外門打開
		Serch.SetActive (true);SerchText.SetActive (false);//調查的UI
		if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)) {OpenGate = true;}//按圈圈或E
		if(OpenGate == true){
			PlayAudio.SetActive (true);
			Serch.SetActive (false);SerchText.SetActive (true);//調查的UI
			Gate.transform.position = Vector3.Slerp (Gate.transform.position, NewPos.position, Time.deltaTime);//鐵門橫移
			CubeCollider.SetActive (false);//物理碰撞關掉
		}
	}
}
