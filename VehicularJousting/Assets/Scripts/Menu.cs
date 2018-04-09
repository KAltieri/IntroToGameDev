using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	//script to control all the different menus in the game

    public void StartClick()
    {
        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
    }

	//sets another canvas to active and deactivates the original canvas
    public void TurnOnOtherCanvas(GameObject otherCanvas)
    {
		gameObject.SetActive (false);
		otherCanvas.SetActive (true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

	public void LoadMenu()
	{
		SceneManager.LoadScene ("Start", LoadSceneMode.Single);
	}

	public void SetCarColor(int color)
	{
		PlayerPrefs.SetInt ("CarColor", color);
	}

	public void SetXInverese(int setInverse)
	{
		PlayerPrefs.SetInt ("Inverse X", setInverse);
	}

	public void SetYInverse(int setInverse)
	{
		PlayerPrefs.SetInt ("Inverse Y", setInverse);
	}
}
