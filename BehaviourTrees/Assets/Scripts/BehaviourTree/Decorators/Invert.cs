namespace BehaviourTrees
{
    public class Invert : INode
    {
        private readonly INode node;

        public Invert(INode node)
        {
            this.node = node;
        }

        public Status Process(out string name)
        {
            // FUNNY SYNTAX HERE:
            return node.Process(out name) switch
            {
                Status.Failure => Status.Success,
                Status.Success => Status.Failure,
                _ => Status.Standby,
            };
        }
    }
}