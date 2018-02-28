using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenesController : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if (GameObject.FindGameObjectsWithTag ("GameController").Length > 1) 
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
