using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizerBar : MonoBehaviour
{

    public WheelCollider WheelL;
    public WheelCollider WheelR;
    public float AntiRoll = 5000;
    Rigidbody car;

    void Start()
    {
        car = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        WheelHit hit;
        float travelL = 1;
        float travelR = 1;
		float antiRollForce;
        bool groundedL = WheelL.GetGroundHit(out hit);
		bool groundedR = WheelR.GetGroundHit(out hit);

		//Checks to see if the wheels are on the ground, and if not, sets the value to the amount off the ground
        if (groundedL)
        {
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;
        }
        if (groundedR)
        {
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;
        }

        antiRollForce = (travelL - travelR) * AntiRoll;

		//evens out the forces so both wheels stay on the ground
        if (groundedL)
        {
            car.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position);
        }

        if (groundedR)
        {
            car.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position);

        }
    }
}