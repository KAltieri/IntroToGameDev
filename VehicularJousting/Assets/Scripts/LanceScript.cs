using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceScript : MonoBehaviour
{
    public float speed;
    Vector3 mousePos;
	public GameObject carObj;

    void Start()
    {
    }

    void Update()
    {
		Vector3 car = carObj.transform.position;
		Vector3 pos = new Vector3 (car.x - 1f, car.y + 1f, car.z + 1f);
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Quaternion temp = Quaternion.Euler((mousePos.y) * speed, (-1*mousePos.x) * speed, mousePos.z * speed);
        //Quaternion temp = Quaternion.Euler(new Vector3(transform.rotation.x + (mousePos.y*speed), transform.position.y + (mousePos.x*speed), transform.position.y + (mousePos.z * speed)));
        transform.SetPositionAndRotation(pos, temp);
    }

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag.Equals ("Outer")) 
		{
			Debug.Log ("1 point");
			Destroy (collision.transform.parent.gameObject);
		} 
		else if (collision.gameObject.tag.Equals ("Inner")) 
		{
			Debug.Log ("5 point");
			Destroy (collision.transform.parent.gameObject);
		}
		else if (collision.gameObject.tag.Equals ("Perfect")) 
		{
			Debug.Log ("10 point");
			Destroy (collision.transform.parent.gameObject);
		}
    }
}