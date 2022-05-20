using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public GameObject AliveMei,DeadMei,Bullet,BulletReal,Feet,Hand;//活著跟死掉模型,被複製的子彈物件,可以打跟梯敵人的範圍
	public GameObject GameOverText,CameraAim0,CameraAim1,CameraAim2,CameraAim3,MainCamera,NewMainCamera,MeiHead0,MeiHead1,MeiHead2;//死掉的文字,相機瞄準UI,相機位置,角色UI
	public Transform Camera,BulletFather;//瞄準那台相機的位置 //子彈生成位置
	public Image DeadBlood,StaminaValue,Flash;//臨血的UI//跑步耐力值//相機閃光燈
	public Text BulletText;//子彈數量

	public static Transform Mei;//角色位置公開
	public static Animator myAnim;//角色動畫 
	public static int HP = 200,FromSceneNumber,BulletNumber = 30,CameraNumber = 6;//血量,在哪個場景,子彈數量,相機使用數量
	public static float BeAttackTime;//被攻擊之後回血的CD計算
	public static bool UseGun = true,UseBowling,UseMask,UseCamera,CanUseMask = true;//使用甚麼道具
	public static bool UseJoystick,CanMove,Dead,BeAttack,Run,Climb,IsFlash;//用搖桿玩,可以移動,死亡,正在被攻擊,跑步,攀爬,用閃光燈

	float SetSpeed = 3,Speed,BulletTime,HPRecover,DeadTime,RunTime = 100,FlashTime,AttackTime;//角色移動速度,子彈CD,HP回復速度,死亡時間,跑步耐力值,閃光燈時間,攻擊CD
	bool Grounded = true,CanRun,CanShoot,Stop;//是否在地上,是否可以跑,是否可以射擊,是否停止

	GameObject BulletClone,BulletRealClone;//複製出來的子彈物件
	Rigidbody myRigidbody;//角色鋼體 

	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();//設定角色鋼體
		myAnim = GetComponent<Animator> ();//設定角色動畫
		UseJoystick = true;//用搖桿玩
		if(UseJoystick==true) Cursor.visible = false;//隱藏滑鼠
		transform.position = new Vector3 (6, 0, 25);//設定腳色座標
		Speed = SetSpeed;//設定速度
	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");//左搖桿水平
		float v = Input.GetAxis ("Vertical");//左搖桿垂直
		float Cam = Input.GetAxis ("VerticalCam");//右搖桿垂直
		float AxisAim = Input.GetAxis ("AxisAim");//L2
		float AxisShoot = Input.GetAxis ("AxisShoot");//R2
		float n = 1 - HP*0.02f;//血量透明度

		Mei = transform;//腳色公開座標

		if (HP <= 0) {HP = 0;Dead = true;}
		HPRecover = HPRecover + Time.deltaTime;//血量介於0-100 //回血速度
		if (HP < 100) {
			DeadBlood.color = new Color (DeadBlood.color.r, DeadBlood.color.g, DeadBlood.color.b, n);//血量透明度
			MeiHead0.SetActive(false);//角色UI
			MeiHead1.SetActive(false);//角色UI
			MeiHead2.SetActive(true);//角色UI
			PlayerSound.heartbeat = true;//心跳
		} else {
			DeadBlood.color = new Color (DeadBlood.color.r, DeadBlood.color.g, DeadBlood.color.b, 0);//血量小於100才顯示血跡UI
			MeiHead0.SetActive(true);//角色UI
			MeiHead2.SetActive(false);//角色UI
		}

		if(HP>=100){PlayerSound.heartbeat = false;}//心跳聲
		if(HP<50){PlayerSound.heartbeatfast = true;}//更快的心跳聲
		if(HP>50){PlayerSound.heartbeatfast = false;}//回復普通心跳速度

		if (BeAttack == true) {BeAttackTime = BeAttackTime + Time.deltaTime; if (BeAttackTime > 5)BeAttack = false; }//被攻擊的UI
		else if (BeAttack == false) { BeAttackTime = 0;if (HP < 100) {if (HPRecover > 1) {HP = HP + 5;HPRecover = 0;}}}//回血

		Vector3 velocity = new Vector3 (0, 0, 0);//角色移動

		if (CanMove == true && Dead == false){//如果角色可以移動
			if (Grounded==true){//在地上
				GetComponent<CapsuleCollider>().radius = 0.3f;myAnim.SetBool("Fall", false);
				if (Climb == false) {
					myAnim.SetBool("CanClimb",false);
					if (UseJoystick == true) {//用搖桿玩
						if (v > 0) {//往前
							transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y + h * 90, 0);
							velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);FootStep.playermoving = true;myAnim.SetLayerWeight(1,1);
							if (Input.GetKey (KeyCode.JoystickButton5) || Input.GetKey (KeyCode.JoystickButton10))Run = true;} 
						else if (v == 0) {//左右
							if (h < 0) {transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y - 90, 0);velocity =  new Vector3 (0, 0, Speed);FootStep.playermoving = true;}//往左
							if (h > 0) {transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y + 90, 0);velocity =  new Vector3 (0, 0, Speed);FootStep.playermoving = true;}//往右
							if (h != 0){myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);}
							if (h == 0) {myAnim.SetFloat ("Speed", 0);myAnim.SetLayerWeight(1,0);FootStep.playermoving = false;FootStepRun.playermoving = false;Run = false;}} //角色動畫//腳步聲
						else if (v < 0){//往後
							transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y + 180 - h * 90, 0);
							velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);FootStep.playermoving = true;
							if (Input.GetKey (KeyCode.JoystickButton5) || Input.GetKey (KeyCode.JoystickButton10))Run = true;
						}velocity = transform.TransformDirection (velocity);transform.localPosition += velocity * Time.deltaTime;//角色往角色前方移動
					}else if (UseJoystick == false) {//用鍵盤玩
						if (Input.GetKey(KeyCode.W)) {transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y + h * 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);CanRun = true;FootStep.playermoving = true;} //往前
						else if (Input.GetKey(KeyCode.A)) {transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y - 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);CanRun = true;FootStep.playermoving = true;}//往左
						else if (Input.GetKey(KeyCode.D)) {transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y + 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);CanRun = true;FootStep.playermoving = true;}//往右
						else if (Input.GetKey(KeyCode.S)) {transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y + 180 - h * 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);CanRun = true;FootStep.playermoving = true;}//往後
						else {myAnim.SetFloat ("Speed", 0);myAnim.SetLayerWeight(1,0);CanRun = false;} if(CanRun == true){if (Input.GetKey (KeyCode.LeftShift))Run = true;FootStepRun.run = true;} //角色動畫
						velocity = transform.TransformDirection (velocity);transform.localPosition += velocity * Time.deltaTime;//角色往角色前方移動
					}
				}

				if (Input.GetKeyUp (KeyCode.JoystickButton5)) {Run = false;}//按左邊搖桿或R1跑步
				if(Stop == true){
					Run = false;Speed = 1;myAnim.SetBool ("Stop", true);myAnim.SetBool ("Run", false);
					RunTime = RunTime + Time.deltaTime*40;FootStepRun.playermoving = false;
					MeiHead0.SetActive(false);MeiHead1.SetActive(true);
				}
				if(Run == false){//不跑步
					if (Stop == false){
						RunTime = RunTime + Time.deltaTime*20;
						FootStepRun.run = false;FootStep.run = false;
						myAnim.SetBool ("Run", false);Speed = SetSpeed;//移動速度變回普通速度
					}
				}else if (Run == true) {
					myAnim.SetLayerWeight(1,1);
					RunTime = RunTime - Time.deltaTime*10;
					FootStep.run =true;FootStepRun.run = true;FootStepRun.playermoving = true;
					myAnim.SetBool ("Run", true);Speed = SetSpeed + 3;//跑步時速度加快
					if (UseJoystick == true) {if (v == 0 && h == 0) Run = false; }//不跑步
					else if(UseJoystick == false){if(CanRun==false)Run = false;}//不跑步
				}if(RunTime > 100){
					RunTime = 100;Stop = false;myAnim.SetBool ("Stop", false);
					MeiHead0.SetActive(true);MeiHead1.SetActive(false);
				}
				if (RunTime < 0){RunTime = 0;Run = false;Stop = true;}
				StaminaValue.fillAmount = RunTime*0.01f;

				if (AxisAim <= -1||Input.GetMouseButton (1)) {//拿著槍//按L2或右
					Run = false;Speed = SetSpeed - 0.5f;transform.rotation = Quaternion.Euler (0, CameraControl.CameraControlTra.rotation.eulerAngles.y+20f, 0);//角色角度等於相機角度
					myAnim.SetBool ("Run", false);myAnim.SetBool ("Jump", false);//移動速度放慢
					if (CameraControl.CameraControlTra.rotation.eulerAngles.x < 180) {myAnim.SetFloat ("Angle", CameraControl.CameraControlTra.transform.rotation.eulerAngles.x);}//瞄準角度(往下)
					else if (CameraControl.CameraControlTra.rotation.eulerAngles.x > 180) {myAnim.SetFloat ("Angle", CameraControl.CameraControlTra.rotation.eulerAngles.x-360);}//瞄準角度(往上)
					if (UseGun == true){
						myAnim.SetBool ("Aim", true);BulletTime = BulletTime + Time.deltaTime;//瞄準動畫//CD時間 
						myAnim.SetLayerWeight(1,1);BulletText.text = BulletNumber.ToString() + " / 30";
						if (BulletTime >= 1f) {
							if (BulletNumber > 0){if (AxisShoot <= -1||Input.GetMouseButton (0)){//CD夠了//按R2或左鍵
									if (CameraControl.CameraControlTra.rotation.eulerAngles.x < 180) {myAnim.SetFloat ("Angle", CameraControl.CameraControlTra.transform.rotation.eulerAngles.x);}//瞄準角度(往下)
									else if (CameraControl.CameraControlTra.rotation.eulerAngles.x > 180) {myAnim.SetFloat ("Angle", CameraControl.CameraControlTra.rotation.eulerAngles.x-360);}//瞄準角度(往上)
									myAnim.Play("Shoot");PlayerSound.Fire = true;//射擊動畫 //開槍音效
									Vector3 BulletPos =BulletFather.position; Vector3 BulletRealPos = Camera.position;//子彈座標
									BulletClone = (GameObject)Instantiate (Bullet, BulletPos, CameraControl.CameraControlTra.rotation);//子彈生成
									BulletRealClone = (GameObject)Instantiate (BulletReal, BulletRealPos, CameraControl.CameraControlTra.rotation);//子彈生成
									BulletClone.AddComponent<BulletControl> ();BulletRealClone.AddComponent<BulletControl> ();//子彈程式
									BulletNumber = BulletNumber-1;BulletTime = 0;}//子彈CD
							}else if(BulletNumber == 0){BulletText.color = Color.red;}
						}	
					}else if(UseCamera == true){
						myAnim.Play("TakeCamera");BulletTime = BulletTime + Time.deltaTime;	myAnim.SetLayerWeight(1,1);
						if (CameraNumber == 1 || CameraNumber == 2) {CameraAim0.SetActive (false);CameraAim1.SetActive (true);CameraAim2.SetActive (false);CameraAim3.SetActive (false);}
						if (CameraNumber == 3 || CameraNumber == 4) {CameraAim0.SetActive (false);CameraAim1.SetActive (false);CameraAim2.SetActive (true);CameraAim3.SetActive (false);}
						if (CameraNumber == 5 || CameraNumber == 6) {CameraAim0.SetActive (false);CameraAim1.SetActive (false);CameraAim2.SetActive (false);CameraAim3.SetActive (true);}
						if (BulletTime >= 1){
							if (CameraNumber > 0){
								if (AxisShoot <= -1 || Input.GetMouseButton(0)){//按R2或左鍵
									Enemy.canMove = false; EnemyGuard.canMove = false; EnemyMelee.canMove = false; EnemyGuardMelee.canMove = false; PlayerSound.Usecamera = true;
									Flash.color = new Color(1, 1, 1, 1); CameraNumber = CameraNumber - 1; BulletTime = 0; FlashTime = 0; IsFlash = true;
								}else { Flash.color = new Color(1, 1, 1, 0); }
							}else if (CameraNumber == 0){
								CameraAim0.SetActive(true); CameraAim1.SetActive(false); CameraAim2.SetActive(false); CameraAim3.SetActive(false);
								Flash.color = new Color(1, 1, 1, 0);
							}
						}
					}else if (UseCamera == false) { BulletTime = 1f; Flash.color = new Color(1, 1, 1, 0);
					}else if(UseBowling == true){myAnim.SetBool("Bowling",true);BulletText.text = " ";
						if (AxisShoot <= -1 || Input.GetMouseButton (0)){//按R2或左鍵
							myAnim.Play("ThrowBowling");
							BulletFather.transform.GetChild(0).gameObject.AddComponent<WalkFoward>();
							OptionControl.UIOnHeadNumber = OptionControl.UIOnHeadNumber-1;UseBowling = false;
						}
					}else if(UseBowling == false){myAnim.SetBool("Bowling",false);OptionControl.UIOnHeadNumber = 0;}
				}else {myAnim.SetBool("Aim", false);}

				if(IsFlash == true){FlashTime = FlashTime + Time.deltaTime;if (FlashTime > 5) {Enemy.canMove = true;EnemyGuard.canMove = true;EnemyMelee.canMove = true;EnemyGuardMelee.canMove = true;IsFlash = false;}}
			}else if(Grounded==false){
				GetComponent<CapsuleCollider>().radius = 0.2f;myAnim.SetLayerWeight(1,0);myAnim.SetBool("Fall",true);FootStep.playermoving = false;FootStepRun.playermoving = false;
				NewMainCamera.SetActive(false);MainCamera.SetActive(true);
			}//掉落
		}else if ( CanMove == false){
			myAnim.SetFloat ("Speed", 0);myAnim.SetBool ("Run", false);
			myAnim.SetBool ("Aim", false);//開UI時 沒有動畫播放
		}else if (Dead == true) {//角色死亡
			FootStep.playermoving = false;FootStepRun.playermoving = false;enemySoundEffect.playerdie = true;PlayerSound.heartbeat = false;//各種音效
			DeadTime = DeadTime+Time.deltaTime;Speed = 0;myAnim.Play ("Dead");GameOverText.SetActive(true);//死掉動畫
			Enemy.canMove = false;EnemyMelee.canMove = false;EnemyGuard.canMove = false;EnemyGuardMelee.canMove = false;CanMove = false;//不能移動
			if(DeadTime >= 1){
				Quaternion Rot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);//倒下
				DeadMei.transform.rotation = Rot;DeadMei.transform.position = transform.position;//屍體角度座標
				AliveMei.SetActive (false);DeadMei.SetActive (true);//換成屍體物件
			}
			if (Input.GetKeyDown (KeyCode.JoystickButton2)|| Input.GetKeyDown(KeyCode.E)) {SceneManager.LoadScene (0);}//回到主畫面
		}

		if (Climb == true) {
			MainCamera.SetActive(false);NewMainCamera.SetActive(true);GetComponent<SphereCollider>().enabled = false;
			myRigidbody.constraints = RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ;
			if (UseJoystick == true) {myAnim.SetFloat("Climb",v);
				if (v > 0 ) {velocity =  new Vector3 (0, Speed, 0);FootStep.playermoving = true;} //往上
				else if (v < 0) {velocity =  new Vector3 (0, -Speed, 0);FootStep.playermoving = true;}//往下
				else if(v == 0){FootStep.playermoving = false;}
				velocity = transform.TransformDirection (velocity);transform.localPosition += velocity * Time.deltaTime;//角色往角色前方移動
			}else if (UseJoystick == false) {//用鍵盤玩
				if (Input.GetKey(KeyCode.W)) {velocity =  new Vector3 (0, Speed, 0);myAnim.SetFloat("Climb",1);} //往上
				else if (Input.GetKey(KeyCode.S)) {velocity =  new Vector3 (0, -Speed, 0);myAnim.SetFloat("Climb",-1);}//往下
				else {FootStep.playermoving = false;}
				velocity = transform.TransformDirection (velocity);transform.localPosition += velocity * Time.deltaTime;//角色往角色上方移動
			}
		}else{myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ;}}

	void OnTriggerEnter(Collider Other){if (Other.tag == "Ground")Grounded = true;}//在地上
	void OnTriggerExit(Collider Other) {if (Other.tag == "Ground")Grounded = false;//不在地上
		if (Other.tag == "EnemyFront"){Feet.SetActive (false);}//踢敵人
		if (Other.tag == "EnemyBack") {Hand.SetActive (false);}}//打敵人
	void OnTriggerStay(Collider Other) {if (Other.tag == "Ground")Grounded = true;//在地上
		if(Dead == false){
			if (Other.tag == "EnemyFront"){//踢敵人
				AttackTime = AttackTime + Time.deltaTime;
				if (AttackTime >= 1) {
					if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)) {
						Feet.SetActive (true);myAnim.Play ("Kick");CanUseMask = false;UseMask = false;PlayerSound.frontkick = true;AttackTime = 0;
					}else {Feet.SetActive (false);}
				}
			}
			if (Other.tag == "EnemyBack") {//打敵人
				AttackTime = AttackTime + Time.deltaTime;
				if (AttackTime >= 1) {
					if (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown (KeyCode.E)){
						Hand.SetActive (true);myAnim.Play ("Melee");PlayerSound.backpunch = true;AttackTime = 0;
					}else {Hand.SetActive (false);}
				}
			}
		}
	}
}