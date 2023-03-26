using System.Collections.Generic;
using System.Linq;
using Configs;
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
        private HorizontalLayoutGroup _pointsLayoutGroup;
        private MainConfig _mainConfig;

        public Button StartButton => _startButton;

        [Inject]
        private void Constructor(MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
        }

        private void Awake()
        {
            _addPointsButton.onClick.AddListener(OnClickAddPointsButton);
        }

        private void Start()
        {
            _pointsContainer = _targetPointsSR.content;
            _pointsLayoutGroup = _pointsContainer.GetComponent<HorizontalLayoutGroup>();
        }

        private void OnDestroy()
        {
            _addPointsButton.onClick.RemoveAllListeners();
        }

        private void OnClickAddPointsButton()
        {
            var spacing = _pointsLayoutGroup != null 
                ? _pointsLayoutGroup.spacing 
                : 0f;
            
            var prefab = _mainConfig.PointsInfoPrefab;
            var pointInfo = Instantiate(prefab, _pointsContainer);

            var pointsInfoTransform = pointInfo.transform as RectTransform;
            pointsInfoTransform.SetSiblingIndex(1);
            
            var offsetMax = _pointsContainer.offsetMax;
            _pointsContainer.offsetMax = new Vector2
            {
                x = offsetMax.x + pointsInfoTransform.sizeDelta.x + spacing, 
                y = offsetMax.y
            };
            
            pointInfo.Init();
        }
        
        public List<PointsInfo> CollectPointsInformation()
        {
            return _pointsContainer
                .GetComponentsInChildren<PointsInfo>()
                .ToList();
        }
    }
}