namespace BehaviourTrees
{
    public class Invert : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }
        private readonly INode node;

        public Invert(INode node)
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
            name = GetType().Name;
            Status status = node.Process(ref name);
            if (status == Status.Failure)
                return Status.Success;

            if (status == Status.Success)
                return Status.Failure;

            return Status.Running;
        }
    }
}