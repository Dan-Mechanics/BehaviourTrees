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
            settings.Verify();
            this.settings = settings;
            this.self = self;
            this.target = target;
        }

        public Status Process()
        {
            if (target == null)
                return Status.Failure;
            
            // RAYCAST + CHECK TAG + ANGLE BETWEEN.
            Vector3 dir = (target.position - self.position).normalized;
            bool hasFound = Physics.Raycast(self.position, dir, out RaycastHit hit, settings.maxRange,
                settings.mask, QueryTriggerInteraction.Ignore) && hit.collider.CompareTag(settings.tag) &&
                Vector3.Angle(self.position, target.position) <= settings.maxViewingAngle;

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
            public string tag;

            public void Verify()
            {
                if (!Utils.IsStringValid(tag))
                    tag = "Untagged";
            }
        }
    }
}