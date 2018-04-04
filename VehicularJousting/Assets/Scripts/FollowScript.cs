using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

    GameObject follow;
	// Use this for initialization
	void Start () 
	{
		GameObject car = GameObject.FindGameObjectWithTag ("Car");
		follow = car.transform.Find ("LancePositionTarget").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.SetPositionAndRotation(follow.transform.position, follow.transform.rotation);
	}
}
