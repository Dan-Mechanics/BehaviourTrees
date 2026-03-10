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

        public Status Process(out string name)
        {
            name = GetType().ToString();
            return predicate() ? Status.Success : Status.Failure;
        }
    }
}