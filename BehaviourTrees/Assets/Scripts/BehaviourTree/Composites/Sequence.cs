using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Sequence : INode, IBlackboardRequired, IResettable
    {
        private readonly List<INode> nodes = new List<INode>();
        private readonly List<IResettable> resettables = new List<IResettable>();
        private readonly List<IBlackboardRequired> blackboardRequireds = new List<IBlackboardRequired>();

        private int index;
        private bool allowReset;

        public Sequence(params INode[] nodes)
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

        public void DisallowReset() => allowReset = true;

        public void AssignBlackboard(Blackboard blackboard)
        {
            blackboardRequireds.ForEach(x => x.AssignBlackboard(blackboard));
        }

        public Status Process(ref string name)
        {
            name = GetType().FullName;
            for (; index < nodes.Count; index++)
            {
                switch (nodes[index].Process(ref name))
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failed:
                        index = 0;
                        return Status.Failed;
                    case Status.Success:
                        continue;
                }
            }

            index = 0;
            return Status.Success;
        }

        public void Reset()
        {
            if (allowReset)
                return;

            resettables.ForEach(x => x.Reset());
            index = 0;
        }
    }
}
