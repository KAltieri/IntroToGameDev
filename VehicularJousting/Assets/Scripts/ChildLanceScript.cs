using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLanceScript : MonoBehaviour {

    public float speed;
    float xRot, yRot;
    Transform car;
    Vector3 carPos;

    // Use this for initialization
    void Start ()
    {
        carPos = GetComponentInParent<Transform>().position;
	}

	// Update is called once per frame
	void Update ()
    {
        //xRot = transform.localRotation.eulerAngles.x;
       // yRot = transform.localRotation.eulerAngles.y;
        yRot += Input.GetAxis("Mouse X") * speed;
        xRot += Input.GetAxis("Mouse Y") * speed;
        Quaternion moveAngle = Quaternion.Euler(xRot, yRot, 0);
        //Vector3 pos = new Vector3(transform.position.x + transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        transform.SetPositionAndRotation(pos, moveAngle);
    }
}