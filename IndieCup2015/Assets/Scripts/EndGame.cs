using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {
    public static int roundNumber = 0;
    public static Difficulty difficulty = Difficulty.NORMAL;

    public enum Difficulty {
        EASY,
        NORMAL,
        HARD,
        EXTREME
    }

	// Use this for initialization
	void Start () {
        GameObject.Find("POSTSCREEN").SetActive(false);
        GameObject.Find("ROUNDNUMBER").GetComponent<Text>().text = "You made it to round # " + roundNumber;
        if(GameJolt.API.Manager.Instance.CurrentUser != null && GameJolt.API.Manager.Instance.CurrentUser.IsAuthenticated)
        {
            GameObject.Find("SUBMITSCORE").GetComponent<Button>().interactable = false;
            GameJolt.API.Scores.Add(roundNumber, roundNumber + " rounds!", 81140, "");
            GameJolt.UI.Manager.Instance.ShowLeaderboards();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showLeaderboards()
    {
        GameJolt.UI.Manager.Instance.ShowLeaderboards();
    }

    public void returnToMenu()
    {
        Application.LoadLevel("menu");
    }

    public void postScore(Text text)
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                GameJolt.API.Scores.Add(roundNumber, roundNumber + " rounds!", text.text, 81174, "");
                break;
            case Difficulty.NORMAL:
                GameJolt.API.Scores.Add(roundNumber, roundNumber + " rounds!", text.text, 81140, "");
                break;
            case Difficulty.HARD:
                GameJolt.API.Scores.Add(roundNumber, roundNumber + " rounds!", text.text, 81768, "");
                break;
            case Difficulty.EXTREME:
                GameJolt.API.Scores.Add(roundNumber, roundNumber + " rounds!", text.text, 81769, "");
                break;
        }

        GameJolt.UI.Manager.Instance.ShowLeaderboards();   
    }
}
