using UnityEngine;

namespace BehaviourTrees
{
    public class GameManager : MonoBehaviour
    {
        private Blackboard blackboard;
        private FirstPersonLook firstPersonLook;
        private PlayerMovement playerMovement;
        private SceneBoilerplate sceneBoilerplate;
        private SensitivityMouse sensitivityMouse;
        private Player player;
        private Guard guard;

        private void Awake()
        {
            blackboard = new Blackboard();
            guard = FindAnyObjectByType<Guard>();
            player = FindAnyObjectByType<Player>();
            firstPersonLook = FindAnyObjectByType<FirstPersonLook>();
            playerMovement = FindAnyObjectByType<PlayerMovement>();
            sensitivityMouse = FindAnyObjectByType<SensitivityMouse>();
            sceneBoilerplate = FindAnyObjectByType<SceneBoilerplate>();
        }

        private void Start()
        {
            sceneBoilerplate.Setup();
            guard.AssignBlackboard(blackboard);
            player.AssignBlackboard(blackboard);
            guard.Setup();

            firstPersonLook.SetupCursor();
            firstPersonLook.SetLookInput(sensitivityMouse);
            playerMovement.SetMoveInput(playerMovement);
        }
    }
}
