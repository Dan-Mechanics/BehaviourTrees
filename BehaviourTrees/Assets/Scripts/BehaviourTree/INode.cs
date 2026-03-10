using UnityEngine;

namespace BehaviourTrees
{
    public interface INode
    {
        Status Process();
        string GetName() { return GetType().ToString(); }
    }
}
