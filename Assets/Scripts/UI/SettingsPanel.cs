using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SettingsPanel : PanelController
    {
        [SerializeField] private ScrollRect _weaponsSR;
        [SerializeField] private ScrollRect _targetPointsSR;
        [SerializeField] private Button _addPointsButton;
        [SerializeField] private Button _startButton;
        private RectTransform _pointsContainer;

        [Inject]
        private void Init()
        {
        }

        private void Awake()
        {
            _addPointsButton.onClick.AddListener(OnClickAddPointsButton);
            _startButton.onClick.AddListener(OnClickStartButton);
        }

        private void Start()
        {
            _pointsContainer = _targetPointsSR.content;
        }

        private void OnDestroy()
        {
            _addPointsButton.onClick.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
        }

        private void OnClickAddPointsButton()
        {
        }

        private void OnClickStartButton()
        {
        }
    }
}