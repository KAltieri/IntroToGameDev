using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanceScript : MonoBehaviour
{
    public float allotedTime = 600;
    public int targetScore = 50;
    float currentScore = 0;

    public Text timeText;
    public Text pointText;
    public float endTime;
    public float endScore;
    public AudioSource outer, inner, perfect;
    float time, lastTime;
    bool wait = false;

    void Start()
    {
    }

    void Update()
    {
        scoreBoard();
        if (Time.time - lastTime > .05f)
        {
            wait = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.tag == "Outer")
        {
            Destroy(collision.gameObject);
            if (!wait)
            {
                currentScore++;
                outer.Play();
                lastTime = Time.time;
                wait = true;
            }
        }
        else if (collision.collider.tag == "Inner")
        {
            Destroy(collision.gameObject);
            if (!wait)
            {
                currentScore += 5;
                inner.Play();
                lastTime = Time.time;
                wait = true;
            }
        }
		else if (collision.collider.tag == "Perfect")
        {
            Destroy(collision.gameObject);
            if (!wait)
            {
                currentScore += 10;
                perfect.Play();
                lastTime = Time.time;
                wait = true;
            }
        }
        if(Time.time - lastTime > .05f)
        {
            wait = false;
        }
    }

    void scoreBoard()
    {
        float time = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        timeText.text = "Time Left: " + (endTime - time);
        pointText.text = "Points: " + currentScore;
        if (currentScore > endScore)
        {
            pointText.text = "YOU WIN!!!";
        }
    }

    void quitGame()
    {
        if (Time.timeSinceLevelLoad > endTime)
        {
            Application.Quit();
        }
    }
}