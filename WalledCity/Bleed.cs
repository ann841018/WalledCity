using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : MonoBehaviour {

	public int EnemyNumber;

	// Update is called once per frame
	void Update () {}
	void OnTriggerEnter(Collider Other)	{if (Other.tag == "Bullet") {SystemControl.EnemyHP[EnemyNumber] = SystemControl.EnemyHP[EnemyNumber] - 20;}}//被子彈打到
	void OnTriggerExit(Collider Other){if (Other.tag == "Bullet") {SystemControl.EnemyHP[EnemyNumber] = SystemControl.EnemyHP[EnemyNumber] - 20;}}//被子彈打到
}
