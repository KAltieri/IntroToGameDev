using System.Collections;
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
    public float highestSpeed = 50;
    public float lowSpeedTurnAngle = 1;
    public float highSpeedTurnAngle = 10;
    public float decellerationSpeed = 50;
    public float maxBrakeTorque = 100;
	float currentSpeed;
	bool brake = false;


    //friction
    float sidewaysFriction;
    float forwardFriction;
    public float slipSidewWayFriction = 0.05f;
    public float slipForwardFriction = .085f;

	//Gears for sound
    Rigidbody rb;
    float[] gearRatio = new float[5];

    // Use this for initialization
    void Start()
    {
		//sets the gear speeds for enginePitche
        for(int i = 0; i < gearRatio.Length; i++)
        {
			gearRatio[i] = (highestSpeed/10 - highestSpeed/100) * (i+1);
        }
        gearRatio[4] = highestSpeed+10;
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
        float SpeedFactor = rb.velocity.magnitude / highestSpeed;
        float currentSteeringAngle = Mathf.Lerp(lowSpeedTurnAngle, highSpeedTurnAngle, SpeedFactor);
        currentSteeringAngle *= Input.GetAxis("Horizontal");
        wheelFL.steerAngle = currentSteeringAngle;
        wheelFR.steerAngle = currentSteeringAngle;
        HandBrake();
        EngineSound();
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
				SetRearSlip(slipForwardFriction, slipSidewWayFriction);
            }
            else if (currentSpeed < 0)
            {
                wheelRR.brakeTorque = maxBrakeTorque;
                wheelRL.brakeTorque = maxBrakeTorque;
                wheelRR.motorTorque = 0;
                wheelRL.motorTorque = 0;
                SetRearSlip(1, 1);
            }
            else
            {
                SetRearSlip(1, 1);
            }
        }
        else
        {
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
			SetRearSlip(forwardFriction, sidewaysFriction);
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

    void EngineSound()
    {
        int gear = 0;
        for (int i = 0; i < gearRatio.Length; i++)
        {
            if (gearRatio[i] > currentSpeed-10f)
            {
                gear = i;
                break;
            }
        }
        float minGearValue = 0f;
        float maxGearValue = 0f;
        if (gear == 0)
        {
            minGearValue = 0;
        }
        else
        {
            maxGearValue = gearRatio[gear - 1];
        }
        maxGearValue = gearRatio[gear];
        float enginePitch = ((currentSpeed - minGearValue) / (maxGearValue - minGearValue)) + 1;
        GetComponent<AudioSource>().pitch = enginePitch;
    }
}
