using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Selector : Node
    {
        private readonly List<Node> nodes = new List<Node>();
        private int index;

        public void Add(Node node) => nodes.Add(node);

        public override Status Process()
        {
            if (index < nodes.Count)
            {
                switch (nodes[index].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failed:
                        break;
                    case Status.Success:
                        return Status.Success;
                    default:
                        index++;
                        return Status.Running;
                }
            }

            return Status.Failed;
        }
    }
}
