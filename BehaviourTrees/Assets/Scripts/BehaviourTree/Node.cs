using UnityEngine;

namespace BehaviourTrees
{
    public abstract class Node
    {
        public abstract Status Process();
        public virtual void OnEnter() { Debug.Log($"Entered {GetType()}."); }
        public virtual void OnExit() { Debug.Log($"Exited {GetType()}."); }
    }
}
