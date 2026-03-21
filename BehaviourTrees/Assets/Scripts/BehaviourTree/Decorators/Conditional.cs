using System;

namespace BehaviourTrees
{
    public class Conditional : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }
        private readonly bool invert;
        private readonly string inputKey;
        private readonly INode node;

        public Conditional(string inputKey, INode node, bool invert = false)
        {
            this.inputKey = inputKey;
            this.node = node;
            this.invert = invert;
        }

        public void AssignBlackboard(Blackboard blackboard)
        {
            Blackboard = blackboard;
            if (node is IBlackboardRequired blackboardRequired)
                blackboardRequired.AssignBlackboard(blackboard);
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            if (Blackboard.GetValue<bool>(inputKey) != invert)
                return node.Process(ref name);

            return Status.Failed;
        }
    }
}