using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private List<Transform> waypoints = default;
        [SerializeField] private float minDistance = default;
        [SerializeField] private float patrolSpeed = default;
        private Node behaviourTree;

        private void Start()
        {
            List<Node> nodes = new List<Node>();
            Blackboard blackboard = new Blackboard();

            Sequence patrol = new Sequence();
            waypoints.ForEach(x => patrol.Add(new MoveTo(x, agent, minDistance, patrolSpeed)));

            /*foreach (object task in nodes)
            {
                if (task is IBlackboardRequired required)
                    required.Blackboard = blackboard;
            }*/

            behaviourTree = patrol;
            behaviourTree.OnEnter();
        }

        private void FixedUpdate()
        {
            behaviourTree.Process();
        }
    }
}
