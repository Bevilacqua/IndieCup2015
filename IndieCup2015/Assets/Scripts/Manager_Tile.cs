using UnityEngine;
using System.Collections;

public class Manager_Tile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        transform.position = new Vector3(transform.position.x, .25f, transform.position.z);
    }

    void OnMouseExit()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}
