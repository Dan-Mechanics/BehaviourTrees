using System;

namespace BehaviourTrees
{
    public class Conditional : INode
    {
        private Func<bool> predicate;
        private INode a;
        private INode b;

        public Conditional(Func<bool> predicate, INode a, INode b)
        {
            this.predicate = predicate;
            this.a = a;
            this.b = b;
        }

        public Status Process()
        {
            Status status = (predicate() ? a : b).Process();
            if (status == Status.Failed)
                return Status.Failed;

            return Status.Running;
        }
    }
}