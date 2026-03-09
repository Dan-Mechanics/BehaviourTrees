using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Sequence : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private int index;

        public void Add(INode node) => nodes.Add(node);

        public Status Process()
        {
            Status status = nodes[index].Process();
            if (status == Status.Failed)
                return Status.Failed;
            
            if (status == Status.Success)
            {
                index++;
                if (index >= nodes.Count - 1)
                    index = 0;
            }

            return Status.Running;
        }
    }
}
