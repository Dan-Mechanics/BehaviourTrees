namespace BehaviourTrees
{
    public class Idle : INode
    {
        public Status Evaluate() => Status.Running;
    }
}