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

        [Header("Debug UI")]
        [SerializeField] private Color color = Color.white;
        [SerializeField] private int padding = default;
        [SerializeField] private int width = default;
        [SerializeField] private int height = default;

        private void Update()
        {
            if (next.WasPressed)
                sensitivity = Mathf.Clamp(sensitivity + sensitivityPerClick, min, max);

            if (previous.WasPressed)
                sensitivity = Mathf.Clamp(sensitivity - sensitivityPerClick, min, max);
        }

        public Vector2 GetLook()
        {
            Vector2 mouseInput = new Vector2(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
            return mouseInput * sensitivity;
        }

        private void OnGUI()
        {
            GUI.color = color;
            Rect rect = new Rect(Screen.width - width - padding, Screen.height - height - padding, width, height);
            GUIStyle style = GUI.skin.GetStyle("Label");
            style.fontSize = 20;

            style.alignment = TextAnchor.MiddleRight;
            GUI.Label(rect, $"sens: {sensitivity}".ToLowerInvariant(), style);
            GUI.color = Color.white;
        }
    }
}
