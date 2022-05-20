using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StartStory : MonoBehaviour {

	public GameObject Camera;//相機物件
	public Flowchart talkFlowchart;
	public string playerInString;
	public static bool JustStart = true;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter(Collider Other)
	{
		if (Other.tag == "Player") {
			Player.CanMove = false;Enemy.canMove = false;
			EnemyMelee.canMove = false;EnemyGuard.canMove = false;EnemyGuardMelee.canMove = false;
			Camera.GetComponent<CameraControl>().enabled = false;//角色敵人相機不能動
			Block targetBlock = talkFlowchart.FindBlock (playerInString);//Fungus
			talkFlowchart.ExecuteBlock (targetBlock);
			Player.FromSceneNumber = 1;
		}
	}
	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player") {
			Player.CanMove = false;Enemy.canMove = false;
			EnemyMelee.canMove = false;EnemyGuard.canMove = false;EnemyGuardMelee.canMove = false;
			Camera.GetComponent<CameraControl>().enabled = false;//角色敵人相機不能動
			Block targetBlock = talkFlowchart.FindBlock (playerInString);//Fungus
			talkFlowchart.ExecuteBlock (targetBlock);
			Player.FromSceneNumber = 1;
		}
	}
}
