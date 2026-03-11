using UnityEngine;

namespace BehaviourTrees
{
    public class Attacking : INode
    {
        private readonly float attackTime;
        private readonly Transform weapon;
        private readonly float visualAngle;
        private bool ticking;
        private float doneTime;

        public Attacking(float attackTime, Transform weapon, float visualAngle)
        {
            this.visualAngle = visualAngle;
            this.attackTime = attackTime;
            this.weapon = weapon;
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            if (!ticking)
            {
                doneTime = Time.time + attackTime;
                ticking = true;
                return Status.Running;
            }

            if (ticking && Time.time >= doneTime)
            {
                ticking = false;
                return Status.Success;
            }

            weapon.localEulerAngles = Random.value > 0.5f ? Vector3.right * visualAngle : Vector3.zero;
            return Status.Running;
        }
    }
}