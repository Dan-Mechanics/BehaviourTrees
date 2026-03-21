using System;

namespace BehaviourTrees
{
    public class Condition : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }
        private readonly string inputKey;

        public Condition(string inputKey)
        {
            this.inputKey = inputKey;
        }

        public void AssignBlackboard(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            if (Blackboard.GetValue<bool>(inputKey))
                return Status.Success;

            return Status.Failed;
        }
    }
}