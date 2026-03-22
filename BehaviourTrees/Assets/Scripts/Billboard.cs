using UnityEngine;

namespace BehaviourTrees
{
    public class Billboard : MonoBehaviour
    {
        private Transform cam;

        private void Awake() => cam = GameObject.FindWithTag("MainCamera").transform;
        private void LateUpdate() => transform.LookAt(cam.position, Vector3.up);
    }
}