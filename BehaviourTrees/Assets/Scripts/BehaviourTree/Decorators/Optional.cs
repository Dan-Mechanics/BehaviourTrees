using System;

namespace BehaviourTrees
{
    public class Optional : INode, IBlackboardRequired
    {
        private readonly INode node;

        public Optional(INode node)
        {
            this.node = node;
        }

        public void AssignBlackboard(Blackboard blackboard)
        {
            if (node is IBlackboardRequired blackboardRequired)
                blackboardRequired.AssignBlackboard(blackboard);
        }

        public Status Process(ref string name)
        {
            Status status = node.Process(ref name);
            if (status == Status.Failed)
                return Status.Success;

            return status;
        }
    }
}