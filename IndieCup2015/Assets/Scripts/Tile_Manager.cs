using UnityEngine;
using System.Collections;

public class Tile_Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        liftTile();
    }

    void OnMouseExit()
    {
        lowerTile();
    }

    public void liftTile()
    {
        transform.position = new Vector3(transform.position.x, .25f, transform.position.z);
    }

    public void lowerTile()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}
