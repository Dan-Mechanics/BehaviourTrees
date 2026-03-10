namespace BehaviourTrees
{
    public class Invert : INode
    {
        private readonly INode node;

        public Invert(INode node)
        {
            this.node = node;
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            return node.Process(ref name) switch
            {
                Status.Failure => Status.Success,
                Status.Success => Status.Failure,
                _ => Status.Running,
            };
        }
    }
}