using UnityEngine;

namespace BehaviourTrees
{
    public interface INode
    {
        Status Process(out string name);
    }
}
