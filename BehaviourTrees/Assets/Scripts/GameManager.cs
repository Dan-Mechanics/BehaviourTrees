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
        private Transform player;

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

            guard.Setup(player);
            firstPersonLook.SetupCursor();
            firstPersonLook.SetLookInput(sensitivityMouse);
            playerMovement.SetMoveInput(playerMovement);
        }
    }
}
