using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLanceScript : MonoBehaviour {

    public float speed;
    float xRot, yRot;

	private float rangeInDeg = 30;

	void Update ()
    {
		//follows the mouse movement, and clamps the movement to a specified range
        yRot -= Input.GetAxis("Mouse X") * speed;
        xRot += Input.GetAxis("Mouse Y") * speed;

		yRot = Mathf.Clamp (yRot, -rangeInDeg, rangeInDeg);
		xRot = Mathf.Clamp (xRot, -rangeInDeg, rangeInDeg/6);

		transform.localRotation = Quaternion.Euler (xRot, yRot, 0);
    }
}