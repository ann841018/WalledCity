using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractItem : MonoBehaviour {

	public static int BillboardHP = 1000;

	// Use this for initialization
	void Start () {transform.position = new Vector3(transform.position.x,0,transform.position.z);}

	// Update is called once per frame
	void FixedUpdate () {
		if (BillboardHP <= 0) {
			Enemy.BeAtttract = false;
			EnemyGuard.BeAtttract = false;
			EnemyMelee.BeAtttract = false;
			EnemyGuardMelee.BeAtttract = false;
			Destroy (this.gameObject);
		}
	}
}
