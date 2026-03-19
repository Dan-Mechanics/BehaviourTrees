using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Parallel : INode, IBlackboardRequired, IResettable
    {
        private readonly List<INode> nodes = new List<INode>();
        private readonly List<IResettable> resettables = new List<IResettable>();
        private readonly List<IBlackboardRequired> blackboardRequireds = new List<IBlackboardRequired>();

        public Parallel(params INode[] nodes)
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

        public void AssignBlackboard(Blackboard blackboard)
        {
            blackboardRequireds.ForEach(x => x.AssignBlackboard(blackboard));
        }

        public void Reset() => resettables.ForEach(x => x.Reset());
    }
}
