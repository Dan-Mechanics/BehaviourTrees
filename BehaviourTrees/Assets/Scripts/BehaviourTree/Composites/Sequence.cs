using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Sequence : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private readonly bool allowExternalReset;
        private int index;

        public Sequence(bool allowExternalReset, params INode[] nodes)
        {
            this.nodes = nodes.ToList();
            this.allowExternalReset = allowExternalReset;
        }

        public void Add(INode node) => nodes.Add(node);

        public Status Process(ref string name)
        {
            name = GetType().FullName;
            if (index < nodes.Count)
            {
                switch (nodes[index].Process(ref name))
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        index = 0;
                        return Status.Failure;
                    default:
                        index++;
                        return index >= nodes.Count ? Status.Success : Status.Running;
                }
            }

            index = 0;
            Reset();
            return Status.Success;
        }

        public void Reset()
        {
           if (allowExternalReset)
                index = 0;

            nodes.ForEach(x => x.Reset());
        }
    }
}
