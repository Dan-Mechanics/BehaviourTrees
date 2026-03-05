using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Sequence : Node
    {
        private readonly Node[] nodes;
        private int index;

        public Sequence(params Node[] nodes) 
        {
            this.nodes = nodes;
        }
        
        public override BehaviourResult Update()
        {
            // THIS SYNTAX IS CRAZY.
            for (; index < nodes.Length; index++)
            {
                switch (nodes[index].Update())
                {
                    case BehaviourResult.Running:
                        return BehaviourResult.Running;
                    case BehaviourResult.Failed:
                        // RESET.
                        index = 0;
                        return BehaviourResult.Failed;
                    case BehaviourResult.Success:
                        continue;
                    default:
                        break;
                }
            }

            // RESET.
            index = 0;
            return BehaviourResult.Success;
        }
    }
}
