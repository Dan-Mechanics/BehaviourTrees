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

        public Status Process(out string name)
        {
            Status status = node.Process(out name);
            if (status == Status.Failure)
                return Status.Success;

            return status;
        }
    }
}