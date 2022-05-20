using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

	float BulletSpeed = 0.5f;//子彈速度
	float BulletTime;//子彈存在的時間

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {
		BulletTime = BulletTime + Time.deltaTime;//子彈存在的時間
		//transform.Translate(transform.right*-1*BulletSpeed);
		transform.Translate (0, 0, BulletSpeed);//子彈往前
		if (this.tag == "BulletFake") {if(BulletTime >= 0.3f)Destroy (this.gameObject);}//刪掉子彈
		if(BulletTime >= 5)Destroy (this.gameObject);//刪掉子彈
	}

	void OnTriggerEnter(Collider Other)
	{
		Destroy (this.gameObject);
		if (Other.tag == "Ground") {Destroy (this.gameObject);}//刪掉子彈
		if (Other.tag == "Build") {Destroy (this.gameObject);}//刪掉子彈
		if (Other.tag == "Enemy") {Destroy (this.gameObject);}//刪掉子彈
		if (Other.tag == "RedLight") {Destroy (this.gameObject);}//刪掉子彈
		if(Other.tag == "buildDoor"){Destroy(this.gameObject);}

	}
}