using System;

namespace BehaviourTrees
{
    public class Conditional : INode
    {
        private readonly Func<bool> predicate;
        private readonly INode node;

        public Conditional(Func<bool> predicate, INode node)
        {
            this.predicate = predicate;
            this.node = node;
        }

        public Status Process()
        {
            if (predicate())
                return node.Process();

            return Status.Failure;
        }
    }
}