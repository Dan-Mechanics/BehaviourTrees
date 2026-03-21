using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class MoveToFixed : INode
    {
        private readonly Vector3 position;
        private readonly NavMeshAgent agent;
        private readonly MoveSettings moveSettings;

        public MoveToFixed(Vector3 position, NavMeshAgent agent, MoveSettings moveSettings)
        {
            this.position = position;
            this.agent = agent;
            this.moveSettings = moveSettings;
        }

        public Status Process(ref string name)
        {
            name = $"move to {position}";
            agent.speed = moveSettings.speed;
            agent.SetDestination(position);

            float dist = Vector3.Distance(agent.transform.position, position);
            if (dist <= moveSettings.minDistance)
                return Status.Success;

            return Status.Running;
        }
    }
}