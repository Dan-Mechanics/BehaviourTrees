using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Selector : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private int index;

        public void Add(INode node) => nodes.Add(node);

        public Status Process()
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
