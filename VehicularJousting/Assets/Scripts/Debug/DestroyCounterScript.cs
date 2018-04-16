
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCounterScript : MonoBehaviour {

    [SerializeField] private int counter;
	
	// Update is called once per frame
	void Update () {
		if (counter <= 0){
            Destroy(gameObject);
        }
        counter--;
	}

}
