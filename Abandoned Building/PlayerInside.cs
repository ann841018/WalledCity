using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInside : MonoBehaviour 
{
	public GameObject Bullet,BulletReal;//被複製的子彈物件
	public Transform Camera,BulletFather;//瞄準那台相機的位置 //子彈生成位置
	public static Transform Mei;

	float BulletTime;//子彈CD
	float SetSpeed = 3f,Speed;//角色移動速度

	GameObject BulletClone,BulletRealClone;//複製出來的子彈物件
	Rigidbody myRigidbody;//角色鋼體
	Animator myAnim;//角色動畫

	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody> ();myAnim = GetComponent<Animator> ();//設定角色鋼體及動畫
		Player.FromSceneNumber = 2;
		//Player.UseJoystick = true;
		Player.CanMove = true;
		Speed = SetSpeed;//設定速度
	}

	// Update is called once per frame
	void FixedUpdate () {Mei = transform;
		if(Player.UseJoystick==true) Cursor.visible = false;//隱藏滑鼠
		if (Player.CanMove == true) {//如果角色可以移動
			float h = Input.GetAxis ("Horizontal");float v = Input.GetAxis ("Vertical");//左搖桿
			float AxisAim = Input.GetAxis ("AxisAim"); float AxisShoot = Input.GetAxis ("AxisShoot");//L2R2
			Vector3 velocity = new Vector3 (0, 0, 0);//角色移動
			if (Player.UseJoystick == true) {//用搖桿玩
				if (v > 0) {transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y + h * 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);FootStep.playermoving = true;myAnim.SetLayerWeight(1,1);} //往前
				else if (v == 0) {//左右
					if (h < 0) {transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y - 90, 0);velocity =  new Vector3 (0, 0, Speed);FootStep.playermoving = true;}//往左
					if (h > 0) {transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y + 90, 0);velocity =  new Vector3 (0, 0, Speed);FootStep.playermoving = true;}//往右
					if (h != 0) myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1); if (h == 0) {myAnim.SetFloat ("Speed", 0);myAnim.SetLayerWeight(1,0);FootStep.playermoving = false;FootStepRun.playermoving = false;}} //角色動畫//腳步聲
				else if (v < 0) {transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y + 180 - h * 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);FootStep.playermoving = true;}//往後
				velocity = transform.TransformDirection (velocity);transform.localPosition += velocity * Time.deltaTime;//角色往角色前方移動
			}else if (Player.UseJoystick == false) {//用鍵盤玩
				if (Input.GetKey(KeyCode.W)) {transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y + h * 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);} //往前
				else if (Input.GetKey(KeyCode.A)) {transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y - 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);}//往左
				else if (Input.GetKey(KeyCode.D)) {transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y + 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);}//往右
				else if (Input.GetKey(KeyCode.S)) {transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y + 180 - h * 90, 0);velocity =  new Vector3 (0, 0, Speed);myAnim.SetFloat ("Speed", 1);myAnim.SetLayerWeight(1,1);}//往後
				else {myAnim.SetFloat ("Speed", 0);myAnim.SetLayerWeight(1,0);} 
				velocity = transform.TransformDirection (velocity);transform.localPosition += velocity * Time.deltaTime;//角色往角色前方移動
			}
			if (AxisAim <= -1||Input.GetMouseButton (1)) {//按L2或右鍵
				myAnim.SetBool ("Aim", true);//瞄準
				Speed = SetSpeed - 0.5f;//速度放慢
				BulletTime = BulletTime + Time.deltaTime;//CD時間
				transform.rotation = Quaternion.Euler (0, CameraControlInside.CameraControlTra.rotation.eulerAngles.y, 0);//角色角度等於相機角度
				if (CameraControlInside.CameraControlTra.rotation.eulerAngles.x < 180) {myAnim.SetFloat ("Angle", CameraControlInside.CameraControlTra.rotation.eulerAngles.x);}//瞄準角度(往下)
				else if (CameraControlInside.CameraControlTra.rotation.eulerAngles.x > 180) {myAnim.SetFloat ("Angle", CameraControlInside.CameraControlTra.rotation.eulerAngles.x-360);}//瞄準角度(往上)
				if (BulletTime >= 1f) {
					if (AxisShoot <= -1||Input.GetMouseButton (0)){//CD夠了//按R2或左鍵
						myAnim.Play("Shoot");PlayerSound.Fire = true;//射擊動畫 //開槍音效
						Vector3 BulletPos =BulletFather.position; Vector3 BulletRealPos = Camera.position;//子彈座標
						BulletClone = (GameObject)Instantiate (Bullet, BulletPos, CameraControlInside.CameraControlTra.rotation);//子彈生成
						BulletRealClone = (GameObject)Instantiate (BulletReal, BulletRealPos, CameraControlInside.CameraControlTra.rotation);//子彈生成
						BulletClone.AddComponent<BulletControl> ();BulletRealClone.AddComponent<BulletControl> ();//子彈程式
						BulletTime = 0;}//子彈CD
				}
			}else {myAnim.SetBool ("Aim", false);myAnim.SetBool ("Shoot", false);}//瞄準動畫
		}else if(Player.CanMove==false)	{myAnim.SetFloat ("Speed", 0);myAnim.SetBool ("Aim", false);}//開UI時 沒有動畫播放
	}
}