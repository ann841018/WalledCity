using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGuardMelee : MonoBehaviour {
	public GameObject EnemyDeadObject,EnemyOriginal,Gate;//屍體
	public Transform PlayerRayPoint,EnemyRayPoint,OriginalPos,TargetPos;//角色座標
	public static bool canMove,PlayerInSight,BeAtttract,IsAttack;//是否可以移動
	public int EnemyNumber;//敵人編號

	float TargetDistance,time,DelayTime,AttractTime,AttackTime;//死掉延遲時間//被吸引時間
	bool EnemyDead,EnemyDeadRightNow,EnemyDeadByHit;//角色是否在視線內//敵人是否暈倒
	bool IsChase,Flashed;//是否追角色 自動尋路
    bool attacktalk;
	bool foundtalk;
	public enemySoundEffect  soundscript;


	//bool strangetalk;
	Vector3 PlayerLastInSight;RaycastHit EnemyHit;NavMeshAgent EnemyNav;//射線 自動尋路
	Rigidbody EnemyRigidbody;Collider EnemyCollider;Animator EnemyAnim;//鋼體 碰撞體 動畫

	void Start () {EnemyCollider = GetComponent<Collider> ();EnemyRigidbody = GetComponent<Rigidbody> ();EnemyAnim = GetComponent<Animator> ();EnemyNav = GetComponent<NavMeshAgent> (); soundscript = GetComponent<enemySoundEffect>();}
	void FixedUpdate () {if (canMove == true && EnemyDead == false && Player.Dead == false) {time = time + Time.deltaTime;
			if (time >= 3.2f) {EnemyNav.enabled = true;enemySoundEffect.run = false;TargetDistance = Vector3.Distance (TargetPos.position, transform.position);//一開始不能自動尋路//目標座標
		if (BeAtttract == true) {if (TargetDistance > 1.5f) {Go (TargetPos.position, false);} else  if (TargetDistance <= 1.5f){Attract ();}}
		else if (BeAtttract == false){TargetPos.position = PlayerLastInSight;if(Player.UseMask == false && Player.Run == true){if (Vector3.Distance (Player.Mei.position, transform.position) < 10) {transform.LookAt (Player.Mei.transform);}}//角色跑步的話聽到腳步聲(用距離判斷)
		if(Enemy.IsAttack == true||EnemyMelee.IsAttack == true||EnemyGuard.IsAttack == true||EnemyGuardMelee.IsAttack == true)transform.LookAt (Player.Mei.transform);
		Vector3 aim = PlayerRayPoint.position;Vector3 face = (EnemyRayPoint.position - PlayerRayPoint.position).normalized;//距離//Debug.DrawLine (EnemyRayPoint.position, aim, Color.yellow);//顯示那條線
		float angle = transform.eulerAngles.y;aim = aim - face*angle;if (Physics.Linecast (EnemyRayPoint.position, aim, out EnemyHit)) {//朝角色方向射一條射線
						if (EnemyHit.collider.gameObject.tag == "Player") {if(foundtalk == false){soundscript.enemyfirstfound = true;foundtalk = true;}/*發現語音*/ PlayerInSight = true;if(Player.UseMask == false){IsChase = true;
		if (Vector3.Distance (Player.Mei.position, transform.position) <= 1) {IsAttack = true;Attack ();}
			else if (Vector3.Distance (Player.Mei.position, transform.position) > 1){AttackTime = 2;IsAttack = false;EnemyAnim.SetBool ("Alart", false);Go (TargetPos.position, true);}}//角色在視線內的話持續更新最後看到角色的位置
		else if (EnemyHit.collider.gameObject.tag != "Player") {if (IsChase == true) {
			if (EnemyHit.collider.gameObject.tag != "Enemy") {PlayerInSight = false;Go (TargetPos.position, true);if (TargetDistance <= 1) {Serch ();Go (OriginalPos.position, false);}} 
			else if (EnemyHit.collider.gameObject.tag == "Enemy") {if (TargetDistance > 1)Go (TargetPos.position, true);else transform.LookAt (Player.Mei.transform);}}
		else {Go (OriginalPos.position, false);}}}}//追角色的途中被障礙物擋住
		if(PlayerInSight == true){PlayerLastInSight = Player.Mei.position;Gate.SetActive(false);}}}}//角色在視線內的話持續更新最後看到角色的位置 //不能進室內
		else if (canMove == false) {//打開UI時 敵人不能動
		EnemyNav.enabled = false;EnemyAnim.SetBool ("Walk", false);EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool("Serch",false);EnemyAnim.SetBool("Alart",false);
			if (Player.IsFlash == true) {if (Flashed == false) {EnemyAnim.Play ("Flashed");Flashed = true;}}else {EnemyAnim.Play("Idle");Flashed = false;}//被相機閃到
			if (EnemyDead == true) {DelayTime = DelayTime + Time.deltaTime;DeadDelay ();}}//如果敵人死亡
		else if (EnemyDead == true) {DelayTime = DelayTime + Time.deltaTime;DeadDelay ();}//如果敵人死亡
		else if (Player.Dead == true) {EnemyAnim.enabled = false;EnemyNav.enabled = false;canMove = false;}//如果角色死亡
		if (SystemControl.EnemyHP[EnemyNumber] <= 0){EnemyDead = true;}if (SystemControl.EnemyHP[EnemyNumber] <= -5) {EnemyDead = true;EnemyDeadRightNow = true;}//敵人暈倒
	}
	void Attack(){if (canMove == true && EnemyDead == false && Player.Dead == false) {
		EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool ("Walk", false);EnemyAnim.SetBool ("Check", false);EnemyAnim.SetBool("Serch",false);EnemyAnim.SetBool ("Alart", true);
			enemySoundEffect.run = false;transform.LookAt (Player.Mei.transform);EnemyNav.enabled = false;
		Vector3 aim = PlayerRayPoint.position;Vector3 face = (EnemyRayPoint.position - PlayerRayPoint.position).normalized;//距離
		float angle = transform.eulerAngles.y;aim = aim - face * angle;//朝角色方向射一條射線
		if (Physics.Linecast (EnemyRayPoint.position, aim, out EnemyHit)) {if (EnemyHit.collider.gameObject.tag == "Player") {
					if (attacktalk == false) {soundscript.enemywantattack = true; attacktalk = true;}//敵人語音
			AttackTime = AttackTime + Time.deltaTime;if (AttackTime >= 2){//子彈CD
						EnemyAnim.Play("Attack");Player.myAnim.Play("GetHit");enemySoundEffect.Knifeattack = true;//攻擊動畫
			Player.HP = Player.HP - 10;Player.BeAttack = true;Player.BeAttackTime = 0;//角色扣血
			AttackTime = 0;}}else{EnemyAnim.SetBool ("Alart", false);IsAttack = false;}}//中間有障礙物就不攻擊
		}
	}
	void Serch(){if (canMove == true && EnemyDead == false && Player.Dead == false && Player.UseMask == false) {
		EnemyNav.enabled = true;AttractTime = AttractTime + Time.deltaTime;
			EnemyAnim.SetBool ("Alart", false);EnemyAnim.SetBool ("Check", false);EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool("Serch",true);enemySoundEffect.run = false;
			if (AttractTime >= 3) {/*strangetalk = false; */attacktalk = false;foundtalk = false;EnemyAnim.SetBool("Serch",false);IsChase = false;AttractTime = 0;}}//超過一定的時間後 回到原位重新站崗
	}
	void Attract(){if (canMove == true && EnemyDead == false && Player.Dead == false) {
		EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool ("Check", false);EnemyAnim.SetBool("Serch",false);
			enemySoundEffect.run = false;transform.LookAt (TargetPos.position);EnemyNav.enabled = false;
		Vector3 ItemPos = new Vector3 (TargetPos.position.x, TargetPos.position.y+1, TargetPos.position.z);
		Vector3 aim = TargetPos.position;Vector3 face = (EnemyRayPoint.position - ItemPos).normalized;//距離
		float angle = transform.eulerAngles.y;aim = aim - face * angle;//朝角色方向射一條射線
		if (Physics.Linecast (EnemyRayPoint.position, aim, out EnemyHit)) {
			if (EnemyHit.collider.gameObject.tag == "1") {//敵人跟角色之間沒有障礙物
				EnemyAnim.SetBool ("Alart", true);AttackTime = AttackTime + Time.deltaTime;if (AttackTime >= 1) {EnemyAnim.Play ("Attack");//子彈CD
				AttractItem.BillboardHP = AttractItem.BillboardHP - 5;AttackTime = 0;}} //角色扣血
			else if (EnemyHit.collider.gameObject.tag == "0"||EnemyHit.collider.gameObject.tag == "5") {transform.LookAt (TargetPos.position);}
			else{EnemyAnim.SetBool ("Alart", false);BeAtttract = false;}}//中間有障礙物就不攻擊
		}
	}
	void Go(Vector3 Pos,bool Target){if (canMove == true && EnemyDead == false && Player.Dead == false) {
		EnemyNav.enabled = true;EnemyAnim.SetBool ("Alart", false);EnemyAnim.SetBool ("Check", false);EnemyAnim.SetBool("Serch",false);
			if (Target == true){EnemyNav.speed = 6;IsChase = true;EnemyAnim.SetBool ("Run", true);enemySoundEffect.run = true;} //追角色
			else if (Target == false){IsChase = false;if (Vector3.Distance (OriginalPos.position, transform.position) <= 1) {transform.rotation = Quaternion.Slerp(transform.rotation,OriginalPos.rotation,Time.deltaTime);EnemyAnim.SetBool ("Walk", false);}else {EnemyAnim.SetBool ("Walk", true);enemySoundEffect.run = true;}} //回到原位
		EnemyNav.SetDestination (Pos);}
	}
	void OnTriggerEnter(Collider Other)	{
		if (Other.tag == "Bullet") {if (PlayerInSight == true) {Player.CanUseMask = false;Player.UseMask = false;} 
		else {if (IsChase == false) {EnemyNav.enabled = false;EnemyAnim.SetBool ("Check", true);}}
		SystemControl.EnemyHP[EnemyNumber] = SystemControl.EnemyHP[EnemyNumber] - 5;time = 0;}//被子彈打到
		if (Other.tag == "PlayerHand") {EnemyNav.enabled = false;EnemyAnim.Play ("GetHit");SystemControl.EnemyHP[EnemyNumber] = 0;EnemyDeadByHit = true;time = 0;}//被梅打
		if (Other.tag == "PlayerFoot") {EnemyNav.enabled = false;EnemyAnim.SetBool ("Alart", false);EnemyAnim.Play ("GetKicked");time = 0;}//被梅踢
		if (Other.tag == "1" || Other.tag == "0" || Other.tag == "5") {
			//if (strangetalk == false) {soundscript.enemystrange = true;strangetalk = true;}
			Vector3 ItemPos = new Vector3 (Other.transform.position.x, Other.transform.position.y + 1, Other.transform.position.z);
			Vector3 aim = Other.transform.position;Vector3 face = (EnemyRayPoint.position - ItemPos).normalized;//距離
			float angle = transform.eulerAngles.y;aim = aim - face * angle;//朝角色方向射一條射線
			if (Physics.Linecast (EnemyRayPoint.position, aim, out EnemyHit)) {
				if (EnemyHit.collider.gameObject.tag == "1" || EnemyHit.collider.gameObject.tag == "0"|| EnemyHit.collider.gameObject.tag == "5") {//敵人跟角色之間沒有障礙物
				BeAtttract = true;TargetPos.position = Other.gameObject.transform.position;}}
		}
		if (Other.tag == "2") {EnemyAnim.Play("GetKicked");SystemControl.EnemyHP[EnemyNumber] = 0;}
	}
	void OnTriggerStay(Collider Other){if (Other.tag == "1" || Other.tag == "0" || Other.tag == "5") {BeAtttract = true;TargetPos.position = Other.gameObject.transform.position;}else TargetPos.position = PlayerLastInSight;}
	void DeadDelay(){EnemyNav.enabled = false;canMove = false;PlayerInSight = false;IsAttack = false;BeAtttract = false;//死掉
		if (EnemyDeadRightNow == true) {EnemyAnim.SetBool ("Check", false);EnemyAnim.Play ("Dead");attacktalk = false;foundtalk = false;//strangetalk = false;//被打到頭的話//死掉動畫
			Quaternion Rot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);//倒下
			EnemyDeadObject.transform.rotation = Rot;EnemyDeadObject.transform.position = transform.position;//屍體角度座標
			canMove = true;EnemyDead = false;EnemyDeadRightNow = false;SystemControl.EnemyHP[EnemyNumber] = 10;DelayTime = 0;time = 0;//設回初始值
			EnemyOriginal.SetActive (false);EnemyDeadObject.SetActive (true);//換成屍體物件
		}else if (EnemyDeadByHit == true) {if (DelayTime >= 1) {EnemyAnim.Play ("Dead");attacktalk = false;foundtalk = false;//strangetalk = false;//被偷襲的話//死掉動畫
			Quaternion Rot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);//倒下
			EnemyDeadObject.transform.rotation = Rot;EnemyDeadObject.transform.position = transform.position;//屍體角度座標
			canMove = true;EnemyDead = false;EnemyDeadRightNow = false;SystemControl.EnemyHP[EnemyNumber] = 10;DelayTime = 0;time = 0;//設回初始值
			EnemyOriginal.SetActive (false);EnemyDeadObject.SetActive (true);}//換成屍體物件
		}else {if (DelayTime >= 2) {EnemyAnim.SetBool ("Check", false);EnemyAnim.Play ("Dead");attacktalk = false;foundtalk = false;//strangetalk = false;//死掉動畫
			Quaternion Rot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);//倒下
			EnemyDeadObject.transform.rotation = Rot;EnemyDeadObject.transform.position = transform.position;//屍體角度座標
			canMove = true;EnemyDead = false;EnemyDeadRightNow = false;SystemControl.EnemyHP[EnemyNumber] = 10;DelayTime = 0;time = 0;//設回初始值
			EnemyOriginal.SetActive (false);EnemyDeadObject.SetActive (true);}//換成屍體物件
			else {EnemyAnim.SetBool ("Walk", false);EnemyAnim.SetBool ("Run", false);EnemyAnim.SetBool ("Alart", false);EnemyAnim.SetBool("Serch",false);}//不能移動//動畫關掉
		}
	}
}