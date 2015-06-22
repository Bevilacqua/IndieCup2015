using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Non_Mono
{
    public class Edge
    {
        private Node A;
        private Node B;

        public Edge(Node A, Node B)
        {
            this.A = A;
            this.B = B;
        }

        public Node determineNeighbor(Node testNode)
        {
            if (testNode.getPosition() == A.getPosition())
                return B;
            else if (testNode.getPosition() == B.getPosition())
                return A;
            else
                return null;
        }

        public Node[] getNodes()
        {
            return new Node[] {A, B};
        }
    }
}
