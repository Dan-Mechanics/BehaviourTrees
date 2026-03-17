using System;
using System.Collections.Generic;
using System.Linq;

namespace BehaviourTrees
{
    public class Selector : INode
    {
        private readonly List<INode> nodes = new List<INode>();
        /// <summary>
        /// this is a mistake, ti should be local.
        /// First start with simple patorl and then chase and slowly add shit
        /// 
        /// </summary>
        private int index;

        public Selector(params INode[] nodes)
        {
            this.nodes = nodes.ToList();
        }

        public void Add(INode node) => nodes.Add(node);

        /// <summary>
        /// Todo: remove reset puilses,
        /// de sensor kan schirjven to blackboard
        /// Optional node of always
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Status Process(ref string name)
        {
            name = GetType().Name;
            if (index < nodes.Count)
            {
                switch (nodes[index].Process(ref name))
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    default:
                        index++;
                        return Status.Running;
                }
            }

            Reset();
            return Status.Failure;
        }

        public void Reset()
        {
            index = 0;
            nodes.ForEach(x => x.Reset());
        }
    }
}
