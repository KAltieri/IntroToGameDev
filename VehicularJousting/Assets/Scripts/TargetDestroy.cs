using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroy : MonoBehaviour {

	bool destroyTarget = false;
    public bool target_hit = false;
    float timeDestroy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (destroyTarget)
        {
            timeDestroy -= 1;
            if (timeDestroy <= 0)
            {
                destroyTarget = false;
                Destroy(gameObject);
            }
            float shrink_size = (Mathf.Clamp(timeDestroy, 0, 40) / 40f) * 180;
            gameObject.transform.GetChild(0).transform.localScale = new Vector3(shrink_size, shrink_size, shrink_size);
        }

    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Car"))
        {
            if (!destroyTarget){
                explode(collision.gameObject);
            }
        }
	}

    public void explode(GameObject car){
        Instantiate(Resources.Load("pExplosion2") as GameObject, transform.position, transform.rotation);
        timeDestroy = 120;

        Vector3 car_force = car.gameObject.GetComponent<Rigidbody>().velocity;
        float add_force = 20f;
        GetComponent<Rigidbody>().AddForce(new Vector3(car_force.x * add_force, car_force.y * add_force, car_force.z * add_force));
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().mass = 5;

        Camera.main.GetComponent<CarCameraScript>().screenShake(10f);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Resources.Load("CrashSFX") as AudioClip);
        destroyTarget = true;
    }
}
