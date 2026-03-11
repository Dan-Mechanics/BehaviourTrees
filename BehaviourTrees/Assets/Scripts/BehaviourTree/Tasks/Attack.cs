namespace BehaviourTrees
{
    public class Attack : INode
    {
        public Status Process(ref string name)
        {
            name = GetType().Name;
            return Status.Running;
        }
    }
}