using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroy : MonoBehaviour {

	bool destroyTarget = false;
	float time = 0f;
	float timeDestroy = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (destroyTarget) 
		{
			if(Time.time - timeDestroy >= 5f)
			{
				Debug.Log ("yes");
				destroyTarget = false;
				Destroy(this);
			}		
		}
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		destroyTarget = true;
		timeDestroy = Time.time + 5f;
	}
}
