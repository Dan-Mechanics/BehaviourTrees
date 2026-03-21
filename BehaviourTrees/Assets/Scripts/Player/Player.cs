using UnityEngine;

namespace BehaviourTrees
{
    public class Player : MonoBehaviour, IDamagable, IBlackboardRequired
    {
        public Blackboard Blackboard { get; set; }
        public const string GET_ALIVE = "get_alive";

        [SerializeField] private GameObject diedText = default;
        [SerializeField] private Fade fade = default;
        [SerializeField] private float dmgToFadeConversion = default;
        [SerializeField] private float health = default;

        public void AssignBlackboard(Blackboard blackboard) => Blackboard = blackboard;

        public void Damage(float damage)
        {
            health -= damage;
            fade.Flash(damage * dmgToFadeConversion);
        }

        private void FixedUpdate()
        {
            bool alive = health > 0f;
            Blackboard.SetValue(GET_ALIVE, alive);
            diedText.SetActive(!alive);
            if (!alive)
                Destroy(this);
        }
    }
}
