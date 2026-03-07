using UnityEngine;

namespace BehaviourTrees
{
    public class Node
    {
        public virtual Status Process() => Status.Running;
    }
}
