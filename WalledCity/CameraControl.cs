using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class CameraControl : MonoBehaviour
{
	public GameObject MainCamera, AimCamera;//兩台相機
	public GameObject AimUI,CameraUI,MinimapUI,UIOnHead,MeiHeadUI,StaminaUI,bowlingaim;//瞄準的UI
	public Transform AimPos,OriPos,Spine2,CameraPos;//座標;
	public static Transform CameraControlTra;
	public static float CameraRotateSpeed;//旋轉相機的速度
	public float damping;//相機跟隨的緩和值
	float mouseX, mouseY,time;//滑鼠水平移動值&垂直移動值

	// Use this for initialization
	void Start () {CameraRotateSpeed = 2.5f;}//初始相機璇轉速度
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Player.CanMove == true && Player.Dead == false) {//如果角色可以移動
			float AxisAim = Input.GetAxis ("AxisAim"); float AxisShoot = Input.GetAxis ("AxisShoot"); //搖桿的L2R2
			float h = Input.GetAxis ("HorizontalCam");//讀取右手搖桿水平移動值
			float v = Input.GetAxis ("VerticalCam");//讀取右手搖桿垂直移動值
			CameraControlTra = transform;//公用的相機座標
			transform.position = Vector3.Slerp(transform.position,Player.Mei.position,Time.deltaTime*damping);//座標等於角色座標
			AimPos.position =  Spine2.position;//瞄準視角座標跟腳色移動
			if(Player.Dead==false){
				if (Player.UseJoystick == true) {//用搖桿的話
				if (AxisAim <= -1) {
					if (Player.UseCamera == true) {time = time + Time.deltaTime;
						if (time > 0.5f) {
							MinimapUI.SetActive(false);UIOnHead.SetActive(false);MeiHeadUI.SetActive(false);StaminaUI.SetActive(false);CameraUI.SetActive (true);CameraRotateSpeed = 1;
							MainCamera.transform.position = Vector3.Slerp (MainCamera.transform.position, CameraPos.position, Time.deltaTime * damping);
						}//瞄準第一人稱
					}else if (Player.UseGun == true){
						MinimapUI.SetActive(false);UIOnHead.SetActive(false);MeiHeadUI.SetActive(false);StaminaUI.SetActive(false);MainCamera.GetComponent<OutlineEffect>().enabled = true;AimUI.SetActive (true);CameraUI.SetActive (false);
						CameraRotateSpeed = 1;MainCamera.transform.position = Vector3.Slerp (MainCamera.transform.position, AimCamera.transform.position, Time.deltaTime * damping);//相機移到肩上位置
					}else if (Player.UseBowling == true){
						MinimapUI.SetActive(false);UIOnHead.SetActive(false);MeiHeadUI.SetActive(false);StaminaUI.SetActive(false);AimUI.SetActive (true);CameraUI.SetActive (false);bowlingaim.SetActive (true);CameraRotateSpeed = 1;
						MainCamera.transform.position = Vector3.Slerp (MainCamera.transform.position, AimCamera.transform.position, Time.deltaTime * damping);}///相機移到肩上位置
					else {AimUI.SetActive(false);CameraUI.SetActive (false);MinimapUI.SetActive(true);UIOnHead.SetActive(true);MeiHeadUI.SetActive(true);StaminaUI.SetActive(true);bowlingaim.SetActive (false);}
				}else {
					AimUI.SetActive(false);CameraUI.SetActive (false);MinimapUI.SetActive(true);UIOnHead.SetActive(true);MeiHeadUI.SetActive(true);StaminaUI.SetActive(true);
					MainCamera.GetComponent<OutlineEffect>().enabled = false;CameraRotateSpeed = OptionControl.CameraRotateSpeedSet;Player.myAnim.SetBool("Camera",false);time = 0;
				}
				if(v!=0)transform.Rotate (-v*CameraRotateSpeed*OptionControl.Yn, 0, 0);//垂直轉
				if(h!=0)transform.Rotate (0, h*CameraRotateSpeed*OptionControl.Xn, 0);//水平角度
				if (transform.rotation.eulerAngles.x >= 0 && transform.rotation.eulerAngles.x <= 180) {//整個相機群組的角度
					if (transform.rotation.eulerAngles.x > 30 ) transform.rotation = Quaternion.Euler(30,transform.rotation.eulerAngles.y,0);//視角最高不超過30度
					else if (transform.rotation.eulerAngles.x < -30 ) transform.rotation = Quaternion.Euler(-30,transform.rotation.eulerAngles.y,0);//視角最矮不低於-30度
				}else if (transform.rotation.eulerAngles.x > 180 && transform.rotation.eulerAngles.x < 360) {//因為可能有負數
					if (transform.rotation.eulerAngles.x < 330 ) transform.rotation = Quaternion.Euler(330,transform.rotation.eulerAngles.y,0);//視角最矮不低於-30度
					if (transform.rotation.eulerAngles.x < 330 ) transform.rotation = Quaternion.Euler(330,transform.rotation.eulerAngles.y,0);//視角最矮不低於-30度
				}else if (transform.rotation.eulerAngles.x > 360)transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 360,transform.rotation.eulerAngles.y,0);//確保角度在360內
			}else if (Player.UseJoystick == false) {//用滑鼠的話
				mouseX += Input.GetAxis("Mouse X")*180*Time.deltaTime;//滑鼠水平值
				mouseY += Input.GetAxis("Mouse Y")*90*Time.deltaTime;//滑鼠垂直值
				Quaternion rot = Quaternion.Euler (-mouseY, mouseX, 0);//設定角度
				if (Input.GetMouseButton (2)) {mouseX = Player.Mei.rotation.eulerAngles.y+180;mouseY = 0;rot = Quaternion.Euler (0, Player.Mei.rotation.eulerAngles.y, 0);}//右搖桿按下去那個鍵或滑鼠中鍵//相機面向角色前方
				transform.rotation = rot;//相機旋轉
				if (mouseX > 360) {mouseX = mouseX- 360;} else if (mouseX < 0) {mouseX = mouseX + 360;}//確保角度在360內
				if (mouseY > 30) {mouseY = 30;} else if (mouseY < -30) {mouseY = -30;}//上下角度不超過30
				if (Input.GetMouseButton (1)) {
					if (Player.UseCamera == true) {time = time + Time.deltaTime;
						if (time > 0.5f) {
							MinimapUI.SetActive(false);UIOnHead.SetActive(false);MeiHeadUI.SetActive(false);StaminaUI.SetActive(false);CameraUI.SetActive (true);CameraRotateSpeed = 1;
							MainCamera.transform.position = Vector3.Slerp (MainCamera.transform.position, CameraPos.position, Time.deltaTime * damping);
						}//瞄準第一人稱
					}else if (Player.UseGun == true){
						MinimapUI.SetActive(false);UIOnHead.SetActive(false);MeiHeadUI.SetActive(false);StaminaUI.SetActive(false);MainCamera.GetComponent<OutlineEffect>().enabled = true;AimUI.SetActive (true);CameraUI.SetActive (false);
						CameraRotateSpeed = 1;MainCamera.transform.position = Vector3.Slerp (MainCamera.transform.position, AimCamera.transform.position, Time.deltaTime * damping);//相機移到肩上位置
					}else if (Player.UseBowling == true){
						MinimapUI.SetActive(false);UIOnHead.SetActive(false);MeiHeadUI.SetActive(false);StaminaUI.SetActive(false);AimUI.SetActive (true);CameraUI.SetActive (false);bowlingaim.SetActive (true); CameraRotateSpeed = 1;
						MainCamera.transform.position = Vector3.Slerp (MainCamera.transform.position, AimCamera.transform.position, Time.deltaTime * damping);}///相機移到肩上位置
				}else {
					AimUI.SetActive(false);CameraUI.SetActive (false);MinimapUI.SetActive(true);UIOnHead.SetActive(true);MeiHeadUI.SetActive(true);StaminaUI.SetActive(true);
					MainCamera.GetComponent<OutlineEffect>().enabled = false;CameraRotateSpeed = OptionControl.CameraRotateSpeedSet;Player.myAnim.SetBool("Camera",false);time = 0;
				}
			}
			if (Input.GetKey (KeyCode.JoystickButton11)) {h = 0;v = 0;transform.rotation = Quaternion.Euler (0, Player.Mei.rotation.eulerAngles.y, 0);}//右搖桿按下去那個鍵//相機面向角色前方
			else {transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);}//設定z值永遠為0
			}
		}
	}
}