using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressBottonToStart : MonoBehaviour {

	public GameObject Fungus;
	public Text text;
	float alpha = 1;
	float miner = 0.01f;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		alpha = alpha - miner;
		if (alpha <= 0)	miner = -0.01f;
		if (alpha >= 1)	miner = 0.01f;
		text.color = new Color (1, 1, 1, alpha);

		if (Input.anyKey) {
			Fungus.SetActive (true);
			Destroy (this.gameObject);
		}
	}
}
