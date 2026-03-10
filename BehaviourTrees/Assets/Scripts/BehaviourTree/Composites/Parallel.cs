using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Parallel : INode
    {
        private readonly List<INode> nodes = new List<INode>();

        public Parallel(params INode[] nodes)
        {
            this.nodes = nodes.ToList();
        }

        public void Add(INode node) => nodes.Add(node);

        public Status Process(ref string name)
        {
            name = GetType().Name;
            Status result = Status.Running;
            for (int i = 0; i < nodes.Count; i++)
            {
                Status status = nodes[i].Process(ref name);
                if (status != Status.Running)
                    result = status;
            }

            return result;
        }
    }
}
