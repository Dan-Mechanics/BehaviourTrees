using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class MoveToPosition : INode
    {
        private NavMeshAgent agent;
        private float distance;
        private Vector3 target;

        public MoveToPosition(NavMeshAgent agent, float distance, Vector3 target)
        {
            this.agent = agent;
            this.distance = distance;
            this.target = target;
        }

        public Status Process()
        {
            agent.SetDestination(target);
            if (Vector3.Distance(agent.transform.position, target) <= distance)
                return Status.Success;

            return Status.Running;
        }
    }
}