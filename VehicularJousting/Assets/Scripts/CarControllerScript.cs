using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerScript : MonoBehaviour
{

	//script that controls the cars

	[Header("Wheel Colliders")]
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

	[Header("Speed Modifiers")]
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

	[Header("Friction Modifiers")]
    public float slipSidewWayFriction = 0.05f;
    public float slipForwardFriction = .085f;

	//Gears for sound
    Rigidbody rb;
    float[] gearRatio = new float[5];

    void Start()
    {
		//sets the gear speeds for enginePitch
        for(int i = 0; i < gearRatio.Length; i++)
        {
			gearRatio[i] = (highestSpeed/10 - highestSpeed/100) * (i+1);
        }
        gearRatio[4] = highestSpeed+10;

		//sets up the rest of the vars
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.9f, .5f);

		//gets the friction of the rear wheel and sets it to values
        forwardFriction = wheelRR.forwardFriction.stiffness;
        sidewaysFriction = wheelRR.sidewaysFriction.stiffness;
    }

    void FixedUpdate()
    {
		//controls the wheel torque, therefore speed, based on the input and maxTorque
        wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
        wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");

		//declerates
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
		currentSpeed = getCarSpeed();
		float SpeedFactor = rb.velocity.magnitude / highestSpeed;
		//gets the steering angle based on Lerping the low speed angle and the high speed angle
		float currentSteeringAngle = Mathf.Lerp(lowSpeedTurnAngle, highSpeedTurnAngle, SpeedFactor);

		currentSteeringAngle *= Input.GetAxis("Horizontal");
		wheelFL.steerAngle = currentSteeringAngle;
		wheelFR.steerAngle = currentSteeringAngle;

        ReverseSlip();
    }

    public float getCarSpeed()
    {
        currentSpeed = 2 * 22 / 7 * wheelRL.radius * wheelRL.rpm * 60 / 1000;
		currentSpeed = Mathf.Round (currentSpeed);
		return currentSpeed;
    }

    void Update()
	{
//        currentSpeed = getCarSpeed();
//		float SpeedFactor = rb.velocity.magnitude / highestSpeed;
//
//		//gets the steering angle based on Lerping the low speed angle and the high speed angle
//		float currentSteeringAngle = Mathf.Lerp(lowSpeedTurnAngle, highSpeedTurnAngle, SpeedFactor);
//		currentSteeringAngle *= Input.GetAxis("Horizontal");
//		wheelFL.steerAngle = currentSteeringAngle;
//		wheelFR.steerAngle = currentSteeringAngle;
//
		HandBrake ();
		EngineSound ();
	}

	// a brake
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
			//sets the forward torque to nothing and sets the break torque to the max brake torque
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

	//deals with friction values
    void ReverseSlip()
    {
        if (currentSpeed < 0)
        {
            SetFrontSlip(slipForwardFriction, slipSidewWayFriction);
        }
        else
        {
            SetFrontSlip(forwardFriction, sidewaysFriction);
        }
    }

	//increases back wheel friction
    void SetRearSlip(float currentForwardFriction, float currentSidewayFriction)
    {
		//sets a friction curve equal to the Rear Right wheel forward Friction, then increases the stiffness on it
		//Lastly sets the RR wheel forward friction equal to the curve
        WheelFrictionCurve wFRR = wheelRR.forwardFriction;
        wFRR.stiffness = currentForwardFriction;
        wheelRR.forwardFriction = wFRR;

		//same as above except with the sidways friction
        WheelFrictionCurve wSRR = wheelRR.sidewaysFriction;
        wSRR.stiffness = currentSidewayFriction;
        wheelRR.forwardFriction = wSRR;

		//same as the first
        WheelFrictionCurve wFRL = wheelRL.forwardFriction;
        wFRL.stiffness = currentForwardFriction;
        wheelRL.forwardFriction = wFRL;

		//same as above except with the sidways friction
        WheelFrictionCurve wSRL = wheelRL.forwardFriction;
        wSRL.stiffness = currentForwardFriction;
        wheelRL.forwardFriction = wSRL;
    }

	//increases front wheel friction
    void SetFrontSlip(float currentForwardFriction, float currentSidewayFriction)
    {
		//sets a friction curve equal to the Front Right wheel forward Friction, then increases the stiffness on it
		//Lastly sets the FR wheel forward friction equal to the curve
        WheelFrictionCurve wFFR = wheelFR.forwardFriction;
        wFFR.stiffness = currentForwardFriction;
        wheelFR.forwardFriction = wFFR;

        WheelFrictionCurve wSFR = wheelFR.sidewaysFriction;
        wSFR.stiffness = currentSidewayFriction;
        wheelRR.forwardFriction = wSFR;

		//Front Left
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
            if (gearRatio[i] > currentSpeed)
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
            minGearValue = gearRatio[gear - 1];
        }
        maxGearValue = gearRatio[gear];
		float modCurrentSpeed = currentSpeed / 10;
		//sets the pitch based on gear and speed
        float enginePitch = ((modCurrentSpeed - maxGearValue) / (maxGearValue - minGearValue)) + 1;
		if (enginePitch <= .5f) 
		{
			enginePitch = .5f;
		}
        GetComponent<AudioSource>().pitch = enginePitch;
    }
}
