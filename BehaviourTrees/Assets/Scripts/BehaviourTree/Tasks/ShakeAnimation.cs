using UnityEngine;

namespace BehaviourTrees
{
    public class ShakeAnimation : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }
        
        private readonly string inputKey;
        private readonly Vector3 eulerA;
        private readonly Vector3 eulerB;

        public ShakeAnimation(string inputKey, Vector3 eulerA, Vector3 eulerB)
        {
            this.inputKey = inputKey;
            this.eulerA = eulerA;
            this.eulerB = eulerB;
        }

        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;

        public Status Process(ref string name)
        {
            name = GetType().Name;
            Transform graphic = Blackboard.GetValue<Transform>(inputKey);
            if (graphic == null)
                return Status.Running;

            graphic.localEulerAngles = Random.value > 0.5f ? eulerA : eulerB;

            // THE ANIMATION SHOULDN'T LEAD THE LOGIC.
            return Status.Running;
        }
    }
}