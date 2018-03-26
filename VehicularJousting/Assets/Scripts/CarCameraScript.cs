using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraScript : MonoBehaviour
{

    public Transform car;
    Rigidbody carRb;
    float distance = 7.4f;
    float height = 3f;
    float rotationDamping = 3.5f;
    float heightDamping = 2.5f;
    float zoomRatio = .5f;
    float DefaultFOV = 60f;
    private Vector3 rotationVector;

    // Use this for initialization
    void Start()
    {
        carRb = car.GetComponent<Rigidbody>();
    }

void LateUpdate()
    {
        var wantedAngle = rotationVector.y;
        var wantedHeight = car.position.y + height;
        var myAngle = transform.eulerAngles.y;
        var myHeight = transform.position.y;
        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
        myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
        var currentRotation = Quaternion.Euler(0, myAngle, 0);
        transform.position = car.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        Vector3 temp = transform.position;
        transform.position = new Vector3(temp.x, myHeight, temp.z);
        transform.LookAt(car);
    }
    void FixedUpdate()
    {
        var localVilocity = car.InverseTransformDirection(carRb.velocity);
        if (localVilocity.z < -0.5)
        {
            rotationVector.y = car.eulerAngles.y + 180;
        }
        else
        {
            rotationVector.y = car.eulerAngles.y;
        }
        var acc = carRb.velocity.magnitude;
        GetComponent<Camera>().fieldOfView = DefaultFOV + acc * zoomRatio;
    }

}