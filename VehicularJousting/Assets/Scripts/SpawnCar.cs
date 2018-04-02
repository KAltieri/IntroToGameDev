using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour {

	public GameObject[] cars;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("CarColor")) 
		{
			PlayerPrefs.SetInt ("CarColor", 0);
		}
		Instantiate (cars [PlayerPrefs.GetInt ("CarColor")], transform.position, transform.rotation);
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
