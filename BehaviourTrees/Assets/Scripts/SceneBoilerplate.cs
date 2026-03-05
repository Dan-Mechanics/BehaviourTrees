using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BehaviourTrees
{
    public class SceneBoilerplate : MonoBehaviour
    {
        [SerializeField] private EasyBinding reload = default;
        [SerializeField] private EasyBinding escape = default;
        [SerializeField] private int fps = default;
        [SerializeField] private float physicsTicksPerSecond = default;

        public void Setup()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Application.targetFrameRate = fps;
            QualitySettings.vSyncCount = 0;
            Time.fixedDeltaTime = 1f / physicsTicksPerSecond;
        }

        private void Update()
        {
            if (reload.WasPressed)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            if (escape.WasPressed)
                Application.Quit();
        }
    }
}