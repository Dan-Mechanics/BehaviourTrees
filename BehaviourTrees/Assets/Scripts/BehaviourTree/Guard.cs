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

        /// <summary>
        /// Possibly assign this via gameamanger if you
        /// wanna make the context more context.
        /// </summary>
        [SerializeField] private Transform player = default;
        [SerializeField] private Transform weaponPickup = default;
        [SerializeField] private bool hasWeapon = default;
        [SerializeField] private SenseProfile sensePlayer = default;
        [SerializeField] private SenseProfile senseWeapon = default;
        [SerializeField] private MoveToProfile chaseProfile = default;
        [SerializeField] private MoveToProfile patrolProfile = default;
        [SerializeField] private List<Transform> waypoints = default;

        private INode behaviourTree;

        private void Start()
        {
            Blackboard blackboard = new Blackboard();

            Sequence patrol = new Sequence();
            waypoints.ForEach(x => patrol.Add(new MoveTo(x, agent, patrolProfile.minDistance, patrolProfile.speed)));

            MoveTo chase = new MoveTo(player, agent, chaseProfile.minDistance, chaseProfile.speed);
            Conditional seesPlayerConditional = new Conditional(SeesPlayer, chase, patrol);

            behaviourTree = seesPlayerConditional;
        }

        private bool SeesPlayer()
        {
            Vector3 dir = (player.position - transform.position).normalized;
            return Physics.Raycast(transform.position, dir, out RaycastHit hit, sensePlayer.range,
                sensePlayer.mask, QueryTriggerInteraction.Ignore) && hit.collider.CompareTag(player.tag);
        }

        private void FixedUpdate() => behaviourTree.Process();

        [System.Serializable]
        public struct SenseProfile 
        {
            public float range;
            public LayerMask mask;
        }

        [System.Serializable]
        public struct MoveToProfile
        {
            public float minDistance;
            public float speed;
        }
    }
}
