using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class MoveTo : Node, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }

        private Transform target;
        private NavMeshAgent agent;
        private float minDistance;
        private float speed;

        public MoveTo(NavMeshAgent agent, float minDistance, float speed)
        {
            this.agent = agent;
            this.minDistance = minDistance;
            this.speed = speed;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            target = Blackboard.GetValue<Transform>(nameof(target));
            agent.speed = speed;
        }

        public override Status Process()
        {
            agent.SetDestination(target.position);
            if (Vector3.Distance(agent.transform.position, target.position) <= minDistance)
                return Status.Success;

            return Status.Running;
        }
    }
}