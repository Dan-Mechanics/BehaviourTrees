using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Selector : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private int index;

        public void Add(INode node) => nodes.Add(node);

        public Status Process()
        {
            for (; index < nodes.Count; index++)
            {
                switch (nodes[index].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        index = 0;
                        return Status.Failure;
                    case Status.Failure:
                        continue;
                    default:
                        break;
                }
            }

            index = 0;
            return Status.Success;
        }
    }
}
