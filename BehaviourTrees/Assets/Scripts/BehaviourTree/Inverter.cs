namespace BehaviourTrees
{
    public class Inverter : Node
    {
        private readonly Node node;

        public Inverter(Node node)
        {
            this.node = node;
        }

        public override Status Process()
        {
            Status result = node.Process();
            if (result == Status.Failed)
                return Status.Success;

            if (result == Status.Success)
                return Status.Failed;

            return result;
        }
    }
}