using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace BehaviourTrees
{
    public class Guard : MonoBehaviour
    {
        public const string TARGET_NAME = "player";
        public const string PICKUP_NAME = "pickup";

        private readonly Blackboard blackboard = new Blackboard();

        [SerializeField] private NavMeshAgent agent = default;
        [SerializeField] private TMP_Text billboardText = default;
        [SerializeField] private GameObject weaponGraphic = default;
        [SerializeField] private float attackTime = default;    
        [SerializeField] private bool hasWeapon = default;
        [SerializeField] private float maxHeldWeaponAngle = default;

        [Space(25)]
        [SerializeField] private Sense.Settings sensePlayerSettings = default;
        [SerializeField] private Sense.Settings senseWeaponSettings = default;
        [SerializeField] private MoveSettings urgentMovement = default;
        [SerializeField] private MoveSettings regularMovement = default;
        [SerializeField] private DealDamage.Settings damageSettings = default;

        [Space(25)]
        [SerializeField] private Player player = default;
        [SerializeField] private Transform weapon = default;
        [SerializeField] private List<Transform> waypoints = default;
        private INode root;

        public void Setup()
        {
            Sequence patrol = new Sequence();
            waypoints.ForEach(x => patrol.Add(new MoveToFixed(x.position, agent, regularMovement)));
            patrol.DisallowReset();

            blackboard.SetValue(PICKUP_NAME, weapon);
            blackboard.SetValue(TARGET_NAME, player.transform);

            Sequence getWeapon = new Sequence(
                new Sense(senseWeaponSettings, transform, PICKUP_NAME),
                new MoveTo(PICKUP_NAME, agent, urgentMovement),
                new Pickup(() => { hasWeapon = true; }, weapon.gameObject));

            Sequence chase = new Sequence(
                new Sense(sensePlayerSettings, transform, TARGET_NAME),
                new Optional(new Conditional(() => { return !hasWeapon; }, getWeapon)),
                new MoveTo(TARGET_NAME, agent, urgentMovement),
                new Attacking(attackTime, weaponGraphic.transform, maxHeldWeaponAngle),
                new DealDamage(transform, damageSettings),
                new Invert(new Condition(player.GetAlive)));

            Selector selector = new Selector(chase, patrol);
            selector.AssignBlackboard(blackboard);
            selector.Reset();

            root = selector;
        }

        private void FixedUpdate()
        {
            string currentNode = nameof(root);
            root.Process(ref currentNode);
            DisplayText(currentNode);

            weaponGraphic.SetActive(hasWeapon);
        }

        public void DisplayText(string str) => billboardText.text = str;

        private void OnDrawGizmos()
        {
            ShowSettings(sensePlayerSettings);
            // ShowSettings(senseWeaponSettings);
        }

        private void ShowSettings(Sense.Settings settings)
        {
            Gizmos.color = settings.color;
            Gizmos.DrawSphere(transform.position, settings.maxRange);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, settings.maxRange);
        }
    }
}
