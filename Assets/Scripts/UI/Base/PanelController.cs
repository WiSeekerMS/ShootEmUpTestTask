using Unity.VisualScripting;
using UnityEngine;

namespace UI.Base
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private bool _isVisible = true;

        public bool IsVisible
        {
            get => _isVisible;
            set => SetVisibility(value);
        }

        private void SetVisibility(bool value)
        {
            _canvasGroup.alpha = value ? 1f : 0f;
            _canvasGroup.interactable = value;
            _isVisible = value;
        }
    }
}