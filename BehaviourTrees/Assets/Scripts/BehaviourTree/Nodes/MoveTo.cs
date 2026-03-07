using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class MoveTo : Node
    {
        private Transform target;
        private NavMeshAgent agent;
        private float minDistance;
        private float speed;

        public MoveTo(Transform target, NavMeshAgent agent, float minDistance, float speed)
        {
            this.target = target;
            this.agent = agent;
            this.minDistance = minDistance;
            this.speed = speed;
        }

        public override Status Process()
        {
            agent.speed = speed;
            agent.SetDestination(target.position);
            float dist = Vector3.Distance(agent.transform.position, target.position);
            if (Vector3.Distance(agent.transform.position, target.position) <= minDistance)
                return Status.Success;

            return Status.Running;
        }
    }
}