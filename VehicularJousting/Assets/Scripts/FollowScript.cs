using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

    public GameObject follow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.SetPositionAndRotation(follow.GetComponent<Transform>().position, follow.GetComponent<Transform>().rotation);
	}
}
