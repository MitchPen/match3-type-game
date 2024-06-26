using System;
using DG.Tweening;
using EasyButtons;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts
{
    public class CanvasResizer : MonoBehaviour
    {
        public event Action Risezed;
        public bool Resized => _resized;
        
        private bool _resized = false;
        private const float BaseCanvasHeight = 1280f;
        private const float BaseCanvasWidth = 720f;
        private const float BaseScreenRatio = 16 / 9f;

        private void Awake() => SetupCanvas();

        [Button] private void SetupCanvas()
        {
            GetComponent<CanvasScaler>().referenceResolution = new Vector2(
                BaseCanvasWidth,
                BaseCanvasHeight * Mathf.Max(1f, Screen.height / (float) Screen.width / BaseScreenRatio));

            transform.DOScale(0.01f, 0).OnComplete(() =>
            {
                _resized = true;
                Risezed?.Invoke();
            });
        }
    }
}
