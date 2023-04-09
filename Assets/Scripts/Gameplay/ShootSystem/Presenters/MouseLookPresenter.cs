using System;
using Common.InputSystem.Services;
using Configs;
using FPS.Models;
using FPS.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace FPS.Presenters
{
    public class MouseLookPresenter : IDisposable
    {
        private readonly InputService _inputService;
        private readonly PlayerConfig _playerConfig;
        private readonly MouseLookModel _mouseLookModel;
        private readonly SignalBus _signalBus;
        private IDisposable _updateObservable;

        public MouseLookPresenter(
            InputService inputService, 
            PlayerConfig playerConfig,
            MouseLookModel mouseLookModel,
            SignalBus signalBus)
        {
            _inputService = inputService;
            _playerConfig = playerConfig;
            _mouseLookModel = mouseLookModel;
            _signalBus = signalBus;
        }

        public void Enable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _updateObservable = Observable
                .EveryUpdate()
                .Subscribe(_ => OnUpdate());
        }

        public void Disable()
        {
            _updateObservable?.Dispose();
            
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        
        public void Dispose()
        {
            _updateObservable?.Dispose();
        }

        private void OnUpdate()
        {
            var valueX = _inputService.AxisXDelta * _playerConfig.MouseSensitivity * Time.deltaTime;
            var valueY = _inputService.AxisYDelta * _playerConfig.MouseSensitivity * Time.deltaTime;
            var euler = _mouseLookModel.GetRotation(valueX, valueY);
            _signalBus.Fire(new ShootSignals.UpdateRotation(euler));
        }
    }
}