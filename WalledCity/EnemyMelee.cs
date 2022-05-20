using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour {
	public GameObject EnemyDeadObject,EnemyOriginal,Gate;//屍體
	public Transform PlayerRayPoint,EnemyRayPoint;//角色座標
	public Transform[] Target;//目標座標
	public static bool canMove,PlayerInSight,BeAtttract,IsAttack;//是否可以移動
	public int EnemyNumber;//敵人編號
	bool attacktalk;
	bool foundtalk;
	bool strangetalk;
	public enemySoundEffect  soundscript;

	float[] TargetDistance = new float[3],AllTargetDistance = new float[10]; //全部座標的距離
	float time,DelayTime,AttractTime,AttackTime,StayTime;//死掉延遲時間//被吸引時間
	bool EnemyDead,EnemyDeadRightNow,EnemyDeadByHit;//角色是否在視線內//敵人是否暈倒
	bool IsChase,Flashed;//是否追角色 自動尋路
	int TargetNumber = 1;//敵人目標

	Vector3[] AllTarget = new Vector3[10] {new Vector3(-6.61f,0,-6.87f),new Vector3(-16.88f,0,19.81f),new Vector3(-36.11f,0,-5.85f),new Vector3(-37.44f,0,-27.22f),new Vector3(22.16f,0,-30.61f),new Vector3(6.29f,0,10.34f),new Vector3(-17.44f,0,-30.61f),new Vector3(28.83f,0,1.39f),new Vector3(-22.76f,0,7.39f),new Vector3(-25.97f,0,31.03f)};//目標座標
	Vector3 PlayerLastInSight;NavMeshAgent EnemyNav;RaycastHit EnemyHit;//自動尋路 射線
	Rigidbody EnemyRigidbody;Collider EnemyCollider;Animator EnemyAnim;//鋼體 碰撞體 動畫

	void Start () {EnemyCollider = GetComponent<Collider> ();EnemyRigidbody = GetComponent<Rigidbody> ();EnemyAnim = GetComponent<Animator> ();EnemyNav = GetComponent<NavMeshAgent> ();soundscript = GetComponent<enemySoundEffect>();}
	void FixedUpdate () {if (canMove == true && EnemyDead == false && Player.Dead == false) {time = time + Time.deltaTime;//一開始不能自動尋路
		if (time >= 3.2f) {EnemyNav.enabled = true;for (int i = 0; i < 3; i++) {TargetDistance [i] = Vector3.Distance (Target [i].position, transform.position);}//目標座標
		if (BeAtttract == true) {if (TargetDistance[0] > 1.5f)Go (0);else if (TargetDistance[0] <= 1.5f)Attract ();}//被道具吸引
		else if (BeAtttract == false){Target[0].position = PlayerLastInSight;if(Player.UseMask == false && Player.Run == true){if (Vector3.Distance (Player.Mei.position, transform.position) < 10) {transform.LookAt (Player.Mei.transform);}}//角色跑步的話聽到腳步聲(用距離判斷)
		if(Enemy.IsAttack == true||EnemyMelee.IsAttack == true||EnemyGuard.IsAttack == true||EnemyGuardMelee.IsAttack == true)transform.LookAt (Player.Mei.transform);
		if (TargetDistance [TargetNumber] <= EnemyNav.stoppingDistance) {if (TargetNumber != 0) {//到目標座標後
			EnemyAnim.SetBool("Serch",true);EnemyAnim.SetBool ("Walk", false);StayTime = StayTime + Time.deltaTime;//搜尋動畫
			if (StayTime >= 5) {EnemyAnim.SetBool("Serch",false);TargetNumber = TargetNumber + 1;StayTime = 0;}	if (TargetNumber >= 3)TargetNumber = 1;}}//到達座標後前往下一個
		else if (TargetDistance [TargetNumber] > EnemyNav.stoppingDistance)Go (TargetNumber);//移動到目標
		Vector3 aim = PlayerRayPoint.position;Vector3 face = (EnemyRayPoint.position - PlayerRayPoint.position).normalized;//距離
		float angle = transform.eulerAngles.y;aim = aim - face*angle;if (Physics.Linecast (EnemyRayPoint.position, aim, out EnemyHit)) {//敵人往角色身上設一條射線
			if (EnemyHit.collider.gameObject.tag == "Player") {if(foundtalk == false){soundscript.enemyfirstfound = true;foundtalk = true;}/*發現語音*/ PlayerInSight = true;if(Player.UseMask == false){IsChase = true;
			if (Vector3.Distance (Player.Mei.position, transform.position) <= 1) {IsAttack = true;Attack ();}//攻擊
			else if (Vector3.Distance (Player.Mei.position, transform.position) > 1) {AttackTime = 2;IsAttack = false;EnemyAnim.SetBool ("Alart", false);Go (0);}}//角色在視線內的話持續更新最後看到角色的位置
		else if (EnemyHit.collider.gameObject.tag != "Player") {if (IsChase == true) {//正在追角色
			if (EnemyHit.collider.gameObject.tag != "Enemy") {PlayerInSight = false;Go (0);if (TargetDistance[0] <= 1) {Serch ();Go (0);}}//移動到最後看到角色的位置
			else if (EnemyHit.collider.gameObject.tag == "Enemy") {if (TargetDistance[0] > 1) Go (0);else transform.LookAt (Player.Mei.transform);}}//面向角色
		else Go (TargetNumber);}}}//追角色的途中被障礙物擋住
		if(PlayerInSight == true){PlayerLastInSight = Player.Mei.position;Gate.SetActive(false);}}}}//角色在視線內的話持續更新最後看到角色的位置 //不能進室內
		else if (canMove == false) {
			EnemyNav.enabled = false;EnemyAnim.SetBool ("Walk", false);EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool("Serch",false);EnemyAnim.SetBool("Alart",false);
			if (Player.IsFlash == true) {if (Flashed == false) {EnemyAnim.Play ("Flashed");Flashed = true;}}else {EnemyAnim.Play("Idle");Flashed = false;}//被相機閃到
			if (EnemyDead == true) {DelayTime = DelayTime + Time.deltaTime;DeadDelay ();}//如果敵人死亡
		}
		else if (EnemyDead == true) {DelayTime = DelayTime + Time.deltaTime;DeadDelay ();}//如果敵人死亡
		else if (Player.Dead == true) {EnemyAnim.enabled = false;EnemyNav.enabled = false;canMove = false;}//如果角色死亡
		else if (Player.UseMask == true){if (EnemyDead == true) {DelayTime = DelayTime + Time.deltaTime;DeadDelay ();}}//如果角色戴面具時敵人死亡
		if (SystemControl.EnemyHP[EnemyNumber] <= 0){EnemyDead = true;}if (SystemControl.EnemyHP[EnemyNumber] <= -5) {EnemyDead = true;EnemyDeadRightNow = true;}//敵人暈倒
	}
	
	void Attack(){if (canMove == true && EnemyDead == false && Player.Dead == false) {//攻擊角色
		EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool ("Walk", false);EnemyAnim.SetBool ("Check", false);EnemyAnim.SetBool("Serch",false);EnemyAnim.SetBool ("Alart", true);//緊戒動畫
		enemySoundEffect.run = false;transform.LookAt (Player.Mei.transform);EnemyNav.enabled = false;//面向角色 攻擊
		Vector3 aim = PlayerRayPoint.position;Vector3 face = (EnemyRayPoint.position - PlayerRayPoint.position).normalized;//距離
		float angle = transform.eulerAngles.y;aim = aim - face*angle;//朝角色方向射一條射線
		if (Physics.Linecast (EnemyRayPoint.position, aim, out EnemyHit)) {if (EnemyHit.collider.gameObject.tag == "Player") {//敵人跟角色之間沒有障礙物
			if (attacktalk == false) {soundscript.enemywantattack = true; attacktalk = true;}//敵人語音
			AttackTime = AttackTime + Time.deltaTime;if (AttackTime >= 2){//攻擊CD
			EnemyAnim.Play("Attack");Player.myAnim.Play("GetHit");enemySoundEffect.Knifeattack = true;//攻擊動畫
			Player.HP = Player.HP - 5;Player.BeAttack = true;Player.BeAttackTime = 0;//角色扣血
			AttackTime = 0;}}else{EnemyAnim.SetBool ("Alart", false);IsAttack = false;}}//中間有障礙物就不攻擊
		}
	}
	void Serch(){if (canMove == true && EnemyDead == false && Player.Dead == false && Player.UseMask == false) {//搜尋
		EnemyNav.enabled = false;AttractTime = AttractTime + Time.deltaTime;//被吸引的時間
		int No1 = 0, No2 = 0;float No1Dis = 20, No2Dis = 20;//目標位置及編號
		if (IsChase == false) {for (int i = 0; i < 10; i++) {AllTargetDistance [i] = Vector3.Distance (AllTarget [i], transform.position);//找所有目標裡面
		if (AllTargetDistance [i] < No1Dis) {No2Dis = No1Dis;No2 = No1;No1Dis = AllTargetDistance[i];No1 = i;}else if (AllTargetDistance [i] < No2Dis) {No2Dis = AllTargetDistance[i];No2 = i;}}//離敵人最近的兩個目標
		Target [1].position = AllTarget [No1];Target [2].position = AllTarget [No2];IsChase = true;}//那兩個目標變成新的巡邏點
		EnemyAnim.SetBool ("Alart", false);EnemyAnim.SetBool ("Check", false);EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool("Serch",true);enemySoundEffect.run = false;//尋找動畫
			if (AttractTime >= 5) {strangetalk = false; attacktalk = false;foundtalk = false;TargetNumber = (int)(Random.Range (1, 3));EnemyAnim.SetBool("Serch",false);AttractTime = 0;}}//超過一定的時間後 隨機前往其他巡邏點重新巡邏
	}
	void Attract(){if (canMove == true && EnemyDead == false && Player.Dead == false) {//被道具吸引
		EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool ("Check", false);EnemyAnim.SetBool("Serch",false);EnemyAnim.SetBool ("Alart", true);enemySoundEffect.run = false;transform.LookAt (Target[0].position);//面向道具
		Vector3 ItemPos = new Vector3 (Target[0].position.x, Target[0].position.y+1, Target[0].position.z);EnemyNav.enabled = false;
		Vector3 aim = Target[0].position;Vector3 face = (EnemyRayPoint.position - ItemPos).normalized;//距離
		float angle = transform.eulerAngles.y;aim = aim - face * angle;//朝角色方向射一條射線
		if (Physics.Linecast (EnemyRayPoint.position, aim, out EnemyHit)) {
			if (EnemyHit.collider.gameObject.tag == "1") {//敵人跟道具之間沒有障礙物
				AttackTime = AttackTime + Time.deltaTime;if (AttackTime >= 1){EnemyAnim.Play("Attack");//子彈CD
				AttractItem.BillboardHP = AttractItem.BillboardHP - 10;	AttackTime = 0;}}
				else if (EnemyHit.collider.gameObject.tag == "0"||EnemyHit.collider.gameObject.tag == "5") {transform.LookAt (Target[0].position);}
			else{EnemyAnim.SetBool ("Alart", false);BeAtttract = false;}}//中間有障礙物就不攻擊
		}
	}
	void Go(int TargetNumber){if(canMove==true && EnemyDead == false && Player.Dead == false) {//移動
		EnemyNav.enabled = true;EnemyAnim.SetBool ("Alart", false);EnemyAnim.SetBool ("Check", false);IsChase = false;//動畫
		if (TargetNumber == 0) {EnemyNav.speed = 5;IsChase = true;EnemyAnim.SetBool ("Run", true); EnemyAnim.SetBool ("Walk", false);enemySoundEffect.run = true;} //追角色
		else if (TargetNumber != 0) {EnemyNav.speed = 3;IsChase = false;EnemyAnim.SetBool ("Walk", true);EnemyAnim.SetBool ("Run", false);enemySoundEffect.run = true;}//移動
		EnemyNav.SetDestination (Target [TargetNumber].position);}//前往目標座標
	}
	void OnTriggerEnter(Collider Other)	{
		if (Other.tag == "Bullet") {if (PlayerInSight == true) {Player.CanUseMask = false;Player.UseMask = false;} 
		else {if (IsChase == false) {EnemyNav.enabled = false;EnemyAnim.SetBool ("Check", true);}}
		SystemControl.EnemyHP[EnemyNumber] = SystemControl.EnemyHP[EnemyNumber] - 5;time = 0;}//被子彈打到
		if (Other.tag == "PlayerHand") {EnemyNav.enabled = false;EnemyAnim.Play ("GetHit");SystemControl.EnemyHP[EnemyNumber] = 0;EnemyDeadByHit = true;time = 0;}//被梅打
		if (Other.tag == "PlayerFoot") {EnemyNav.enabled = false;EnemyAnim.SetBool ("Alart", false);EnemyAnim.Play ("GetKicked");time = 0;}//被梅踢
		if (Other.tag == "1" || Other.tag == "0"|| Other.tag == "5") {
			if (strangetalk == false) {soundscript.enemystrange = true;strangetalk = true;}
			Vector3 ItemPos = new Vector3 (Other.transform.position.x, Other.transform.position.y + 1, Other.transform.position.z);
			Vector3 aim = Other.transform.position;
			Vector3 face = (EnemyRayPoint.position - ItemPos).normalized;//距離
			float angle = transform.eulerAngles.y;
			aim = aim - face * angle;//朝角色方向射一條射線
			if (Physics.Linecast (EnemyRayPoint.position, aim, out EnemyHit)) {
				if (EnemyHit.collider.gameObject.tag == "1" || EnemyHit.collider.gameObject.tag == "0"||EnemyHit.collider.gameObject.tag == "5") {//敵人跟角色之間沒有障礙物
					BeAtttract = true;
					Target[0].position = Other.gameObject.transform.position;
				}
			}
		}
		if (Other.tag == "2") {EnemyAnim.Play("GetKicked");SystemControl.EnemyHP[EnemyNumber] = 0;}//被保齡球打
	}
	void OnTriggerStay(Collider Other){if (Other.tag == "1" || Other.tag == "0"|| Other.tag == "5") {BeAtttract = true;Target[0].position = Other.gameObject.transform.position;}}//被道具吸引
	void DeadDelay(){EnemyNav.enabled = false;canMove = false;PlayerInSight = false;IsAttack = false;BeAtttract = false;//死掉
		if (EnemyDeadRightNow == true) {EnemyAnim.SetBool ("Check", false);EnemyAnim.Play ("Dead");attacktalk = false;foundtalk = false;strangetalk = false;//被打到頭的話//死掉動畫
			Quaternion Rot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);//倒下
			EnemyDeadObject.transform.rotation = Rot;EnemyDeadObject.transform.position = transform.position;//屍體角度座標
			canMove = true;EnemyDead = false;EnemyDeadRightNow = false;SystemControl.EnemyHP[EnemyNumber] = 10;DelayTime = 0;time = 0;//設回初始值
			EnemyOriginal.SetActive (false);EnemyDeadObject.SetActive (true);//換成屍體物件
		}else if (EnemyDeadByHit == true) {if (DelayTime >= 1) {EnemyAnim.Play ("Dead");attacktalk = false;foundtalk = false;strangetalk = false;//被偷襲的話//死掉動畫
			Quaternion Rot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);//倒下
			EnemyDeadObject.transform.rotation = Rot;EnemyDeadObject.transform.position = transform.position;//屍體角度座標
			canMove = true;EnemyDead = false;EnemyDeadRightNow = false;SystemControl.EnemyHP[EnemyNumber] = 10;DelayTime = 0;time = 0;//設回初始值
			EnemyOriginal.SetActive (false);EnemyDeadObject.SetActive (true);}//換成屍體物件
		}else {if (DelayTime >= 2) {EnemyAnim.SetBool ("Check", false);EnemyAnim.Play ("Dead");attacktalk = false;foundtalk = false;strangetalk = false;//死掉動畫
			Quaternion Rot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);//倒下
			EnemyDeadObject.transform.rotation = Rot;EnemyDeadObject.transform.position = transform.position;//屍體角度座標
			canMove = true;EnemyDead = false;EnemyDeadRightNow = false;SystemControl.EnemyHP[EnemyNumber] = 10;DelayTime = 0;time = 0;//設回初始值
			EnemyOriginal.SetActive (false);EnemyDeadObject.SetActive (true);}//換成屍體物件
			else {EnemyAnim.SetBool ("Walk", false);EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool ("Alart", false);EnemyAnim.SetBool("Serch",false);}//不能移動//動畫關掉
		}
	}
}