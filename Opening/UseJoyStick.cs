using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseJoyStick : MonoBehaviour {
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 11; i++) {SystemControl.EnemyDead [i] = false;}
		Player.UseJoystick = true;Player.Dead = false;
		Player.HP = 200;Player.FromSceneNumber = 0;
	}
}
