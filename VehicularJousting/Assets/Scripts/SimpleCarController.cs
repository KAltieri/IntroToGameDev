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

	public void FixedUpdate()
	{
        //motor is the torque times the input axis, steering is steering angle times the input axis
		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

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
	}
}