using UnityEngine;

namespace BehaviourTrees
{
    public class SensitivityMouse : MonoBehaviour, ILookInput
    {
        [SerializeField] private EasyBinding next = default;
        [SerializeField] private EasyBinding previous = default;
        [SerializeField] private float sensitivity = default;
        [SerializeField] private float min = default;
        [SerializeField] private float max = default;
        [SerializeField] private float sensitivityPerClick = default;

        private void Update()
        {
            if (next.WasPressed)
                sensitivity = Mathf.Clamp(sensitivity + sensitivityPerClick, min, max);

            if (previous.WasPressed)
                sensitivity = Mathf.Clamp(sensitivity + sensitivityPerClick, min, max);
        }

        public Vector2 GetLook()
        {
            Vector2 look = new Vector2(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
            return look * sensitivity;
        }
    }
}
