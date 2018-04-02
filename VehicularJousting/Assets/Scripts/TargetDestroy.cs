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
            timeDestroy -= Time.deltaTime;
            if (timeDestroy <= 0)
            {
                destroyTarget = false;
                Destroy(gameObject);
            }
        }

    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Car"))
        {
            destroyTarget = true;
            timeDestroy = Time.time;
        }
	}
}
