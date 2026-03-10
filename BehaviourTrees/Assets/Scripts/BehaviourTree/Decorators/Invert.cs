namespace BehaviourTrees
{
    public class Invert : INode
    {
        private readonly INode node;

        public Invert(INode node)
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