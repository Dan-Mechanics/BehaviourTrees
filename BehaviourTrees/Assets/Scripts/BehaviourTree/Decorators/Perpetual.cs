using System;

namespace BehaviourTrees
{
    public class Perpetual : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }
        private readonly INode node;

        public Perpetual(INode node)
        {
            this.node = node;
        }

        public void AssignBlackboard(Blackboard blackboard)
        {
            Blackboard = blackboard;
            if (node is IBlackboardRequired blackboardRequired)
                blackboardRequired.AssignBlackboard(blackboard);
        }

        public Status Process(ref string name)
        {
            node.Process(ref name);
            return Status.Running;
        }
    }
}