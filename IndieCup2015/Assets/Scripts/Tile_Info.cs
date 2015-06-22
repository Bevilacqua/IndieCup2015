using UnityEngine;
using System.Collections;
using Assets.Scripts.Non_Mono;

public class Tile_Info : MonoBehaviour {
    public bool transversable = false;
    
    private Node node;
    private Vector2 tileMapCoords;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void assignInfo(Vector2 tileMapCoords)
    {
        this.tileMapCoords = tileMapCoords;

        if (transversable)
            node = new Node(this.tileMapCoords, gameObject);
    }

    public Node getNode()
    {
        return this.node;
    }


    public Vector2 getTileMapCoords()
    {
        return this.tileMapCoords;
    }

    public void setTransversable(bool transversable)
    {
        this.transversable = transversable;
    }
}
