using UnityEngine;

namespace BehaviourTrees
{
    public class GameManager : MonoBehaviour
    {
        private FirstPersonLook firstPersonLook;
        private PlayerMovement playerMovement;
        private SceneBoilerplate sceneBoilerplate;
        private SensitivityMouse sensitivityMouse;
        private Transform player;
        private Guard guard;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
            guard = FindAnyObjectByType<Guard>();
            firstPersonLook = FindAnyObjectByType<FirstPersonLook>();
            playerMovement = FindAnyObjectByType<PlayerMovement>();
            sensitivityMouse = FindAnyObjectByType<SensitivityMouse>();
            sceneBoilerplate = FindAnyObjectByType<SceneBoilerplate>();
        }

        private void Start()
        {
            sceneBoilerplate.Setup();
            ServiceLocator<IDebugService>.Provide(guard);

            guard.Setup();
            firstPersonLook.SetupCursor();
            firstPersonLook.SetLookInput(sensitivityMouse);
            playerMovement.SetMoveInput(playerMovement);
        }
    }
}
