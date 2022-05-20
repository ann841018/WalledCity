using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetOut : MonoBehaviour 
{
	public GameObject movie;
	public GameObject Loading;
	public GameObject LoadLevelObject;
	public GameObject LoadingImage;

	public MusicPlayerInside musicinside;

	// Use this for initialization
	void Start () {
		musicinside.BGMFStop ();
		//movie.SetActive (true);
		Loading.SetActive(true);
		LoadLevelObject.SetActive (true);
		LoadingImage.SetActive(true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {}
}
