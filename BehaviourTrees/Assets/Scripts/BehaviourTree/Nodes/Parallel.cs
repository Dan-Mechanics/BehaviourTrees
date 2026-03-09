using System.Collections.Generic;

namespace BehaviourTrees
{
    /// <summary>
    /// Runs all its children at the same time.
    /// </summary>
    public class Parallel : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        public void Add(INode node) => nodes.Add(node);

        public Status Process()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Status status = nodes[i].Process();
            }

            return Status.Running;
        }
    }
}
