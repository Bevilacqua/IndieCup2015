using UnityEngine;
using System.Collections;

public class PlayMenuLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().Play("MENULOAD");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
