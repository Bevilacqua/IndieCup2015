using UnityEngine;
using System.Collections;

public class About_Manager : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void goToMenu()
    {
        Application.LoadLevel("menu");
    }
}