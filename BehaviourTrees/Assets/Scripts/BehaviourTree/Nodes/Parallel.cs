using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Parallel : Node
    {
        private readonly List<Node> nodes = new List<Node>();
        public void Add(Node node) => nodes.Add(node);

        public override Status Process()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Status status = nodes[i].Process();
                if (status != Status.Running)
                    return status;
            }

            return Status.Running;
        }
    }
}
