using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanceScript : MonoBehaviour
{
    public float speed;
    public GameObject carObj;
    public Camera cam;
    public float allotedTime = 600;
    public int targetScore = 50;
    float currentScore = 0;

    public Text timeText;
    public Text pointText;
    public float endTime;
    public float endScore;

    void Start()
    {
    }

    void Update()
    {
        Vector3 car = carObj.transform.position;
        Quaternion carQuat = carObj.transform.rotation;
        Vector3 pos = new Vector3(car.x - 1, car.y - .35f, car.z + 3.85f);
        Vector3 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 carAngle = carQuat.eulerAngles;

        Quaternion temp = Quaternion.Euler(((mousePos.y * speed) + carAngle.x), (((-1 * mousePos.x) * speed) + carAngle.y), ((mousePos.z * speed) + carAngle.z));
        //Quaternion temp = Quaternion.Euler(new Vector3(transform.rotation.x + (mousePos.y*speed), transform.position.y + (mousePos.x*speed), transform.position.y + (mousePos.z * speed)));
        transform.SetPositionAndRotation(pos, temp);
        Debug.Log(currentScore);
        scoreBoard();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Outer"))
        {
            Destroy(collision.transform.parent.gameObject);
            currentScore++;
            return;
        }
        else if (collision.gameObject.tag.Equals("Inner"))
        {
            Destroy(collision.transform.parent.gameObject);
            currentScore += 5;
            return;
        }
        else if (collision.gameObject.tag.Equals("Perfect"))
        {
            Destroy(collision.transform.parent.gameObject);
            currentScore += 10;
            return;
        }
    }

    void scoreBoard()
    {
        float time = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        timeText.text = "Time Left: " + (endTime - time);
        pointText.text = "Points: " + currentScore;
        if(currentScore > endScore)
        {
            pointText.text = "YOU WIN!!!";
        }
    }

    void quitGame()
    {
        if(Time.timeSinceLevelLoad > endTime)
        {
            Application.Quit();
        }
    }
}