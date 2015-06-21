using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
