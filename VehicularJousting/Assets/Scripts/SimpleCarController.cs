using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor;
	public bool steering;
}
	
public class SimpleCarController : MonoBehaviour {
	public List<AxleInfo> axleInfos; 
	public float maxMotorTorque;
	public float maxSteeringAngle;
	public float xRotationLimit;
	public float yRotationLimit;
	public float zRotationLimit;

	// finds the corresponding visual wheel
	// correctly applies the transform
	public void ApplyLocalPositionToVisuals(WheelCollider collider)
	{
        // if no wheels return so the code is safe
		if (collider.transform.childCount == 0) {
			return;
		}

		Transform visualWheel = collider.transform.GetChild(0);

        //sets position and rotation, passes the values out as references
		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);

        //sets the wheels rotation and position equal to the rotatoin and position vars
		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;
	}

    float accelTime;
    public void FixedUpdate()
	{
        //motor is the torque times the input axis, steering is steering angle times the input axis

		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            accelTime = Time.time;

        }
        if (Input.GetKey(KeyCode.W))
        {
            if (Time.time - accelTime > 15)
            {
                //print("15");
            }
            else if (Time.time - accelTime > 10)
            {
                //print("10");
            }
            else if (Time.time - accelTime > 7)
            {
                //print("7");
            }
            else if (Time.time - accelTime > 5)
            {
                //print("5");
            }
            else if (Time.time - accelTime > 3)
            {
                //print("3");
            }
        }

        //makes it so both wheels turn and run at the same speeds
        foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
			}
			ApplyLocalPositionToVisuals(axleInfo.leftWheel);
			ApplyLocalPositionToVisuals(axleInfo.rightWheel);
		}

		if(transform.rotation.eulerAngles.x > xRotationLimit)
		{
            //transform.rotation = Quaternion.Euler(new Vector3(xRotationLimit, transform.rotation.y, transform.rotation.z));           
			transform.rotation = Quaternion.identity;
		}
		if(transform.rotation.eulerAngles.y > yRotationLimit)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, yRotationLimit, transform.rotation.z));
            transform.rotation = Quaternion.identity;
        }

        if (transform.rotation.eulerAngles.z > zRotationLimit)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, zRotationLimit));
            transform.rotation = Quaternion.identity;
        }

    }
}