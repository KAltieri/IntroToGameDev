using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanceScript : MonoBehaviour
{
	[Header("Win Conditions")]
    public float allotedTime = 600;
    public int targetScore = 50;
    float currentScore = 0;

	[Header("Set Objects")]
    public Text timeText;
    public Text pointText;
    public float endTime;
    public float endScore;
    public AudioSource outer, inner, perfect;

    float time, lastTime;
    bool wait = false;

    void Update()
    {
        scoreBoard();
        if (Time.time - lastTime > .05f)
        {
            wait = false;
        }
		quitGame ();
    }

	//based on where the lance hits the target
    private void OnCollisionEnter(Collision collision)
    {
		//outer - black portion of the target - low thud and +1 point
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

		//inner - white portion of the target - medium ding and +5 points
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

		//outer - red portion of the target - high pitch ding and +10 points
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

		//set up to prevent the lance from hitting multiple points on the target before it gets destroyed
        if(Time.time - lastTime > .05f)
        {
            wait = false;
        }
    }

	//deals with the ingame scoreboard, measuring the amount of points and time
    void scoreBoard()
    {
        float time = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        timeText.text = "Time Left: " + (endTime - time);
        pointText.text = "Points: " + currentScore;
        if (currentScore > endScore)
        {
			SceneManager.LoadScene ("Win", LoadSceneMode.Single);
        }
    }

	//if the time has exceeded the amount of allowed time, it loads the lose screen
    void quitGame()
    {
        if (Time.timeSinceLevelLoad > endTime)
        {
			SceneManager.LoadScene ("Lose", LoadSceneMode.Single);
        }
    }
}