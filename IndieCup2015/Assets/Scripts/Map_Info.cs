using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Non_Mono;

public class Map_Info : MonoBehaviour {
    public float scale = 2.5f;

    private GameObject[,] tileMap;
    
    private Graph mapGraph;
    private List<Node> objectivePath = new List<Node>();

    private int tileMapWidth, tileMapHeight;
    private Node startNode, endNode;

    void Start()
    {
        populateGraph();
    }

    public void setTileMap(GameObject[,] tileMap)
    {
        this.tileMap = tileMap;
    }

    public GameObject[,] getTileMap()
    {
        return this.tileMap;
    }

    public void setDimensions(int width, int height)
    {
        this.tileMapWidth = width;
        this.tileMapHeight = height;
    }

    /// <summary>
    /// Add transversable nodes to the graph.
    /// </summary>
    public void populateGraph()
    {
        mapGraph = new Graph();
        Tile_Info currentTileInfo = null;

        foreach(GameObject gameObject in tileMap) {
            currentTileInfo = gameObject.GetComponent<Tile_Info>();    

            if(currentTileInfo.transversable)
            {
                mapGraph.addNode(currentTileInfo.getNode());
            }
        }

        crawlPaths(mapGraph.getNodeList());

        mapGraph.printGraph();
    }

    /// <summary>
    /// Crawl all paths to find viable paths and add edges.
    /// </summary>
    public void crawlPaths(List<Node> nodeList)
    {
        for(int i = 0; i < nodeList.Count; i++)
        {
            List<GameObject> possibleNeighbors = new List<GameObject>();
            Vector2 nodeTileLocation = new Vector2(nodeList[i].getGameObject().GetComponent<Tile_Info>().getTileMapCoords().x, nodeList[i].getGameObject().GetComponent<Tile_Info>().getTileMapCoords().y);

            //Possible Neighbors
            if(nodeTileLocation.x > 0) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x - 1, (int)nodeTileLocation.y]);
            if(nodeTileLocation.x > 0 && nodeTileLocation.y < tileMapHeight - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x - 1, (int)nodeTileLocation.y + 1]);
            if(nodeTileLocation.y < tileMapHeight - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x , (int)nodeTileLocation.y + 1]);
            if(nodeTileLocation.x < tileMapWidth - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x + 1, (int)nodeTileLocation.y]);
            if(nodeTileLocation.x < tileMapWidth - 1 && nodeTileLocation.y > 0) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x + 1, (int)nodeTileLocation.y - 1]);
            if(nodeTileLocation.y > 0)possibleNeighbors.Add(tileMap[(int) nodeTileLocation.x, (int) nodeTileLocation.y - 1]);

            
//            possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x - 1, (int)nodeTileLocation.y]);
                                            
            //TODO: finish adding neighbors

            foreach(GameObject gameObject in possibleNeighbors)
            {
                if(gameObject.GetComponent<Tile_Info>().transversable)
                {
                    //Draw edge if it does not exist
                    if(!nodeList[i].doesEdgeExist(gameObject.GetComponent<Tile_Info>().getNode()))
                        nodeList[i].addEdge(gameObject.GetComponent<Tile_Info>().getNode());
                }
            }
        }
        
    }

    private List<Node> createObjectivePath()
    {
        List<Node> finalPath = new List<Node>();

        List<Node> closedSet = new List<Node>();
        List<Node> openSet = new List<Node>();
        //TODO: Add start node to openSet

        return finalPath;
    }

    private float calculateHeuristic(Node start, Node goal)
    {
        float yDiff = (start.getMapCoordinates().y - goal.getMapCoordinates().y);
        yDiff = Mathf.Abs(yDiff);

        float xDiff = (start.getMapCoordinates().x - goal.getMapCoordinates().x);
        xDiff = Mathf.Abs(xDiff);

        return yDiff + xDiff;
    }

    public void updateScale()
    {
        transform.localScale *= scale;
    }

    public void updateKeyPathValues(Node start, Node end)
    {
        this.startNode = start;
        this.endNode = end;
    }
}
