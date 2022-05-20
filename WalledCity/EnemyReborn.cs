using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReborn : MonoBehaviour {

	public GameObject EnemyDeadObject,EnemyOriginal;//屍體
	float time;

	// Update is called once per frame
	void FixedUpdate () {
		time = time + Time.deltaTime;
		if (time >= 90) {
			EnemyOriginal.SetActive (true);//原本打開
			EnemyDeadObject.SetActive (false);//屍體關掉
			time = 0;
		}
	}
}
