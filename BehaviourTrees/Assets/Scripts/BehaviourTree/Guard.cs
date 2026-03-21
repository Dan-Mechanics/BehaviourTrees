using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace BehaviourTrees
{
    public class Guard : MonoBehaviour, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }
        public const string PLAYER = "player";
        public const string PICKUP = "pickup";
        public const string WEAPON_GRAPHIC = "weapon_graphic";
        public const string HAS_WEAPON = "has_weapon";
        public const string DAMAGE = "damage";

        [SerializeField] private NavMeshAgent agent = default;
        [SerializeField] private TMP_Text billboardText = default;
        [SerializeField] private GameObject weaponGraphic = default;
        [SerializeField] private Vector3 weaponSwingRotation = default;

        [Space(25)]
        [SerializeField] private SenseSettings senseSettings = default;
        [SerializeField] private MoveSettings movement = default;
        [SerializeField] private MoveSettings combatMovement = default;
        [SerializeField] private float baseDamage = default;
        [SerializeField] private float swordDamage = default;

        [Space(25)]
        [SerializeField] private Transform player = default;
        [SerializeField] private List<Transform> waypoints = default;
        private INode root;

        public void Setup()
        {
            if (Blackboard == null)
                throw new System.Exception();
            
            Blackboard.SetValue(WEAPON_GRAPHIC, weaponGraphic);
            Blackboard.SetValue(PLAYER, player);
            Blackboard.SetValue(DAMAGE, baseDamage);

            // ================

            Sequence patrol = new Sequence();
            waypoints.ForEach(x => patrol.Add(new MoveToFixed(x.position, agent, movement)));
            patrol.DisallowReset();

            Sequence getWeapon = new Sequence(
                new Sense<ICollectable>(senseSettings, transform, PICKUP),
                new MoveTo(PICKUP, agent, combatMovement),
                new Equip(HAS_WEAPON, PICKUP, DAMAGE, swordDamage));

            Sequence chase = new Sequence(
                new Sense<IDamagable>(senseSettings, transform, PLAYER),
                new Optional(new Conditional(HAS_WEAPON, getWeapon)),
                new MoveTo(PLAYER, agent, combatMovement),
                new ShakeAnimation(WEAPON_GRAPHIC, Vector3.zero, weaponSwingRotation),
                new Attack(DAMAGE, PLAYER),
                new Invert(new Condition(Player.GET_ALIVE)));

            Selector selector = new Selector(chase, patrol);
            selector.AssignBlackboard(Blackboard);
            selector.Reset();

            root = selector;
        }

        private void FixedUpdate()
        {
            string currentNode = nameof(root);
            root.Process(ref currentNode);
            DisplayText(currentNode);

            weaponGraphic.SetActive(Blackboard.GetValue<bool>(HAS_WEAPON));
        }

        public void DisplayText(string str) => billboardText.text = str;
        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;
        private void OnDrawGizmos() => ShowSettings(senseSettings);

        private void ShowSettings(SenseSettings settings)
        {
            Gizmos.color = settings.color;
            Gizmos.DrawSphere(transform.position, settings.maxRange);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, settings.maxRange);
        }
    }
}
