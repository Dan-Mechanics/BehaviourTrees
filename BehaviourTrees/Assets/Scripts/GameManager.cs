using UnityEngine;

namespace BehaviourTrees
{
    public class GameManager : MonoBehaviour
    {
        private FirstPersonLook firstPersonLook;
        private PlayerMovement playerMovement;
        private SceneBoilerplate sceneBoilerplate;
        private ILookInput lookInput;
        private IMoveInput moveInput;

        private void Awake()
        {
            firstPersonLook = FindAnyObjectByType<FirstPersonLook>();
            playerMovement = FindAnyObjectByType<PlayerMovement>();
            moveInput = FindAnyObjectByType<PlayerMovement>();
            lookInput = FindAnyObjectByType<SensitivityMouse>();
            sceneBoilerplate = FindAnyObjectByType<SceneBoilerplate>();
        }

        private void Start()
        {
            // THIS NEEDS TO GO FIRST.
            sceneBoilerplate.Setup();

            firstPersonLook.SetupCursor();
            firstPersonLook.SetLookInput(lookInput);
            playerMovement.SetMoveInput(moveInput);
        }
    }
}
