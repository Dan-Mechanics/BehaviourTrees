using UnityEngine;

namespace BehaviourTrees
{
    public class Sword : MonoBehaviour, ICollectable
    {
        public void Collect() => Destroy(gameObject);
    }
}
