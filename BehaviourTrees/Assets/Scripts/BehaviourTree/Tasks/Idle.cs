namespace BehaviourTrees
{
    public class Idle : INode
    {
        public Status Process() => Status.Running;
    }
}