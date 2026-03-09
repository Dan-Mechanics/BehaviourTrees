using UnityEngine;

namespace BehaviourTrees
{
    public interface INode
    {
        Status Evaluate();
    }
}
