using System;

namespace BehaviourTrees
{
    public class Conditional : INode
    {
        private readonly INode input;
        private readonly INode a;
        private readonly INode b;

        public Conditional(INode input, INode a, INode b)
        {
            this.input = input;
            this.a = a;
            this.b = b;
        }

        public Status Process(out string name)
        {
            name = GetType().ToString();
            if (input.Process(out name) == Status.Success)
            {
                return a.Process(out name);
            }

            return b.Process(out name);
        }
    }
}