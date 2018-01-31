using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour {

	public float speedMultipler;
	float currentSpeed;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizInput = Input.GetAxis ("Horizontal") * speedMultipler;
		float vertInput = Input.GetAxis ("Vertical") * speedMultipler;
		rb.AddForce (horizInput,0, vertInput, ForceMode.Impulse);
	}
}
