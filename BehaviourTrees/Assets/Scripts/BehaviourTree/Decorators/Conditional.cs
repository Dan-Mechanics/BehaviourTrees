using System;

namespace BehaviourTrees
{
    public class Condition : INode
    {
        private readonly Func<bool> predicate;

        public Condition(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            if (predicate())
                return Status.Success;

            return Status.Failed;
        }
    }
}