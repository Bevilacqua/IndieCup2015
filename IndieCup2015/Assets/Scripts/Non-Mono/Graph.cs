using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Non_Mono
{
    public class Graph
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

        public List<Node> getNodeList()
        {
            return this.nodeList;
        }


        public void printGraph()
        {
            if (nodeList.Count == 0) return;
            foreach(Node node in nodeList) 
            {
                Debug.Log("Node # " + node.getIndex() + " | @ X: " + node.getMapCoordinates().x + "(" + node.getGameObject().transform.localPosition.x + ") , Y: " + node.getMapCoordinates().y + "(" + node.getGameObject().transform.localPosition.z + ") | The node has " + node.getNeighborCount() + " neighbor[s]");
            }
        }
    }
}
