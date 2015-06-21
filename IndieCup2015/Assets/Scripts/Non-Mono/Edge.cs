using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Non_Mono
{
    class Edge
    {
        private Node A;
        private Node B;

        public Node determineNeighbor(Node testNode)
        {
            if (testNode.getPosition() == A.getPosition())
                return B;
            else if (testNode.getPosition() == B.getPosition())
                return A;
            else
                return null;
        }
    }
}
