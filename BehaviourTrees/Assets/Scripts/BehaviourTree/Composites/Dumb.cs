using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Dumb : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        private readonly Status nextStatus;

        public Dumb(Status nextStatus, params INode[] nodes)
        {
            this.nextStatus = nextStatus;
            this.nodes = nodes.ToList();
        }

        public void Add(INode node) => nodes.Add(node);

        public Status Process(out string name)
        {
            name = GetType().ToString();
            Status result = Status.Standby;
            for (int i = 0; i < nodes.Count; i++)
            {
                result = nodes[i].Process(out name);
                if (result != nextStatus)
                    return result;
            }

            return result;
        }
    }
}
