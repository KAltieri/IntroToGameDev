using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {

    public GameObject target;
    Vector3[] targetPosition;
    Quaternion[] targetRotation;
    float timeSpawn = 0f;

	// Use this for initialization
	void Start () {
        targetPosition = new Vector3[transform.childCount];
        targetRotation = new Quaternion[transform.childCount];
        for(int i = 0; i < targetPosition.Length; i++)
        {
            targetPosition[i] = transform.GetChild(i).position;
            targetRotation[i] = transform.GetChild(i).rotation;
        }
	}
	
	// Update is called once per frame
	void Update () {
        timeSpawn -= Time.deltaTime;
   		if(timeSpawn <= 0f)
        {
            int random = (int)Random.Range(1, 10);
            for(int i = 0; i < targetPosition.Length; i++)
            {
                if(i%random == 0)
                {
                    Instantiate(target, targetPosition[i], targetRotation[i]);
                }
            }
            timeSpawn = Time.time + 5f;
        }
	}
}
