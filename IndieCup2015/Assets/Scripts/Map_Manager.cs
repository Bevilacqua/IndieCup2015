using UnityEngine;
using System.Collections;

public class Map_Manager : MonoBehaviour {
    public int width = 10;
    public int height = 10;

    public Map_Creator creator;
	// Use this for initialization
	void Start () {
        creator = gameObject.GetComponent<Map_Creator>();
        creator.createMap(height, width);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

