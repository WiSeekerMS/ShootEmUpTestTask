﻿using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using FPS.Presenters;
using Gameplay.ShootSystem.Configs;
using Target;
using UI;
using UniRx;
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
        private ShootPresenter _shootPresenter;
        private int _currentLevelIndex;
        private WeaponConfig _weaponConfig;
        private bool _isCheckComplete;
        
        private const float TargetYPosition = 10f;
        private const float TimeToMoveToNextLevel = 1.5f;

        public WeaponConfig CurrentWeaponConfig => _weaponConfig;
        
        [Inject]
        private void Constructor(
            MainConfig mainConfig,
            SettingsPanel settingsPanel,
            GameUIController gameUIController,
            TargetCreator targetCreator, 
            ShootPresenter shootPresenter)
        {
            _mainConfig = mainConfig;
            _settingsPanel = settingsPanel;
            _gameUIController = gameUIController;
            _targetCreator = targetCreator;
            _shootPresenter = shootPresenter;
            
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
            var muzzlePosition = _shootPresenter.MuzzleWorldPosition;
            
            _targetCreator.Init(pointsInfo);
            var levelInfo = _levelConfigs.FirstOrDefault(i => i.LevelIndex == _currentLevelIndex);

            if (levelInfo != null)
            {
                _gameUIController.SetLevelIndex = levelInfo.LevelIndex;
                var targetPosition = new Vector3(0f, TargetYPosition, muzzlePosition.z + levelInfo.DistanceToTarget);
                _targetCreator.CreateTarget(targetPosition);
            }
            
            _shootPresenter.Init(_weaponConfig);
            _settingsPanel.IsVisible = false;
        }

        public void OnHitTarget(float points)
        {
            if (_isCheckComplete) return;
            _isCheckComplete = true;

            var value = points * _weaponConfig.ScoringRatio;
            _gameUIController.UpdateScore(value);
            CheckLevelComplete();
        }

        private void CheckLevelComplete()
        {
            var levelInfo = _levelConfigs.FirstOrDefault(i => i.LevelIndex == _currentLevelIndex);
            if (levelInfo && levelInfo.PointsToComplete <= _gameUIController.CurrentScore)
            {
                _shootPresenter.BlockPlayerControl();
                if (++_currentLevelIndex < _levelConfigs.Count)
                {
                    Observable
                        .Timer(TimeSpan.FromSeconds(TimeToMoveToNextLevel))
                        .Subscribe(_ => GoToNextLevel())
                        .AddTo(this);
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
            
            var muzzlePosition = _shootPresenter.MuzzleWorldPosition;
            var targetPosition = new Vector3(0f, TargetYPosition, muzzlePosition.z + levelInfo.DistanceToTarget);
            _targetCreator.SetTargetPosition(targetPosition);
            
            _shootPresenter.ResetParams();
            _shootPresenter.UnlockPlayerControl();

            _isCheckComplete = false;
        }
    }
}