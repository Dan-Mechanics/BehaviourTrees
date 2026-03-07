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
        [SerializeField] private float minDistance = default;
        [SerializeField] private float patrolSpeed = default;
        [SerializeField] private bool check = default;
        [SerializeField] private List<Transform> waypoints = default;

        private bool GetCheck() => check;

        private Node behaviourTree;

        private void Start()
        {
            List<Node> nodes = new List<Node>();
            Blackboard blackboard = new Blackboard();

            Sequence patrol = new Sequence();
            waypoints.ForEach(x => patrol.Add(new MoveTo(x, agent, minDistance, patrolSpeed)));
            Conditional conditional = new Conditional(patrol, new Node(), GetCheck);


            /*foreach (object task in nodes)
            {
                if (task is IBlackboardRequired required)
                    required.Blackboard = blackboard;
            }*/

            behaviourTree = conditional;
        }

        private void FixedUpdate()
        {
            behaviourTree.Process();
        }
    }
}
