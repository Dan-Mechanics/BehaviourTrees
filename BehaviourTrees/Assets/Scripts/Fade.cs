using UnityEngine;

namespace BehaviourTrees
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private float depleteRate = default;
        private float alpha = default;

        private void FixedUpdate()
        {
            alpha -= Time.fixedDeltaTime * depleteRate;
            alpha = Mathf.Clamp01(alpha);
            canvasGroup.alpha = alpha;
        }

        public void Flash(float amount) => alpha += amount;
        private void OnValidate() => canvasGroup = GetComponent<CanvasGroup>();
    }
}
