using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartClick()
    {
        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
    }

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
