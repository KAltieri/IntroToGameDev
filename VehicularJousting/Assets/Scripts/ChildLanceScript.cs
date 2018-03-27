using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLanceScript : MonoBehaviour {

    public float speed;
    float xRot, yRot;

	private float rangeInDeg = 30;

	void Update ()
    {
        //xRot = transform.localRotation.eulerAngles.x;
       // yRot = transform.localRotation.eulerAngles.y;
        yRot -= Input.GetAxis("Mouse X") * speed;
        xRot += Input.GetAxis("Mouse Y") * speed;

		yRot = Mathf.Clamp (yRot, -rangeInDeg, rangeInDeg);
		xRot = Mathf.Clamp (xRot, -rangeInDeg, rangeInDeg/6);

        Quaternion moveAngle = Quaternion.Euler(xRot, yRot, 0);
		//print (xRot + " " + yRot);
		//Vector3.angle - transform.forward
		// take a point at the base
        //Vector3 pos = new Vector3(transform.position.x + transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
		//transform.SetPositionAndRotation(transform.position, moveAngle);

			Debug.Log (Vector3.Angle (transform.parent.forward, transform.forward));
			transform.localRotation = moveAngle;
    }
}