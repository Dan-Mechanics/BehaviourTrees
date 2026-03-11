using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Player : MonoBehaviour, IDamagable
    {
        [SerializeField] private Fade fade = default;
        [SerializeField] private float dmgToFadeConversion = default;
        [SerializeField] private float health = default;

        public void Damage(float damage)
        {
            health -= damage;
            fade.Flash(damage * dmgToFadeConversion);
        }

        public bool GetAlive() => health > 0f;
    }
}
