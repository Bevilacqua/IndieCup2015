using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        GameObject.Find("Manager_Game").GetComponent<Game_Manager>().reset(Game_Manager.MAX_HEALTH, Game_Manager.MAX_DIFFICULTY);
    }
}
