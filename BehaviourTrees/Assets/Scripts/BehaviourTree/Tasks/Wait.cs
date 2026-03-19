using UnityEngine;

namespace BehaviourTrees
{
    public class Wait : INode, IResettable
    {
        private readonly float delay;
        private bool ticking;
        private float doneTime;

        public Wait(float delay)
        {
            this.delay = delay;
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            if (!ticking)
            {
                doneTime = Time.time + delay;
                ticking = true;
                return Status.Running;
            }

            if (ticking && Time.time >= doneTime)
            {
                Reset();
                return Status.Success;
            }

            return Status.Running;
        }

        public void Reset()
        {
            ticking = false;
            doneTime = 0f;
        }
    }
}