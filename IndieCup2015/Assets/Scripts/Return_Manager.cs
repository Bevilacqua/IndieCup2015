using UnityEngine;
using System.Collections;

public class Return_Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void returnToMainMenu()
    {
        Application.LoadLevel("menu");
    }
}
