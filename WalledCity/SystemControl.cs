using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemControl : MonoBehaviour {

	public GameObject[] EnemyObject;//敵人
	public GameObject[] EnemyDeadBody;//敵人屍體
	public GameObject Mei,CameraControl,Gate,MainCamera,NewMainCamera;
	public GameObject MinimapUI,UIOnHead,StartStory,CanMove;
	public MusicPlayer musicoutside;

	public static Vector3[] EnemyDeadPos = new Vector3[11];//每個敵人的座標
	public static Quaternion[] EnemyDeadRot = new Quaternion[11];//每個敵人的角度
	public static bool[] EnemyDead = new bool[11];//每個敵人是否死掉
	public static int[] EnemyHP = new int[11];

	// Use this for initialization
	void Start () {for (int i = 0; i < 11; i++) {EnemyHP [i] = 10;}}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Enemy.PlayerInSight == false && EnemyGuard.PlayerInSight == false && EnemyMelee.PlayerInSight == false && EnemyGuardMelee.PlayerInSight == false ){Gate.SetActive(true);}
		if (Player.FromSceneNumber == 0) {
			StartStory.SetActive (true);CanMove.SetActive(false);
			Mei.transform.position = new Vector3 (6, 0, 25);//廢棄建築物門口
			Mei.transform.rotation = Quaternion.Euler (0, 0, 0);//面向外面
			CameraControl.transform.position = new Vector3 (6, 0, 25);//廢棄建築物門口
			CameraControl.transform.rotation = Quaternion.Euler (0, 0, 0);//面向外面
			OptionControl.ItemCount = 1;OptionControl.UIOnHeadNumber = 0;
			Player.BulletNumber = 30;Player.CameraNumber = 6;
			Player.UseGun = true;Player.UseCamera = false;Player.UseBowling = false;
			Player.UseMask = false;Player.CanUseMask = true;
			Enemy.IsAttack = false;EnemyGuard.IsAttack = false;
			EnemyMelee.IsAttack = false;EnemyGuardMelee.IsAttack = false;
			NewMainCamera.SetActive(false);MainCamera.SetActive(true);
		} 
		if (Player.FromSceneNumber  == 2) {//從室內走出來
			StartStory.SetActive (false);CanMove.SetActive(true);
			Mei.transform.position = new Vector3 (-23.72f, 0, -22);//廢棄建築物門口
			Mei.transform.rotation = Quaternion.Euler (0, 90, 0);//面向外面
			CameraControl.transform.position = new Vector3 (-23.72f, 0, -22);//廢棄建築物門口
			CameraControl.transform.rotation = Quaternion.Euler (0, 90, 0);//面向外面
			NewMainCamera.SetActive(false);MainCamera.SetActive(true);
			for (int i = 0; i < 11; i++) {
				EnemyObject [i].transform.position = EnemyDeadPos[i];EnemyObject [i].transform.rotation = EnemyDeadRot[i];//敵人座標面向跟離開前一樣
				if (EnemyDead [i] == true) {EnemyDeadBody [i].SetActive (true);EnemyObject [i].SetActive (false);}//死掉的敵人還是死掉
			}
		}else if (Player.FromSceneNumber == 3) {//解完所有室內出去到頂樓
			StartStory.GetComponent<SphereCollider>().enabled = false;CanMove.SetActive(true);
			MinimapUI.SetActive(false);UIOnHead.SetActive (false);//UI關掉
			Mei.transform.position = new Vector3 (-28.5f, 21, -22);//廢棄建築物頂樓
			Mei.transform.rotation = Quaternion.Euler (0, -90, 0);//面向外面
			CameraControl.transform.position = new Vector3 (-28.5f, 21, -22);//廢棄建築物頂樓
			CameraControl.transform.rotation = Quaternion.Euler (0, -90, 0);//面向外面
			MainCamera.SetActive(false);NewMainCamera.SetActive(true);
			musicoutside.BGMStop ();
		}

		for (int i = 0; i < 11; i++) {
			if (EnemyObject [i].active == false) {EnemyDead [i] = true;}else if (EnemyObject [i].active == true) {EnemyDead [i] = false;}//檢查每個敵人有沒有死掉
			if (EnemyDead [i] == true) {EnemyDeadPos [i] = EnemyObject [i].transform.position;EnemyDeadRot [i] = EnemyObject [i].transform.rotation;}//死掉存下屍體位置角度
		}
	}
}
