using UnityEngine;

namespace BehaviourTrees
{
    public class Wait : INode
    {
        private readonly float delay;
        private bool ticking;
        private float doneTime;

        public Wait(float delay)
        {
            this.delay = delay;
        }

        public Status Process(out string name)
        {
            name = GetType().ToString();
            if (!ticking)
            {
                doneTime = Time.time + delay;
                ticking = true;
                return Status.Standby;
            }

            if (ticking && Time.time >= doneTime)
            {
                ticking = false;
                return Status.Success;
            }

            return Status.Standby;
        }
    }
}