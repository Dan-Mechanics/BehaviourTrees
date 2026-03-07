using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Sequence : Node
    {
        private readonly List<Node> nodes = new List<Node>();
        private int index;

        public void Add(Node node) => nodes.Add(node);

        public override Status Process()
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
