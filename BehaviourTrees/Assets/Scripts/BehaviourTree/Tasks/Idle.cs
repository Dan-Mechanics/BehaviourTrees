namespace BehaviourTrees
{
    public class Idle : INode
    {
        public Status Process(ref string name)
        {
            name = GetType().Name;
            return Status.Running;
        }
    }
}