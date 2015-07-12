using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GJ_Manager : MonoBehaviour {
    private bool displayedLogin;
    private bool loginScreenDisplayed;

    private GameObject difficultyScreen;

	// Use this for initialization
	void Start () {
        GameObject.Find("Canvas").GetComponent<Animator>().Play("MENULOAD");
        difficultyScreen = GameObject.Find("DIFFICULTYSELECT");
	}
	
	// Update is called once per frame
	void Update () {
        if (GameJolt.API.Manager.Instance.CurrentUser != null && !displayedLogin && GameJolt.API.Manager.Instance.CurrentUser.IsAuthenticated)
        {
            Debug.Log("Login: " + GameJolt.API.Manager.Instance.CurrentUser.Name);
            GameJolt.UI.Manager.Instance.QueueNotification("Welcome " + GameJolt.API.Manager.Instance.CurrentUser.Name + "!", GameJolt.API.Manager.Instance.CurrentUser.Avatar);
            GameObject.Find("Canvas").GetComponent<Animator>().Play("DISPLAY_LOGIN_BUTTON_REVERSE");
            GameObject.Find("Name").GetComponent<Text>().text = "AS " + GameJolt.API.Manager.Instance.CurrentUser.Name;
            displayedLogin = true;
        }   
        else if(!loginScreenDisplayed)
        {
            GameObject.Find("Canvas").GetComponent<Animator>().Play("DISPLAY_LOGIN_BUTTON");
            loginScreenDisplayed = true;
        }
	}

    public void displayLoginScreen()
    {
        GameJolt.UI.Manager.Instance.ShowSignIn();
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

    public void showDifficultySelect()
    {
        difficultyScreen.SetActive(true);
        difficultyScreen.GetComponent<Animator>().Play("SHOW");
    }
}
