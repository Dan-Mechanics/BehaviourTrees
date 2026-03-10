using UnityEngine;

namespace BehaviourTrees
{
    public class Pickup : INode
    {
        public delegate void PickupCallback();

        private readonly PickupCallback pickup;
        private readonly GameObject target;

        public Pickup(PickupCallback pickup, GameObject target)
        {
            this.pickup = pickup;
            this.target = target;
        }

        public Status Process()
        {
            Object.Destroy(target);
            pickup();

            return Status.Success;
        }
    }
}