using UnityEngine;

namespace BehaviourTrees
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private Transform cam = default;

        private void Awake()
        {
            if (cam == null)
                cam = GameObject.FindWithTag("MainCamera").transform;
        }

        private void LateUpdate() => transform.LookAt(cam.position, Vector3.up);
    }
}