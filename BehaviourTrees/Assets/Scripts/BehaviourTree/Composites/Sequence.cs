using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Sequence : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private int index;

        public Sequence(params INode[] nodes)
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
                    case Status.Standby:
                        return Status.Standby;
                    case Status.Failure:
                        index = 0;
                        return Status.Failure;
                    case Status.Success:
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
