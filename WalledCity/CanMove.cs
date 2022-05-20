using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMove : MonoBehaviour {

	public GameObject Camera;//相機物件

	// Use this for initialization
	void Start () 
	{
		Player.CanMove = true;
		Enemy.canMove = true;
		EnemyMelee.canMove = true;
		EnemyGuard.canMove = true;
		EnemyGuardMelee.canMove = true;;
		StartStory.JustStart = false;
		Camera.GetComponentInChildren<CameraMove> ().enabled = true;
		Camera.GetComponent<CameraControl>().enabled = true;
		this.gameObject.SetActive (false);
	}//角色敵人相機能動

	void FixedUpdate()
	{
		Player.FromSceneNumber = 1;
	}
}
