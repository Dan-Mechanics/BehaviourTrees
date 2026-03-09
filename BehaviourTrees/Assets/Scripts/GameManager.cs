using UnityEngine;

namespace BehaviourTrees
{
    public class GameManager : MonoBehaviour
    {
        private FirstPersonLook firstPersonLook;
        private PlayerMovement playerMovement;
        private SceneBoilerplate sceneBoilerplate;
        private SensitivityMouse sensitivityMouse;

        private void Awake()
        {
            firstPersonLook = FindAnyObjectByType<FirstPersonLook>();
            playerMovement = FindAnyObjectByType<PlayerMovement>();
            sensitivityMouse = FindAnyObjectByType<SensitivityMouse>();
            sceneBoilerplate = FindAnyObjectByType<SceneBoilerplate>();
        }

        private void Start()
        {
            // THIS NEEDS TO GO FIRST.
            sceneBoilerplate.Setup();

            firstPersonLook.SetupCursor();
            firstPersonLook.SetLookInput(sensitivityMouse);
            playerMovement.SetMoveInput(playerMovement);
        }
    }
}
