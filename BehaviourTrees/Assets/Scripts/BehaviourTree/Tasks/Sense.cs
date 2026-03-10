using UnityEngine;

namespace BehaviourTrees
{
    public class Sense : INode
    {
        private readonly Settings settings;
        private readonly Transform self;
        private readonly Transform target;

        public Sense(Settings settings, Transform self, Transform target)
        {
            this.settings = settings;
            this.self = self;
            this.target = target;
        }

        public Status Process()
        {
            Debug.Log(target.name);
            if (target == null)
                return Status.Failure;
            
            Vector3 dir = (target.position - self.position).normalized;
            bool hasFound = Physics.Raycast(self.position, dir, out RaycastHit hit, settings.maxRange,
                settings.mask, QueryTriggerInteraction.Ignore) && hit.transform == target &&
                Vector3.Angle(self.forward, dir) <= settings.maxViewingAngle;

            if (hasFound)
                return Status.Success;

            return Status.Failure;
        }

        [System.Serializable]
        public struct Settings
        {
            public float maxRange;
            public LayerMask mask;
            public float maxViewingAngle;
        }
    }
}