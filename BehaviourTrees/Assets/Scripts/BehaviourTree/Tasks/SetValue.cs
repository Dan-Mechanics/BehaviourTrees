namespace BehaviourTrees
{
    public class SetValue<T> : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }

        private readonly string outputKey;
        private readonly T value;

        public SetValue(string outputKey, T value)
        {
            this.outputKey = outputKey;
            this.value = value;
        }

        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;

        public Status Process(ref string name)
        {
            Blackboard.SetValue(outputKey, value);
            return Status.Success;
        }
    }
}