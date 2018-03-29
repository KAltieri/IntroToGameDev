using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroy : MonoBehaviour {

	bool destroyTarget = false;
    float timeDestroy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (destroyTarget)
        {
            timeDestroy = Time.deltaTime;
            if (timeDestroy >= 5f)
            {
                destroyTarget = false;
                Destroy(this);
            }
        }

    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Car")
        {
            destroyTarget = true;
        }
	}
}
