using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiTalk : MonoBehaviour {

	public GameObject Mei,Ran,Jui,Uncle;


	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void FixedUpdate () {
		Jui.gameObject.GetComponent<Animator> ().SetBool ("Talk",false);
		Ran.gameObject.GetComponent<Animator> ().SetBool ("Talk",false);
		Uncle.gameObject.GetComponent<Animator> ().SetBool ("Talk",false);
		Mei.gameObject.GetComponent<Animator> ().SetBool ("Talk",true);
	}
}
