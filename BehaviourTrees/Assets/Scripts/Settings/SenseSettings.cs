using UnityEngine;

namespace BehaviourTrees
{
    [System.Serializable]
    public struct SenseSettings
    {
        public float maxRange;
        public LayerMask mask;
        public float maxViewingAngle;
    }
}