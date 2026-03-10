namespace BehaviourTrees
{
    public class Idle : INode
    {
        public Status Process(out string name)
        {
            name = GetType().ToString();
            return Status.Standby;
        }
    }
}