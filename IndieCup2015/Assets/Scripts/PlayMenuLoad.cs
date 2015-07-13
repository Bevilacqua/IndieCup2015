using UnityEngine;
using System.Collections;

public class PlayMenuLoad : MonoBehaviour {
    private GameObject difficultyScreen;

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().Play("MENULOAD");
        difficultyScreen = GameObject.Find("DIFFICULTYSELECT");
        difficultyScreen.SetActive(false);
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
        Game_Manager.MAX_DIFFICULTY = 1.25f;
        Game_Manager.MAX_HEALTH = 100;
    }

    public void setHard()
    {
        EndGame.difficulty = EndGame.Difficulty.HARD;
        Game_Manager.MAX_DIFFICULTY = 1.5f;
        Game_Manager.MAX_HEALTH = 100;
    }

    public void setExtreme()
    {
        EndGame.difficulty = EndGame.Difficulty.EXTREME;
        Game_Manager.MAX_DIFFICULTY = 2.5f;
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
}
