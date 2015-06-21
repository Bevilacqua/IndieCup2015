using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Non_Mono
{
    class Graph
    {
        private List<Node> nodeList = new List<Node>();

        public void addNode(Node node)
        {
            nodeList.Add(node);
        }

        public Node getNode(int index)
        {
            return nodeList[index];
        }

        public void printMap()
        {
            int index = 0;
            foreach(Node node in nodeList) 
            {
                Debug.Log("Node # " + index + " | @ X: " + node.getMapCoordinates().x + " , Y: " + node.getMapCoordinates().y + " | The node has " + node.getNeighborCount() + " neighbor[s]");
                index++;   
            }
        }
    }
}
