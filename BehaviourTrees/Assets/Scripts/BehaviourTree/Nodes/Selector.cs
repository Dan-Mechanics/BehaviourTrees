using System.Collections.Generic;

namespace BehaviourTrees
{
    /// <summary>
    /// Runs all its children one by one until a node succeeds.
    /// </summary>
    public class Selector : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private int index;

        public void Add(INode node) => nodes.Add(node);

        public Status Process()
        {
            Status status = nodes[index].Process();
            if (status == Status.Success)
            {
                index = 0;
                return Status.Success;
            }

            index++;
            if (index >= nodes.Count - 1)
                index = 0;

            return Status.Running;
        }
    }
}
