using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Non_Mono;

public class Map_Manager : MonoBehaviour {
    public int width = 10;
    public int height = 10;

    public Vector2 spawnLocation = new Vector2();
    public Vector2 goalLocation = new Vector2();

    public Map_Creator creator;
    private Map_Info map_info;

    private GameObject map;
	// Use this for initialization
	void Start () {
        creator = gameObject.GetComponent<Map_Creator>();
        map = creator.createMap(height, width);

        map_info = map.GetComponent<Map_Info>();
        goalLocation.Set((int)width / 2, (int)height / 2);
        foreach(Node node in map_info.init(spawnLocation, goalLocation))
        {
            node.getGameObject().GetComponent<Tile_Manager>().liftTile();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

