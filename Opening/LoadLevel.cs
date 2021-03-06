using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour 
{
	public int SceneNumber;
	float time;
	AsyncOperation op ;

	public GameObject video;

	void Start() { StartCoroutine (Loading (SceneNumber));}

	void FixedUpdate(){time = time + Time.deltaTime;if(time>=2.5f)op.allowSceneActivation = true;}

	IEnumerator Loading(int SceneNumber){
		op = SceneManager.LoadSceneAsync (SceneNumber);
		op.allowSceneActivation = false;
		yield return op;
	}
} 