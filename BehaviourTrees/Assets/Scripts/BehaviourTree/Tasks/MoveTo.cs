using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class MoveTo : INode
    {
        private readonly Transform target;
        private readonly NavMeshAgent agent;
        private readonly Settings settings;

        public MoveTo(Transform target, NavMeshAgent agent, Settings settings)
        {
            this.target = target;
            this.agent = agent;
            this.settings = settings;
        }

        public Status Process()
        {
            agent.speed = settings.speed;
            agent.SetDestination(target.position);

            float dist = Vector3.Distance(agent.transform.position, target.position);
            if (dist <= settings.minDistance)
                return Status.Success;

            ServiceLocator<IDebugService>.Locate().DisplayText(GetName());
            return Status.Running;
        }

        public string GetName() => target.name;

        [System.Serializable]
        public struct Settings
        {
            public float speed;
            public float minDistance;
        }
    }
}