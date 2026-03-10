namespace BehaviourTrees
{
    public class Inverter : INode
    {
        private readonly INode node;

        public Inverter(INode node)
        {
            this.node = node;
        }

        public Status Process()
        {
            Status result = node.Process();
            if (result == Status.Failure)
                return Status.Success;

            if (result == Status.Success)
                return Status.Failure;

            return result;
        }
    }
}