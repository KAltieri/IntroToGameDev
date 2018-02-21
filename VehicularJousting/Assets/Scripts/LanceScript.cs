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

    float timePast;
    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag.Equals ("Outer")) 
		{
            Destroy(collision.transform.parent.gameObject);
            Debug.Log ("1 point");
            return;
		} 
		else if (collision.gameObject.tag.Equals ("Inner")) 
		{
            Destroy(collision.transform.parent.gameObject);
            Debug.Log ("5 point");
            return;
		}
		else if (collision.gameObject.tag.Equals ("Perfect")) 
		{
            Destroy(collision.transform.parent.gameObject);
            Debug.Log ("10 point");
            return;
		}
    }
}