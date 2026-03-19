using UnityEngine;

namespace BehaviourTrees
{
    public interface INode
    {
        Status Process(ref string name);
    }
}
