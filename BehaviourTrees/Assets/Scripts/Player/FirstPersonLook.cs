using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class FirstPersonLook : MonoBehaviour
    {
        private const float MAX_CAM_ANGLE = 90f;

        [SerializeField] private Transform eyes = default;
        [SerializeField] private Vector2 rotation = default;
        private ILookInput lookInput;

        public void SetLookInput(ILookInput lookInput) => this.lookInput = lookInput;

        public void SetupCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            rotation += lookInput.GetLook();
            rotation.x = Mathf.Clamp(rotation.x, -MAX_CAM_ANGLE, MAX_CAM_ANGLE);

            eyes.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.right);
            transform.rotation = Quaternion.AngleAxis(rotation.y, Vector3.up);
        }
    }
}
