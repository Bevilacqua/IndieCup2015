using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Non_Mono;

public class Map_Info : MonoBehaviour {
    public float scale = 2.5f;

    private GameObject[,] tileMap;

    private bool shiftedRowOne = false;
    
    private Graph mapGraph;
    private List<Node> objectivePath = new List<Node>();

    private int tileMapWidth, tileMapHeight;
    private Node startNode, endNode;

    public List<Node> init(Vector2 startNode, Vector2 endNode)
    {
        populateGraph();
        crawlPaths(mapGraph.getNodeList());
//        mapGraph.printGraph();
        this.startNode = tileMap[(int)startNode.x, (int)startNode.y].GetComponent<Tile_Info>().getNode();
        this.endNode = tileMap[(int)endNode.x, (int)endNode.y].GetComponent<Tile_Info>().getNode();

        //DEBUG
        this.startNode.getGameObject().GetComponent<Tile_Manager>().liftTile();
        this.endNode.getGameObject().GetComponent<Tile_Manager>().liftTile();

        return createObjectivePath();
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
       
        foreach(GameObject currentGameObject in tileMap) {
            currentTileInfo = currentGameObject.GetComponent<Tile_Info>();    

            if(currentTileInfo.transversable)
            {
                mapGraph.addNode(currentTileInfo.getNode());
            }
        }

        int i = 0;
        foreach(Node node in mapGraph.getNodeList())
        {
            node.setIndex(i);
            i++;
        }
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
            bool isOffset = false;

             if(shiftedRowOne && nodeList[i].getMapCoordinates().y % 2 != 0)
             {
                 isOffset = true;
             }
             else if(!shiftedRowOne && nodeList[i].getMapCoordinates().y % 2 == 0)
             {
                 isOffset = true;
             }

             if (!shiftedRowOne && nodeList[i].getMapCoordinates().y == 0)
             {
                 isOffset = true;
             }

             //Possible Neighbors
            if(isOffset)
            {
                if (nodeTileLocation.x > 0) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x - 1, (int)nodeTileLocation.y]);
                if (nodeTileLocation.y < tileMapHeight - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x, (int)nodeTileLocation.y + 1]);
                if (nodeTileLocation.x < tileMapWidth - 1 && nodeTileLocation.y < tileMapHeight - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x + 1, (int)nodeTileLocation.y + 1]);
                if (nodeTileLocation.x < tileMapWidth - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x + 1, (int)nodeTileLocation.y]);
                if (nodeTileLocation.x < tileMapWidth - 1 && nodeTileLocation.y > 0) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x + 1, (int)nodeTileLocation.y - 1]);
                if (nodeTileLocation.y > 0) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x, (int)nodeTileLocation.y - 1]);  
            }
            else
            {
                if (nodeTileLocation.x > 0) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x - 1, (int)nodeTileLocation.y]);
                if (nodeTileLocation.x > 0 && nodeTileLocation.y < tileMapHeight - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x - 1, (int)nodeTileLocation.y + 1]);
                if (nodeTileLocation.y < tileMapHeight - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x, (int)nodeTileLocation.y + 1]);
                if (nodeTileLocation.x < tileMapWidth - 1) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x + 1, (int)nodeTileLocation.y]);
                if (nodeTileLocation.y > 0) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x, (int)nodeTileLocation.y - 1]);
                if (nodeTileLocation.x > 0 && nodeTileLocation.y > 0) possibleNeighbors.Add(tileMap[(int)nodeTileLocation.x, (int)nodeTileLocation.y - 1]);  
            }
                
            
            foreach(GameObject neighbor in possibleNeighbors)
            {
                if (neighbor.GetComponent<Tile_Info>().transversable)
                {
                    nodeList[i].addEdge(neighbor.GetComponent<Tile_Info>().getNode());
                }
            }
        }
    }

    public List<Node> createObjectivePath()
    {
        List<Node> finalPath = new List<Node>();
        List<Node> came_from = new List<Node>();

        List<Node> closedSet = new List<Node>();
        List<Node> openSet = new List<Node>();

        openSet.Add(startNode);

        startNode.setGScore(0);
        startNode.setFScore(startNode.getGScore() + calculateHeuristic(startNode, endNode));

        while(openSet.Count != 0)
        {
            int index = lowestFScoreInList(openSet);
            Node current = openSet[index];

            if(current.getIndex() == endNode.getIndex())
            {
                came_from.Add(endNode);
                return came_from;
            }

            openSet.RemoveAt(index);
            closedSet.Add(current);

            foreach(Node neighbor in current.getNeighboringNodes())
            {
                bool foundInClosedSet = false;

                foreach(Node n in closedSet)
                {
                    if (n.getMapCoordinates() == neighbor.getMapCoordinates())
                    {
                        foundInClosedSet = true;
                        break;
                    }
                }

                if (foundInClosedSet) continue;

                int newGScore = (int)neighbor.getGScore() + 1;

                bool foundInOpenSet = false;

                foreach(Node n in openSet)
                {
                    if(n.getIndex() == neighbor.getIndex())
                    {
                        foundInOpenSet = true;
                        break;
                    }
                }

                if(!foundInOpenSet) //Bug possibility
                {
                    if(neighbor.getGScore() == -1f)
                    {
                        came_from.Add(current);
                        neighbor.setGScore(newGScore);
                        neighbor.setFScore(newGScore + calculateHeuristic(neighbor, endNode));
                        openSet.Add(neighbor);
                    }
                   
                }

            }
        }
        Debug.Log("Path failed to generate");
        return null;
    }

    private float calculateHeuristic(Node start, Node goal)
    {
//        float yDiff = (start.getMapCoordinates().y - goal.getMapCoordinates().y);

//        float xDiff = (start.getMapCoordinates().x - goal.getMapCoordinates().x);

        float xDiffRaw = start.getGameObject().transform.position.x - goal.getGameObject().transform.position.x;
        float yDiffRaw = start.getGameObject().transform.position.z - goal.getGameObject().transform.position.z;
        float dx = Mathf.Abs(xDiffRaw);
        float dy = Mathf.Abs(yDiffRaw);

        return 1f * (dx + dy) + (1.41421356237f - 2f) * Mathf.Min(dx, dy);
    }

    /// <summary>
    /// Searches node list for lowest Fscore.
    /// </summary>
    /// <param name="nodeList">List to search in.</param>
    /// <returns>The index of the node with the lowest F score</returns>
    private int lowestFScoreInList(List<Node> nodeList)
    {
        int lowest = 0;

        for(int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].getFScore() < nodeList[lowest].getFScore())
                lowest = i;
        }

        return lowest;
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

    public void setFirstRowShifted(bool shifted)
    {
        this.shiftedRowOne = shifted;
    }

    public bool getShiftedRowOne()
    {
        return this.shiftedRowOne;
    }
}
