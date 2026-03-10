using System;

namespace BehaviourTrees
{
    public class FailIsSucceed : INode
    {
        private readonly INode node;

        public FailIsSucceed(INode node)
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