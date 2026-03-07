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
            for (; index < nodes.Count; index++)
            {
                switch (nodes[index].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failed:
                        // RESET.
                        index = 0;
                        return Status.Failed;
                    case Status.Success:
                        continue;
                    default:
                        break;
                }
            }

            // RESET.
            index = 0;
            return Status.Success;
        }
    }
}
