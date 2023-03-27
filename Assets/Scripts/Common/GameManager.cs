using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Configs;
using FPS;
using Target;
using UI;
using UnityEngine;
using Zenject;

namespace Common
{
    public class GameManager : MonoBehaviour
    {
        private List<LevelConfig> _levelConfigs;
        private MainConfig _mainConfig;
        private SettingsPanel _settingsPanel;
        private GameUIController _gameUIController;
        private TargetCreator _targetCreator;
        private FPSController _fpsController;
        private int _currentLevelIndex;
        private WeaponConfig _weaponConfig;
        private bool _isCheckComplete;
        private Coroutine _timerCor;

        private const float TargetYPosition = 10f;
        private const float TimeToMoveToNextLevel = 1.5f;
        
        [Inject]
        private void Constructor(
            MainConfig mainConfig,
            SettingsPanel settingsPanel,
            GameUIController gameUIController,
            TargetCreator targetCreator, 
            FPSController fpsController)
        {
            _mainConfig = mainConfig;
            _settingsPanel = settingsPanel;
            _gameUIController = gameUIController;
            _targetCreator = targetCreator;
            _fpsController = fpsController;
            
            AfterInit();
        }

        private void AfterInit()
        {
            _settingsPanel
                .StartButton
                .onClick
                .AddListener(OnClickStartButton);

            _levelConfigs = _mainConfig.LevelConfigs;
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
            _weaponConfig = _settingsPanel.GetCurrentWeaponConfig();
            _gameUIController.SetBulletAmount(_weaponConfig.BulletAmount);
            
            var pointsInfo = _settingsPanel.CollectPointsInformation();
            var muzzlePosition = _fpsController.MuzzleWorldPosition;
            
            _targetCreator.Prepare(pointsInfo);
            var levelInfo = _levelConfigs.FirstOrDefault(i => i.LevelIndex == _currentLevelIndex);

            if (levelInfo != null)
            {
                _gameUIController.SetLevelIndex = levelInfo.LevelIndex;
                var targetPosition = new Vector3(0f, TargetYPosition, muzzlePosition.z + levelInfo.DistanceToTarget);
                _targetCreator.CreateTarget(targetPosition);
            }
            
            _fpsController.Init(_weaponConfig);
            _settingsPanel.IsVisible = false;
        }

        private IEnumerator TimerCor(Action action)
        {
            var t = 0f;
            while (t < TimeToMoveToNextLevel)
            {
                t += Time.deltaTime;
                yield return null;
            }
            
            action?.Invoke();
            _timerCor = null;
        }

        public void OnHitTarget(IBuildingBlock buildingBlock)
        {
            if (_isCheckComplete) return;
            _isCheckComplete = true;

            var points = buildingBlock.Points * _weaponConfig.ScoringRatio;
            _gameUIController.UpdateScore(points);
            CheckLevelComplete();
        }

        private void CheckLevelComplete()
        {
            var levelInfo = _levelConfigs.FirstOrDefault(i => i.LevelIndex == _currentLevelIndex);
            if (levelInfo && levelInfo.PointsToComplete <= _gameUIController.CurrentScore)
            {
                _fpsController.BlockPlayerControl();
                if (++_currentLevelIndex < _levelConfigs.Count)
                {
                    if(_timerCor != null) StopCoroutine(_timerCor);
                    _timerCor = StartCoroutine(TimerCor(GoToNextLevel));
                }
                else
                {
                    _gameUIController.SetLevelText = "Game completed!";
                }
            }
            else
            {
                _isCheckComplete = false;
            }
        }

        private void GoToNextLevel()
        {
            _gameUIController.SetLevelIndex= _currentLevelIndex;
            _gameUIController.ShowAllBullets();
            _gameUIController.ResetScore();
            
            var levelInfo = _levelConfigs.FirstOrDefault(i => i.LevelIndex == _currentLevelIndex);
            if (levelInfo == null) return;
            
            var muzzlePosition = _fpsController.MuzzleWorldPosition;
            var targetPosition = new Vector3(0f, TargetYPosition, muzzlePosition.z + levelInfo.DistanceToTarget);
            _targetCreator.SetTargetPosition(targetPosition);
            
            _fpsController.ResetParams();
            _fpsController.UnlockPlayerControl();
            _isCheckComplete = false;
        }
    }
}