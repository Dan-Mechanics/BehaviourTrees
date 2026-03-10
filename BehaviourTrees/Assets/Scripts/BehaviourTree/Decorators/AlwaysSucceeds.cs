using System;

namespace BehaviourTrees
{
    public class AlwaysSucceeds : INode
    {
        private readonly INode node;

        public AlwaysSucceeds(INode node)
        {
            this.node = node;
        }

        public Status Process(ref string name)
        {
            Status status = node.Process(ref name);
            if (status == Status.Failure)
                return Status.Success;

            return status;
        }

        public void Reset() => node.Reset();
    }
}