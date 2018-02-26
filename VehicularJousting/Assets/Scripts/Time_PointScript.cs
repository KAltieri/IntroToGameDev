using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Time_PointScript : MonoBehaviour {

    public Text timeText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString();

    }
}
