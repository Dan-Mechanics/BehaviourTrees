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
            Blackboard.SetValue(hasWeaponKey, true);
            Blackboard.SetValue(damageKey, newDamage);
            Object.Destroy(Blackboard.GetValue<Transform>(pickupKey).gameObject);
            return Status.Success;
        }

        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;
    }
}