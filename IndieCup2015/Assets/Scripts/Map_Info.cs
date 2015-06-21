using UnityEngine;
using System.Collections;
using Assets.Scripts.Non_Mono;

public class Map_Info : MonoBehaviour {
    private GameObject[,] tileMap;
    private Graph mapGraph;
    
    public void setTileMap(GameObject[,] tileMap)
    {
        this.tileMap = tileMap;
    }

    public GameObject[,] getTileMap()
    {
        return this.tileMap;
    }

    public void populateGraph()
    {
        //TODO: populate graph
    }
}
