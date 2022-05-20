using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkFoward : MonoBehaviour {
	float time;

	// Use this for initialization
	void Start () {time = 0;
		gameObject.AddComponent<Rigidbody> ();
		if (this.gameObject.tag == "2") {OptionControl.ItemCount = OptionControl.ItemCount - 1;}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time = time + Time.deltaTime;
		if (this.gameObject.tag == "2") {
			GetComponent<AudioSource> ().enabled = true;
			if (time >= 1) {
				OptionControl.UIOnHeadNumber = 0;;
				gameObject.GetComponent<Rigidbody> ().mass = 10;
				gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				transform.Translate (0, 0, 0.1f);//往前
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
				if (time >= 10)Destroy (this.gameObject);
			}else if (time > 0.85f) {transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y, 0);transform.parent = null;}
		}
		if (this.gameObject.tag == "0"||this.gameObject.tag == "5") {
			if(time<0.1f)transform.rotation =  Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y, 0);
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ;
			if (time <= 10) {transform.Translate (0, 0, 0.03f);GetComponent<AudioSource> ().enabled = true;}//往前
			else if(time<=20){
				Quaternion rot = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y, -90);
				transform.rotation =  Quaternion.Slerp (transform.rotation,rot,Time.deltaTime*10);
				gameObject.GetComponent<AudioSource> ().enabled = false;
				gameObject.GetComponent<SphereCollider> ().enabled = false;
				Enemy.BeAtttract = false;EnemyGuard.BeAtttract = false;
				EnemyMelee.BeAtttract = false;EnemyGuardMelee.BeAtttract = false;
			}else this.gameObject.tag = "Untagged";
		}
	}
	void OnTriggerEnter(Collider Other){if (this.gameObject.tag == "2") {if (Other.tag == "Enemy") {Destroy (this.gameObject);PlayerSound.bowlingStrike = true;}}}
}
