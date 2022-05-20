using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

	public Text text;
	float letterPause = 0.1f;
	string sentence = "L o a d i n g ...";

	// Use this for initialization
	void Start () {StartCoroutine (TypeText(sentence));}

	// Update is called once per frame
	void Update () {}

	IEnumerator TypeText(string str)
	{
		foreach (var word in str) 
		{
			text.text += word;
			yield return new WaitForSeconds (letterPause);
		}
	}
}