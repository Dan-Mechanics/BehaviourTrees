using System;

namespace BehaviourTrees
{
    public class Conditional : Node
    {
        private Node a;
        private Node b;
        private Func<bool> predicate;

        public Conditional(Node a, Node b, Func<bool> predicate)
        {
            this.a = a;
            this.b = b;
            this.predicate = predicate;
        }

        public override Status Process()
        {
            Status status = (predicate() ? a : b).Process();
            if (status == Status.Failed)
                return Status.Failed;

            return Status.Running;
        }
    }
}