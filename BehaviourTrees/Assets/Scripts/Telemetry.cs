using TMPro;
using UnityEngine;

namespace ApplyYourself
{
    public class Telemetry : MonoBehaviour
    {
        [SerializeField] private TMP_Text text = default;

        private void Update() => text.text = $"fps: {Mathf.RoundToInt(1f / Time.smoothDeltaTime)}";
    }
}
