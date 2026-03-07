using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Sequence : Node
    {
        private readonly Node[] nodes;
        private int index;

        public Sequence(params Node[] nodes)
        {
            this.nodes = nodes;
        }

        public override Status Process()
        {
            for (; index < nodes.Length; index++)
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
