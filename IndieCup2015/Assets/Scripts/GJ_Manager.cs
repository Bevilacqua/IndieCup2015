using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GJ_Manager : MonoBehaviour {
    private bool displayedLogin;
    private bool loginScreenDisplayed;

	// Use this for initialization
	void Start () {
        GameObject.Find("Canvas").GetComponent<Animator>().Play("MENULOAD");
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

    public void goToPlayScreen()
    {
        Application.LoadLevel("default");
    }
}
