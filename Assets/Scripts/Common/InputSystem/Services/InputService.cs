using System;
using Common.InputSystem.Signals;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.InputSystem.Services
{
    public class InputService : IInitializable, IDisposable
    {
        private readonly Controls _controls;
        private readonly SignalBus _signalBus;

        public bool IsAim => _controls != null && _controls.Main.Aim.IsPressed();
        public float AxisXDelta => _controls != null ? _controls.Main.AxisX.ReadValue<float>() : 0f;
        public float AxisYDelta => _controls != null ? _controls.Main.AxisY.ReadValue<float>() : 0f;
        public float ButtonAxisX => _controls != null ? _controls.Main.ButtonAxisX.ReadValue<float>() : 0f;
        public float ButtonAxisY => _controls != null ? _controls.Main.ButtonAxisY.ReadValue<float>() : 0f;

        public InputService(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _controls = new Controls();
        }
        
        public void Initialize()
        {
            _controls.Main.Quit.performed += QuitGame;
            _controls.Main.Shot.performed += OnPressedShotButton;
            _controls.Main.Reload.performed += OnPressedReloadButton;
            _controls.Enable();
        }

        public void Dispose()
        {
            _controls.Main.Quit.Disable();
            _controls.Main.Shot.Disable();
            _controls.Main.Reload.Disable();
            _controls.Dispose();
        }

        private void OnPressedShotButton(InputAction.CallbackContext context)
        {
            _signalBus.Fire<InputSignals.Shot>();
        }

        private void OnPressedReloadButton(InputAction.CallbackContext context)
        {
            _signalBus.Fire<InputSignals.Reload>();
        }
        
        private void QuitGame(InputAction.CallbackContext context)
        {
            Application.Quit();
        }
    }
}