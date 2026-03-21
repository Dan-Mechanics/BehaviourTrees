using UnityEngine;

namespace BehaviourTrees
{
    public class Wait : INode, IResettable
    {
        private readonly float delay;
        private float timer;

        public Wait(float delay)
        {
            this.delay = delay;
        }

        public Status Process(ref string name)
        {
            name = GetType().Name;
            if (timer >= delay)
            {
                Reset();
                return Status.Success;
            }
            else
            {
                timer += Time.fixedDeltaTime;
                return Status.Running;
            }
        }

        public void Reset() => timer = 0f;
    }
}