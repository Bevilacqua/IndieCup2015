using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Non_Mono
{

    public class Node
    {
        private List<Edge> usableEdges = new List<Edge>();
        //Map coordinates of tilemap
        private Vector2 tileMapCoordinates;

        //Game object corresponding to current node
        private GameObject gameObject;
        //Position extracted from current node's game object
        private Vector3 position;

        private int index = 0;

        private float gScore = -1f;
        private float fScore = -1f;

        /// <summary>
        /// Node constructor
        /// </summary>
        /// <param name="tileMapCoordinates">The map coordinates of the node on the tile map</param>
        /// <param name="gameObject">The game object cooresponding to the node</param>
        public Node(Vector2 tileMapCoordinates, GameObject gameObject)
        {
            this.tileMapCoordinates = tileMapCoordinates;
            this.gameObject = gameObject;
            this.position = this.gameObject.transform.position;
        }

        /// <summary>
        /// Generate a list of all negihboring nodes. Generated using the list of all usable edges
        /// </summary>
        /// <returns>A list of neightboring nodes</returns>
        public List<Node> getNeighboringNodes()
        {
            List<Node> neighbors = new List<Node>();

            foreach(Edge edge in usableEdges)
            {
                Node given = edge.determineNeighbor(this);
                if (given != null)
                    neighbors.Add(given);
            }
        
            return neighbors;
        }

        public GameObject getGameObject()
        {
            return this.gameObject;
        }

        public Vector3 getPosition()
        {
            return this.position;
        }

        public Vector2 getMapCoordinates()
        {
            return this.tileMapCoordinates;
        }

        public int getNeighborCount()
        {
            return getNeighboringNodes().Count();
        }

        public void addEdge(Node destination)
        {
            if (!doesEdgeExist(destination) && !destination.doesEdgeExist(this))
            {
                Edge e = new Edge(this, destination);
                usableEdges.Add(e);
                destination.usableEdges.Add(e);
            }
            else
            {
                Debug.Log("Edge already exists");
            }
                  
        }

        public bool doesEdgeExist(Node destination)
        {
            foreach(Edge edge in usableEdges)
            {
                if (edge.getNodes()[0].Equals(destination) || edge.getNodes()[1].Equals(destination) || edge.getNodes()[0].getIndex() == destination.getIndex() || edge.getNodes()[1].getIndex() == destination.getIndex())
                    return true;
            }

            return false;
        }

        public float getGScore()
        {
            return gScore;
        }

        public float getFScore()
        {
            return fScore;
        }

        public void setGScore(float gScore)
        {
            this.gScore = gScore;
        }

        public void setFScore(float fScore)
        {
            this.fScore = fScore;
        }

        public void setIndex(int index)
        {
            this.index = index;
        }

        public int getIndex()
        {
            return this.index;
        }
    }
}
