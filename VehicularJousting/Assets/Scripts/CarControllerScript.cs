﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerScript : MonoBehaviour
{

    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    //speed vars
    public float maxTorque = 50;
    float currentSpeed;
    public float highestSpeed = 50;
    public float lowSpeedTurnAngle = 1;
    public float highSpeedTurnAngle = 10;
    public float decellerationSpeed = 50;
    bool brake = false;
    public float maxBrakeTorque = 100;

    //friction
    float sidewaysFriction;
    float forwardFriction;
    public float slipSidewWayFriction = 0.05f;
    public float slipForwardFriction = .085f;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.9f, .5f);
        forwardFriction = wheelRR.forwardFriction.stiffness;
        sidewaysFriction = wheelRR.sidewaysFriction.stiffness;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentSpeed = 2 * 22 / 7 * wheelRL.radius * wheelRL.rpm * 60 / 1000;
        wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
        wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");
        //wheelFL.motorTorque = maxTorque * Input.GetAxis("Horizontal");
        //wheelFL.motorTorque = maxTorque * Input.GetAxis("Horizontal");

        if (!Input.GetButton("Vertical"))
        {
            wheelRR.brakeTorque = decellerationSpeed;
            wheelRL.brakeTorque = decellerationSpeed;
        }
        else
        {
            wheelRR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
        }
        var SpeedFactor = rb.velocity.magnitude / highestSpeed;
        var currentSteeringAngle = Mathf.Lerp(lowSpeedTurnAngle, highSpeedTurnAngle, SpeedFactor);
        currentSteeringAngle *= Input.GetAxis("Horizontal");
        wheelFL.steerAngle = currentSteeringAngle;
        wheelFR.steerAngle = currentSteeringAngle;
        WheelPositioning();
        HandBrake();
    }

    void WheelPositioning()
    {
        RaycastHit hit;
        Vector3 wheelPos;
        //FL
        if (Physics.Raycast(wheelFL.transform.position, -wheelFL.transform.up, out hit, wheelFL.radius + wheelFL.suspensionDistance))
        {
            wheelPos = hit.point + wheelFL.transform.up * wheelFL.radius;
        }
        else
        {
            wheelPos = wheelFL.transform.position - wheelFL.transform.up * wheelFL.suspensionDistance;
        }
        //wheelFLTrans.position = wheelPos;

        //FR
        if (Physics.Raycast(wheelFR.transform.position, -wheelFR.transform.up, out hit, wheelFR.radius + wheelFR.suspensionDistance))
        {
            wheelPos = hit.point + wheelFR.transform.up * wheelFR.radius;
        }
        else
        {
            wheelPos = wheelFR.transform.position - wheelFR.transform.up * wheelFR.suspensionDistance;
        }
        //wheelFRTrans.position = wheelPos;

        //RL
        if (Physics.Raycast(wheelRL.transform.position, -wheelRL.transform.up, out hit, wheelRL.radius + wheelRL.suspensionDistance))
        {
            wheelPos = hit.point + wheelRL.transform.up * wheelRL.radius;
        }
        else
        {
            wheelPos = wheelRL.transform.position - wheelRL.transform.up * wheelRL.suspensionDistance;
        }
        //wheelRLTrans.position = wheelPos;

        //RR
        if (Physics.Raycast(wheelRR.transform.position, -wheelRR.transform.up, out hit, wheelRR.radius + wheelRR.suspensionDistance))
        {
            wheelPos = hit.point + wheelRR.transform.up * wheelRR.radius;
        }
        else
        {
            wheelPos = wheelRR.transform.position - wheelRR.transform.up * wheelRR.suspensionDistance;
        }
        //wheelRRTrans.position = wheelPos;
    }

    void HandBrake()
    {
        if (Input.GetButton("Jump"))
        {
            brake = true;
        }
        else
        {
            brake = false;
        }
        if (brake)
        {
            if (currentSpeed > 1)
            {
                wheelFR.brakeTorque = maxBrakeTorque;
                wheelFL.brakeTorque = maxBrakeTorque;
                wheelRR.motorTorque = 0;
                wheelRL.motorTorque = 0;
                //SetRearSlip(slipForwardFriction, slipSidewayFriction);
            }
            else if (currentSpeed < 0)
            {
                wheelRR.brakeTorque = maxBrakeTorque;
                wheelRL.brakeTorque = maxBrakeTorque;
                wheelRR.motorTorque = 0;
                wheelRL.motorTorque = 0;
                //SetRearSlip(1, 1);
            }
            else
            {
                //SetRearSlip(1, 1);
            }
        }
        else
        {
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            //SetRearSlip(myForwardFriction, mySidewayFriction);
        }
    }

    void ReverseSlip()
    {
        if (currentSpeed < 0)
        {
            SetFrontSlip(slipForwardFriction, slipSidewWayFriction);
        }
        else
        {
            SetFrontSlip(slipForwardFriction, slipSidewWayFriction);
        }
    }

    void SetRearSlip(float currentForwardFriction, float currentSidewayFriction)
    {
        WheelFrictionCurve wFRR = wheelRR.forwardFriction;
        wFRR.stiffness = currentForwardFriction;
        wheelRR.forwardFriction = wFRR;
        WheelFrictionCurve wSRR = wheelRR.sidewaysFriction;
        wSRR.stiffness = currentSidewayFriction;
        wheelRR.forwardFriction = wSRR;


        WheelFrictionCurve wFRL = wheelRL.forwardFriction;
        wFRL.stiffness = currentForwardFriction;
        wheelRL.forwardFriction = wFRL;
        WheelFrictionCurve wSRL = wheelRL.forwardFriction;
        wSRL.stiffness = currentForwardFriction;
        wheelRL.forwardFriction = wSRL;
    }
    void SetFrontSlip(float currentForwardFriction, float currentSidewayFriction)
    {
        WheelFrictionCurve wFFR = wheelFR.forwardFriction;
        wFFR.stiffness = currentForwardFriction;
        wheelFR.forwardFriction = wFFR;
        WheelFrictionCurve wSFR = wheelFR.sidewaysFriction;
        wSFR.stiffness = currentSidewayFriction;
        wheelRR.forwardFriction = wSFR;


        WheelFrictionCurve wFFL = wheelFL.forwardFriction;
        wFFL.stiffness = currentForwardFriction;
        wheelRL.forwardFriction = wFFL;
        WheelFrictionCurve wSFL = wheelFL.forwardFriction;
        wSFL.stiffness = currentForwardFriction;
        wheelRL.forwardFriction = wSFL;
    }
}
