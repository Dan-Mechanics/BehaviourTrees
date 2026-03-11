using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace BehaviourTrees
{
    public class Guard : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent = default;
        [SerializeField] private TMP_Text billboardText = default;
        [SerializeField] private GameObject weaponGraphic = default;
        [SerializeField] private float resetInterval = default;
        [SerializeField] private float attackTime = default;    
        [SerializeField] private bool hasWeapon = default;
        [SerializeField] private float maxHeldWeaponAngle = default;

        [Space(25)]
        [SerializeField] private Sense.Settings sensePlayerSettings = default;
        [SerializeField] private Sense.Settings senseWeaponSettings = default;
        [SerializeField] private MoveTo.Settings urgentMovement = default;
        [SerializeField] private MoveTo.Settings regularMovement = default;
        [SerializeField] private DealDamage.Settings damageSettings = default;

        [Space(25)]
        [SerializeField] private Player player = default;
        [SerializeField] private Transform weapon = default;
        [SerializeField] private List<Transform> waypoints = default;
        private INode root;

        public void Setup()
        {
            Sequence patrol = new Sequence();
            waypoints.ForEach(x => patrol.Add(new MoveTo(x, agent, regularMovement)));
            patrol.AllowExternalReset(false);

            Sequence getWeapon = new Sequence(
                new Sense(senseWeaponSettings, transform, weapon),
                new MoveTo(weapon, agent, urgentMovement),
                new Pickup(() => { hasWeapon = true; }, weapon.gameObject));

            Sequence chase = new Sequence(
                new Sense(sensePlayerSettings, transform, player.transform),
                new AlwaysSucceeds(new Conditional(() => { return !hasWeapon; }, getWeapon)),
                new MoveTo(player.transform, agent, urgentMovement),
                new Attacking(attackTime, weaponGraphic.transform, maxHeldWeaponAngle),
                new DealDamage(transform, damageSettings),
                new Invert(new Condition(player.GetAlive)));

            Selector selector = new Selector(chase, patrol);
            root = selector;

            InvokeRepeating(nameof(ResetTree), resetInterval, resetInterval);
        }

        private void FixedUpdate()
        {
            string currentNode = nameof(root);
            root.Process(ref currentNode);
            DisplayText(currentNode);

            weaponGraphic.SetActive(hasWeapon);
        }

        private void ResetTree() => root.Reset();
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
