using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Selector : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private int index;

        public Selector(params INode[] nodes)
        {
            this.nodes = nodes.ToList();
        }

        public void Add(INode node) => nodes.Add(node);

        public Status Process(out string name)
        {
            name = GetType().ToString();
            for (; index < nodes.Count; index++)
            {
                switch (nodes[index].Process(out name))
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        index = 0;
                        return Status.Success;
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
