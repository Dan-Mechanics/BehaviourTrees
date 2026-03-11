using UnityEngine;

namespace BehaviourTrees
{
    public class DealDamage : INode
    {
        private readonly Transform self;
        private readonly Settings settings;

        public DealDamage(Transform self, Settings settings)
        {
            this.self = self;
            this.settings = settings;
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            Collider[] colliders = Physics.OverlapSphere(self.position, settings.radius, settings.mask, QueryTriggerInteraction.Ignore);
            foreach (Collider coll in colliders)
            {
                if (coll.TryGetComponent(out IDamagable damagable))
                    damagable.Damage(settings.damage);
            }

            return Status.Success;
        }

        [System.Serializable]
        public struct Settings
        {
            public float radius;
            public LayerMask mask;
            public float damage;
        }
    }
}