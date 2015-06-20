using UnityEngine;
using System.Collections;

public class Map_Info : MonoBehaviour {
    private GameObject[,] tileMap;
    
    public void setTileMap(GameObject[,] tileMap)
    {
        this.tileMap = tileMap;
    }

    public GameObject[,] getTileMap()
    {
        return this.tileMap;
    }
}
