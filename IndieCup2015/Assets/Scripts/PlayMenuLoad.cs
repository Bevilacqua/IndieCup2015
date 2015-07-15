using UnityEngine;
using System.Collections;

public class PlayMenuLoad : MonoBehaviour {
    private GameObject difficultyScreen;
    private GameObject offImage;

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().Play("MENULOAD");
        difficultyScreen = GameObject.Find("DIFFICULTYSELECT");
        difficultyScreen.SetActive(false);
        offImage = GameObject.Find("OFF");
        if(GameObject.Find("GameJoltAPI").GetComponent<AudioSource>().isPlaying)
            offImage.SetActive(false);
        else
            offImage.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void showDifficultySelect()
    {
        difficultyScreen.SetActive(true);
        difficultyScreen.GetComponent<Animator>().Play("SHOW");
    }

    public void setEasy()
    {
        EndGame.difficulty = EndGame.Difficulty.EASY;
        Game_Manager.MAX_DIFFICULTY = 1f;
        Game_Manager.MAX_HEALTH = 200;
    }

    public void setNormal()
    {
        EndGame.difficulty = EndGame.Difficulty.NORMAL;
        Game_Manager.MAX_DIFFICULTY = 1.15f;
        Game_Manager.MAX_HEALTH = 100;
    }

    public void setHard()
    {
        EndGame.difficulty = EndGame.Difficulty.HARD;
        Game_Manager.MAX_DIFFICULTY = 1.25f;
        Game_Manager.MAX_HEALTH = 100;
    }

    public void setExtreme()
    {
        EndGame.difficulty = EndGame.Difficulty.EXTREME;
        Game_Manager.MAX_DIFFICULTY = 1.5f;
        Game_Manager.MAX_HEALTH = 75;
    }

    public void goToPlayScreen()
    {
        Application.LoadLevel("default");
    }

    public void goToAboutScreen()
    {
        Application.LoadLevel("about");
    }

    public void toggleMusic()
    {
        if(GameObject.Find("GameJoltAPI").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("GameJoltAPI").GetComponent<AudioSource>().Pause();
            offImage.SetActive(true);
        }
        else
        {
            GameObject.Find("GameJoltAPI").GetComponent<AudioSource>().UnPause();
            offImage.SetActive(false);
        }
    }
}
