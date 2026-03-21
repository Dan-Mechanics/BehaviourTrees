using TMPro;
using UnityEngine;
using UnityEngine.AI;

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
        [SerializeField] private SenseSettings senseSettings = default;
        [SerializeField] private SenseSettings senseWeaponSettings = default;
        [SerializeField] private MoveSettings movement = default;
        [SerializeField] private MoveSettings combatMovement = default;
        [SerializeField] private float baseDamage = default;
        [SerializeField] private float armedDamage = default;
        [SerializeField] private Transform waypointsParent = default;

        [Header("Debug")]
        [SerializeField] private Transform rangeGraphic = default;
        [SerializeField] private float debugGraphicHeight = default;
        private INode root;

        public void Setup()
        {
            Blackboard.SetValue(WEAPON_GRAPHIC, weaponGraphic.transform);
            Blackboard.SetValue(DAMAGE, baseDamage);
            rangeGraphic.localScale = new Vector3(senseSettings.maxRange, debugGraphicHeight * 0.5f, senseSettings.maxRange) * 2f;

            // ===

            Sequence walkOnPath = new Sequence();
            for (int i = 0; i < waypointsParent.childCount; i++)
            {
                walkOnPath.Add(new MoveToFixed(waypointsParent.GetChild(i).position, agent, movement));
            }

            // FOR ME, PATROL MEANS WALKING ON A PATH WHILE ALSO LOOKING FOR PLAYERS.
            Selector patrol = new Selector(new Sense<IDamagable>(senseSettings, transform, PLAYER), new Perpetual(walkOnPath));

            // ===

            Sequence getWeapon = new Sequence(
                new Sense<ICollectable>(senseWeaponSettings, transform, PICKUP),
                new MoveTo(PICKUP, agent, combatMovement),
                new Equip(PICKUP, HAS_WEAPON, DAMAGE, armedDamage));

            Sequence chase = new Sequence(
                new Condition(Player.GET_ALIVE), // DOESN'T NEED TO BE HERE, BUT MAKES SENSE.
                new Optional(new Conditional(HAS_WEAPON, getWeapon, true)),
                new MoveTo(PLAYER, agent, combatMovement),
                new ShakeAnimation(WEAPON_GRAPHIC, Vector3.zero, weaponSwingRotation),
                new Attack(DAMAGE, PLAYER));

            // ===

            Selector selector = new Selector(new Invert(patrol), chase);
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
    }
}
