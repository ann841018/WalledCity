using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class CameraControlInside : MonoBehaviour
{
	public GameObject MainCamera, AimCamera;//兩台相機
	public GameObject AimUI;//瞄準的UI
	public Transform AimPos,OriPos,Spine2;//座標

	public static GameObject OutlineCamera;
	public static Transform CameraControlTra;
	public static float CameraRotateSpeed;//旋轉相機的速度
	public float damping;//相機跟隨的緩和值
	float mouseX, mouseY;//滑鼠水平移動值&垂直移動值

	// Use this for initialization
	void Start () {CameraRotateSpeed = 2.5f;}

	// Update is called once per frame
	void FixedUpdate () 
	{
		float AxisAim = Input.GetAxis ("AxisAim"); float AxisShoot = Input.GetAxis ("AxisShoot"); //搖桿的L2R2
		float h = Input.GetAxis ("HorizontalCam");//讀取右手搖桿水平移動值
		float v = Input.GetAxis ("VerticalCam");//讀取右手搖桿垂直移動值
		CameraControlTra = transform;OutlineCamera = MainCamera;
		if(Player.CanMove == true){
		transform.position = Vector3.Slerp(transform.position,PlayerInside.Mei.position,Time.deltaTime*damping);//座標等於角色座標
		AimPos.position =  Spine2.position;//瞄準視角座標跟腳色移動

		if (Player.UseJoystick == true) {//用搖桿的話
			if (AxisAim <= -1) {
				AimUI.SetActive (true);CameraRotateSpeed = 1;
				MainCamera.transform.position = Vector3.Slerp (MainCamera.transform.position, AimCamera.transform.position, Time.deltaTime * damping);
			}else {AimUI.SetActive(false);CameraRotateSpeed = OptionControl.CameraRotateSpeedSet;}//瞄準第二人稱
			if(v!=0)transform.Rotate (-v*CameraRotateSpeed, 0, 0);//垂直轉
			if(h!=0)transform.Rotate (0, h*CameraRotateSpeed, 0);//水平角度
			if (transform.rotation.eulerAngles.x >= 0 && transform.rotation.eulerAngles.x <= 180) {//整個相機群組的角度
				if (transform.rotation.eulerAngles.x > 30 ) transform.rotation = Quaternion.Euler(30,transform.rotation.eulerAngles.y,0);//視角最高不超過30度
				else if (transform.rotation.eulerAngles.x < -30 ) transform.rotation = Quaternion.Euler(-30,transform.rotation.eulerAngles.y,0);//視角最矮不低於-30度
			}else if (transform.rotation.eulerAngles.x > 180 && transform.rotation.eulerAngles.x < 360) {//因為可能有負數
				if (transform.rotation.eulerAngles.x < 330 ) transform.rotation = Quaternion.Euler(330,transform.rotation.eulerAngles.y,0);//視角最矮不低於-30度
			}else if (transform.rotation.eulerAngles.x > 360)transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 360,transform.rotation.eulerAngles.y,0);//確保角度在360內
		}else if (Player.UseJoystick == false) {//用滑鼠的話
			mouseX += Input.GetAxis("Mouse X")*180*Time.deltaTime;//滑鼠水平值
			mouseY += Input.GetAxis("Mouse Y")*90*Time.deltaTime;//滑鼠垂直值
			Quaternion rot = Quaternion.Euler (-mouseY, mouseX-90, 0);//設定角度
			transform.rotation = rot;
			if (mouseX > 360) {mouseX = mouseX- 360;} else if (mouseX < 0) {mouseX = mouseX + 360;}//確保角度在360內
			if (mouseY > 30) {mouseY = 30;} else if (mouseY < -30) {mouseY = -30;}//上下角度不超過30
			if (Input.GetMouseButton (1)) {MainCamera.transform.position =Vector3.Slerp(MainCamera.transform.position,AimCamera.transform.position,Time.deltaTime*damping);AimUI.SetActive(true);}else {AimUI.SetActive(false);}//瞄準第二人稱
		}

		if (Input.GetKey (KeyCode.JoystickButton11)||Input.GetMouseButton (2)) {//右搖桿按下去那個鍵或滑鼠中鍵
			h = 0;v = 0;mouseX = 0;mouseY = 0;transform.rotation = Quaternion.Euler (0, PlayerInside.Mei.rotation.eulerAngles.y, 0);}//相機面向角色前方
		else {transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);}//設定z值永遠為0
		}
	}
}