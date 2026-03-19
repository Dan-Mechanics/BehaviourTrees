using UnityEngine;

namespace BehaviourTrees
{
    public class Sense : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }

        private readonly Settings settings;
        private readonly Transform transform;
        private readonly string targetKey;

        public Sense(Settings settings, Transform transform, string targetKey)
        {
            this.settings = settings;
            this.targetKey = targetKey;
            this.transform = transform;
        }

        public void AssignBlackboard(Blackboard blackboard)
        {
            Blackboard = blackboard;
            Debug.Log("BLACboard assinged" + targetKey);
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            Transform target = Blackboard.GetValue<Transform>(targetKey);
            if (target == null)
                return Status.Failed;

            Vector3 dir = target.position - transform.position;
            bool hasFound = Physics.Raycast(transform.position, dir.normalized, out RaycastHit hit, settings.maxRange,
                settings.mask, QueryTriggerInteraction.Ignore) && hit.transform == target &&
                Vector3.Angle(dir, transform.forward) <= settings.maxViewingAngle;

           // bool hasFound = Vector3.Distance(target.position, self.position) < settings.maxRange;
            if (hasFound)
                return Status.Success;

            return Status.Failed;
        }

        [System.Serializable]
        public struct Settings
        {
            public float maxRange;
            public LayerMask mask;
            public float maxViewingAngle;
            public Color color;
        }
    }
}