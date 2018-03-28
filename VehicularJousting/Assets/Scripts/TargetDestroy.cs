using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroy : MonoBehaviour {

	bool destroyTarget = false;
	float timeToDestroy = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (destroyTarget) 
		{
			timeToDestroy = Time.deltaTime;
			if(timeToDestroy >= 5f)
			{
				destroyTarget = false;
				Destroy(this);
			}		
		}
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		destroyTarget = true;
	}
}
