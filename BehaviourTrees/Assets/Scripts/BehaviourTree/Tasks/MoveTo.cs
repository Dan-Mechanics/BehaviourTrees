using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class MoveTo : INode, IBlackboardRequired
    {
        private readonly string targetName;
        private readonly NavMeshAgent agent;
        private readonly MoveSettings settings;

        public MoveTo(string targetName, NavMeshAgent agent, MoveSettings settings)
        {
            this.targetName = targetName;
            this.agent = agent;
            this.settings = settings;
        }

        public Blackboard Blackboard { get; set; }
        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;

        public Status Process(ref string name)
        {
            name = targetName;
            Transform target = Blackboard.GetValue<Transform>(targetName);
            if (target == null)
                return Status.Failed;
            
            agent.speed = settings.speed;
            agent.SetDestination(target.position);

            float dist = Vector3.Distance(agent.transform.position, target.position);
            if (dist <= settings.minDistance)
                return Status.Success;

            return Status.Running;
        }
    }
}