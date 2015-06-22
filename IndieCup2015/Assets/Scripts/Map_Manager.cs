using UnityEngine;
using System.Collections;

public class Map_Manager : MonoBehaviour {
    public int width = 10;
    public int height = 10;

    public Vector2 spawnLocation = new Vector2();

    public Map_Creator creator;
    private Map_Info map_info;

    private GameObject map;
	// Use this for initialization
	void Start () {
        creator = gameObject.GetComponent<Map_Creator>();
        map = creator.createMap(height, width);

        map_info = map.GetComponent<Map_Info>();
        map_info.getTileMap()[(int)spawnLocation.x, (int)spawnLocation.y].GetComponent<Tile_Manager>().liftTile();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

