   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brakeLight : MonoBehaviour {




	public Light brakePointLight01;
	public Light brakeSpotLight02;
	public GameObject sparkle;
	public GameObject brokenLight;
	// Use this for initialization
	void Start () {
		brakePointLight01.GetComponent<Light>();
		brakeSpotLight02.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}




	void OnTriggerEnter(Collider Other){

		if (Other.tag == "Bullet") {sparkle.SetActive (true); 
			brakePointLight01.intensity = 0;
			brakeSpotLight02.intensity = 0;brokenLight.SetActive (false);}//被子彈打到


	}
}
