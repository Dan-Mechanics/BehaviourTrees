using System.Collections.Generic;

namespace BehaviourTrees
{
    /// <summary>
    /// Runs all its children in sequence until one node fails.
    /// </summary>
    public class Sequence : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private int index;

        public void Add(INode node) => nodes.Add(node);

        public Status Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (INode node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}
