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
            // FUNNY SYNTAX HERE:
            return node.Process() switch
            {
                Status.Failure => Status.Success,
                Status.Success => Status.Failure,
                _ => Status.Running,
            };
        }
    }
}