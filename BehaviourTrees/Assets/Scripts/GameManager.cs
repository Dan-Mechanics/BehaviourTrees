using UnityEngine;

namespace BehaviourTrees
{
    public class GameManager : MonoBehaviour
    {
        private FirstPersonLook firstPersonLook;
        private PlayerMovement playerMovement;
        private SceneBoilerplate sceneBoilerplate;
        private SensitivityMouse sensitivityMouse;
        private Guard guard;

        private void Awake()
        {
            guard = FindAnyObjectByType<Guard>();
            firstPersonLook = FindAnyObjectByType<FirstPersonLook>();
            playerMovement = FindAnyObjectByType<PlayerMovement>();
            sensitivityMouse = FindAnyObjectByType<SensitivityMouse>();
            sceneBoilerplate = FindAnyObjectByType<SceneBoilerplate>();
        }

        private void Start()
        {
            sceneBoilerplate.Setup();
            guard.Setup();

            firstPersonLook.SetupCursor();
            firstPersonLook.SetLookInput(sensitivityMouse);
            playerMovement.SetMoveInput(playerMovement);
        }
    }
}
