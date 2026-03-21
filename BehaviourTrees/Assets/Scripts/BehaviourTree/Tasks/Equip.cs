using UnityEngine;

namespace BehaviourTrees
{
    public class Equip : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }

        private readonly string pickupKey;
        private readonly string hasWeaponKey;
        private readonly string damageKey;
        private readonly float newDamage;

        public Equip(string pickupKey, string hasWeaponKey, string damageKey, float newDamage)
        {
            this.pickupKey = pickupKey;
            this.hasWeaponKey = hasWeaponKey;
            this.damageKey = damageKey;
            this.newDamage = newDamage;
        }

        public Status Process(ref string name)
        {
            // WE ALREADY HAVE THE SWORD.
            if (Blackboard.GetValue<bool>(hasWeaponKey))
                return Status.Success;

            Debug.Log(nameof(Equip));
            Blackboard.SetValue(hasWeaponKey, true);
            Blackboard.SetValue(damageKey, newDamage);
            Transform pickup = Blackboard.GetValue<Transform>(pickupKey);
            if (pickup && pickup.TryGetComponent(out ICollectable collectable))
                collectable.Collect();

            return Status.Success;
        }

        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;
    }
}