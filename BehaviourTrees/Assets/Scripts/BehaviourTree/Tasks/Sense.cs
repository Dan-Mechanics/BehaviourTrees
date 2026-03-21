using UnityEngine;

namespace BehaviourTrees
{
    public class Sense<T> : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }

        private readonly SenseSettings settings;
        private readonly Transform transform;
        private readonly string outputKey;

        public Sense(SenseSettings settings, Transform transform, string outputKey)
        {
            this.settings = settings;
            this.outputKey = outputKey;
            this.transform = transform;
        }

        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;

        public Status Process(ref string name)
        {
            name = GetType().Name;

            // THIS STEP IS REALLY IMPORTANT.
            Blackboard.SetValue<Transform>(outputKey, null);
            Collider[] colliders = Physics.OverlapSphere(transform.position, settings.maxRange, settings.mask, QueryTriggerInteraction.Ignore);
            foreach (Collider coll in colliders)
            {
                if (coll.GetComponent<T>() == null)
                    continue;

                Transform target = coll.transform;
                if (CheckLineOfSight(target))
                {
                    Blackboard.SetValue<Transform>(outputKey, target);
                    return Status.Success;
                }
            }

            return Status.Failed;
        }

        private bool CheckLineOfSight(Transform target)
        {
            Vector3 dir = target.position - transform.position;
            return Physics.Raycast(transform.position, dir.normalized, out RaycastHit hit, settings.maxRange,
                settings.mask, QueryTriggerInteraction.Ignore) && hit.transform == target &&
                Vector3.Angle(dir, transform.forward) <= settings.maxViewingAngle;
        }
    }
}