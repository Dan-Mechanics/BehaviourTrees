using UnityEngine;

namespace BehaviourTrees
{
    public class Attack : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }
        private readonly string targetKey;
        private readonly string damageKey;

        public Attack(string damageKey, string targetKey)
        {
            this.targetKey = targetKey;
            this.damageKey = damageKey;
        }

        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;

        public Status Process(ref string name)
        {
            Transform target = Blackboard.GetValue<Transform>(targetKey);
            if (!target)
                return Status.Failure;

            if (!target.TryGetComponent(out IDamagable damagable))
                return Status.Failure;

            damagable.Damage(Blackboard.GetValue<float>(damageKey));
            return Status.Success;
        }
    }
}