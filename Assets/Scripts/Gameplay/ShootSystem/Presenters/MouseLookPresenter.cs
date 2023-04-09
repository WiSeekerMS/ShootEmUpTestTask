using Common.InputSystem.Services;
using Configs;
using Gameplay.ShootSystem.Models;
using Gameplay.ShootSystem.Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.ShootSystem.Presenters
{
    public class MouseLookPresenter
    {
        private readonly InputService _inputService;
        private readonly PlayerConfig _playerConfig;
        private readonly MouseLookModel _mouseLookModel;
        private readonly SignalBus _signalBus;

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
        }

        public void Disable()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void OnUpdate()
        {
            var valueX = _inputService.AxisXDelta * _playerConfig.MouseSensitivity * Time.deltaTime;
            var valueY = _inputService.AxisYDelta * _playerConfig.MouseSensitivity * Time.deltaTime;
            var euler = _mouseLookModel.GetRotation(valueX, valueY);
            _signalBus.Fire(new ShootSignals.UpdateRotation(euler));
        }
    }
}