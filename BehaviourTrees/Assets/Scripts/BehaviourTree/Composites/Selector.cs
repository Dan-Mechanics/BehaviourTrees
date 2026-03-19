using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Selector : INode, IResettable, IBlackboardRequired
    {
        private readonly List<INode> nodes = new List<INode>();
        private readonly List<IResettable> resettables = new List<IResettable>();
        private readonly List<IBlackboardRequired> blackboardRequireds = new List<IBlackboardRequired>();

        public Selector(params INode[] nodes)
        {
            this.nodes = nodes.ToList();
            for (int i = 0; i < nodes.Length; i++)
            {
                Add(nodes[i]);
            }
        }

        public void Add(INode node)
        {
            nodes.Add(node);
            if (node is IResettable resettable)
                resettables.Add(resettable);

            if (node is IBlackboardRequired blackboardRequired)
                blackboardRequireds.Add(blackboardRequired);
        }

        public void Reset()
        {
            resettables.ForEach(x => x.Reset());
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            for (int i = 0; i < nodes.Count; i++)
            {
                switch (nodes[i].Process(ref name))
                {
                    case Status.Running:
                        return Status.Running;
                    default:
                        continue;
                }
            }

            return Status.Failed;
        }

        public void AssignBlackboard(Blackboard blackboard)
        {
            blackboardRequireds.ForEach(x => x.AssignBlackboard(blackboard));
        }
    }
}
