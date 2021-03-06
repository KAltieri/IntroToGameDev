﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraScript : MonoBehaviour
{
	GameObject carObj;
    Transform car;
    Rigidbody carRb;
    float distance = 7.4f;
    float height = 3f;
    float rotationDamping = 3.5f;
    float heightDamping = 2.5f;
    float zoomRatio = .5f;
    float DefaultFOV = 60f;
    private Vector3 rotationVector;

    private float shake_timer;
    private float shake_inten;

    // Use this for initialization
    void Start()
    {
		carObj = GameObject.FindGameObjectWithTag ("Car");
		car = carObj.GetComponent<Transform> ();
        carRb = car.GetComponent<Rigidbody>();
    }

	void Update()
	{
		//rotates the camera if the car is moving backwards or forwards
        if(Input.GetKey(KeyCode.W))
        {
            rotationVector.y = car.eulerAngles.y;
        }
        if(Input.GetKey(KeyCode.S))
        {
            rotationVector.y = car.eulerAngles.y + 180;
		}
		else
		{
			if(carObj.GetComponent<CarControllerScript>().getCarSpeed() < 0)
            {
                rotationVector.y = car.eulerAngles.y + 180;
            }
            else
            {
                rotationVector.y = car.eulerAngles.y;
            }
        }

		//zooms out as the car goes faster
		if(Input.GetKey(KeyCode.S))
		{
			GetComponent<Camera>().fieldOfView = DefaultFOV + (zoomRatio * -1);
		}
		else
		{
			var acc = carRb.velocity.magnitude;
			GetComponent<Camera>().fieldOfView = DefaultFOV + (acc * zoomRatio);
		}
	}

	void LateUpdate()
    {
        float wantedAngle = rotationVector.y;
        float wantedHeight = car.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

		//moves the camera as the car moves
        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
        myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
        transform.position = car.position;
		transform.position -= currentRotation * (Vector3.forward * distance);
        Vector3 temp = transform.position;
        transform.position = new Vector3(temp.x, myHeight, temp.z);
        transform.LookAt(car);

        if (shake_timer > 0){
            shake_inten *= 0.85f;
            transform.position = new Vector3(transform.position.x + Random.Range(-shake_inten, shake_inten), transform.position.y + Random.Range(-shake_inten, shake_inten), transform.position.z);
        }
        shake_timer--;
    }

    public void screenShake(float time){
        shake_timer = time;
        shake_inten = 0.65f;
    }
}