using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour 
{
	public Texture[] textures;
	public int CurrentTexture;
	public float changeInterval = 0.33F;
	public Renderer rend;

	void Start () 
	{
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Keypad0)) 
		{
			if (textures.Length == 0)
			return;

			int index = Mathf.FloorToInt(Time.time / changeInterval);
			index = index % textures.Length;
			rend.material.mainTexture = textures[CurrentTexture];

			CurrentTexture++;
			CurrentTexture %= textures.Length;

		}	
	}
}
