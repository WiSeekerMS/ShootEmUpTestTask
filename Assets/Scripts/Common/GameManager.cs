using FPS;
using Target;
using UI;
using UnityEngine;
using Zenject;

namespace Common
{
    public class GameManager : MonoBehaviour
    {
        private SettingsPanel _settingsPanel;
        private GameUIController _gameUIController;
        private TargetCreator _targetCreator;
        private FPSController _fpsController;
        
        [Inject]
        private void Constructor(
            SettingsPanel settingsPanel,
            GameUIController gameUIController,
            TargetCreator targetCreator, 
            FPSController fpsController)
        {
            _settingsPanel = settingsPanel;
            _gameUIController = gameUIController;
            _targetCreator = targetCreator;
            _fpsController = fpsController;
            
            AfterInit();
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var isVisible = _settingsPanel.IsVisible;
                _settingsPanel.IsVisible = !isVisible;
            }
        }*/

        private void AfterInit()
        {
            _settingsPanel
                .StartButton
                .onClick
                .AddListener(OnClickStartButton);
        }

        private void OnDestroy()
        {
            if (_settingsPanel != null)
            {
                _settingsPanel
                    .StartButton
                    .onClick
                    .RemoveListener(OnClickStartButton);
            }
        }

        private void OnClickStartButton()
        {
            var pointsInfo = _settingsPanel.CollectPointsInformation();
            _targetCreator.Prepare(pointsInfo);
            _fpsController.Init();
            
            _settingsPanel.IsVisible = false;
        }

        public void OnHitTarget(IBuildingBlock buildingBlock)
        {
            _gameUIController.UpdateScore(buildingBlock.Points);
        }
    }
}