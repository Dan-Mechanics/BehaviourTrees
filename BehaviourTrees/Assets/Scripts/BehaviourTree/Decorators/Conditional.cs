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

        public Status Process(out string name)
        {
            name = GetType().ToString();
            if (predicate())
                return node.Process(out name);

            return Status.Failure;
        }
    }
}