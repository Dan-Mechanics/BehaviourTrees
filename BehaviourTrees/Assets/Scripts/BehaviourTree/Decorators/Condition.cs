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

        public Status Process(ref string name)
        {
            name = GetType().Name;
            if (predicate())
                return node.Process(ref name);

            return Status.Failure;
        }
    }
}