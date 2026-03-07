using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    /// <summary>
    /// https://medium.com/@lemapp09/beginning-game-development-behavior-trees-24f12c6b1e35
    /// This is the content script for the guard.
    /// </summary>
    public class Guard : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent = default;
        [SerializeField] private Transform[] waypoints = default;

        private Node behaviourTree;

        private void Start()
        {
            List<Node> nodes = new List<Node>();
            Blackboard blackboard = new Blackboard();
            var 

            var sequence = new Sequence()


            foreach (object task in nodes)
            {
                if (task is IBlackboardRequired required)
                    required.Assign(blackboard);
            }
        }

        private void FixedUpdate()
        {
            behaviourTree.Process();
        }
    }
}
