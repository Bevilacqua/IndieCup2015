using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Non_Mono
{

    class Node
    {
        private List<Edge> usableEdges = new List<Edge>();
        //Map coordinates of tilemap
        private Vector2 tileMapCoordinates;

        //Game object corresponding to current node
        private GameObject gameObject;
        //Position extracted from current node's game object
        private Vector3 position;

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

            //TODO: Check for duplicate nodes
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
            return this.getMapCoordinates();
        }

        public int getNeighborCount()
        {
            return getNeighboringNodes().Count();
        }
    }
}
