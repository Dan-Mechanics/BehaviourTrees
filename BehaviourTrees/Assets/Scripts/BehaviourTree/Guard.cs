using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class Guard : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent = default;

        private void Start()
        {
            List<object> tasks = new List<object>();
            Blackboard blackboard = new Blackboard();

            foreach (object task in tasks)
            {
                if (task is IBlackboardAssignable assignable)
                    assignable.Assign(blackboard);
            }

            // I dont get it.
        }
    }
}
