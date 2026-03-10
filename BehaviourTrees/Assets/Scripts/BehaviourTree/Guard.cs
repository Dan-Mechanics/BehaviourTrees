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
        [SerializeField] private bool hasWeapon = default;

        [Space(25)]
        [SerializeField] private Sense.Settings sensePlayerSettings = default;
        [SerializeField] private Sense.Settings senseWeaponSettings = default;
        [SerializeField] private MoveTo.Settings urgentMovement = default;
        [SerializeField] private MoveTo.Settings regularMovement = default;

        [Space(25)]
        [SerializeField] private Transform player = default;
        [SerializeField] private Transform weapon = default;
        [SerializeField] private List<Transform> waypoints = default;

        private INode root;

        public void Setup()
        {
            Sequence patrol = new Sequence();
            waypoints.ForEach(x => patrol.Add(new MoveTo(x, agent, regularMovement)));

            Sequence gatherWeapon = new Sequence(
                new MoveTo(weapon, agent, urgentMovement),
                new Pickup(() => { hasWeapon = true; }, weapon.gameObject));

            Sequence attackPlayer = new Sequence(
                new MoveTo(player, agent, urgentMovement),
                new Wait(2f)); // attack here.

            Conditional lookForWeapon = new Conditional(new Sense(senseWeaponSettings, transform, weapon), attackPlayer, gatherWeapon);
            Conditional lookForPlayer = new Conditional(new Sense(sensePlayerSettings, transform, player), lookForWeapon, patrol);

            root = lookForPlayer;
        }

        private void FixedUpdate()
        {
            root.Process(out string currentNode);
            DisplayText(currentNode);
            weaponGraphic.SetActive(hasWeapon);
        }

        public void DisplayText(string str) => billboardText.text = str;
    }
}
