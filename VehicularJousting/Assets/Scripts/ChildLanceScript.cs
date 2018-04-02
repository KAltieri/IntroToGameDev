using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLanceScript : MonoBehaviour {

    public float speed;
    float xRot, yRot;
	bool inverseX, inverseY;

	private float rangeInDeg = 30;

	void Start()
	{
		if(!PlayerPrefs.HasKey("Inverse X"))
		{
			PlayerPrefs.SetInt ("Inverse X", 0);
			inverseX = false;
		}
		if (!PlayerPrefs.HasKey ("Inverse Y")) 
		{
			PlayerPrefs.SetInt ("Inverse Y", 0);
			inverseY = false;
		}
		if(PlayerPrefs.GetInt("InverseX") == 1)
		{
			inverseX = true;
		}
		if(PlayerPrefs.GetInt("InverseX") == 1)
		{
			inverseX = true;
		}
		else
		{
			inverseY = false;
			inverseX = false;
		}
	}


	void Update ()
    {
		//follows the mouse movement, and clamps the movement to a specified range
		if (inverseX && inverseY) 
		{
			yRot += Input.GetAxis ("Mouse X") * speed;
			xRot -= Input.GetAxis ("Mouse Y") * speed;
		}
		if (inverseX && !inverseY) 
		{
			yRot -= Input.GetAxis ("Mouse X") * speed;
			xRot -= Input.GetAxis ("Mouse Y") * speed;
		}
		if (!inverseX && inverseY) 
		{
			yRot += Input.GetAxis("Mouse X") * speed;
			xRot += Input.GetAxis("Mouse Y") * speed;
		}
		else
		{
	        yRot -= Input.GetAxis("Mouse X") * speed;
	        xRot += Input.GetAxis("Mouse Y") * speed;
		}

		yRot = Mathf.Clamp (yRot, -rangeInDeg, rangeInDeg);
		xRot = Mathf.Clamp (xRot, -rangeInDeg, rangeInDeg/6);

		transform.localRotation = Quaternion.Euler (xRot, yRot, 0);
    }
}