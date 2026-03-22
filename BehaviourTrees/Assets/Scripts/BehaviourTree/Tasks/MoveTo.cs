using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class MoveTo : INode, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }

        private readonly string inputKey;
        private readonly NavMeshAgent agent;
        private readonly MoveSettings settings;

        public MoveTo(string inputKey, NavMeshAgent agent, MoveSettings settings)
        {
            this.inputKey = inputKey;
            this.agent = agent;
            this.settings = settings;
        }

        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;

        public Status Process(ref string name)
        {
            name = inputKey;
            Transform target = Blackboard.GetValue<Transform>(inputKey);
            if (target == null)
                return Status.Failure;
            
            name = target.name;
            agent.speed = settings.speed;
            agent.SetDestination(target.position);

            float dist = Vector3.Distance(agent.transform.position, target.position);
            if (dist <= settings.minDistance)
                return Status.Success;

            return Status.Running;
        }
    }
}