using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Parallel : INode
    {
        private readonly List<INode> nodes = new List<INode>();

        public void Add(INode node) => nodes.Add(node);

        public Status Process()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Status status = nodes[i].Process();
                if (status == Status.Failure)
                    return Status.Failure;
            }

            return Status.Running;
        }
    }
}
