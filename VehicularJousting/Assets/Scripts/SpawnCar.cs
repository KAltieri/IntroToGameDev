using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour {


	// Use this for initialization
	void Start () 
	{
		if (!PlayerPrefs.HasKey ("CarColor")) 
		{
			PlayerPrefs.SetInt ("CarColor", 0);
		}
		switch (PlayerPrefs.GetInt("CarColor"))
		{
		default: 
			Instantiate (Resources.Load ("Blue_Car_Model"), transform.position, transform.rotation);
			break;
		case 0:
			Instantiate (Resources.Load ("Blue_Car_Model"), transform.position, transform.rotation);
			break;
		case 1:
			Instantiate (Resources.Load ("Red_Car_Model"), transform.position, transform.rotation);
			break;
		case 2:
			Instantiate (Resources.Load ("Green_Car_Model"), transform.position, transform.rotation);
			break;
		}
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
