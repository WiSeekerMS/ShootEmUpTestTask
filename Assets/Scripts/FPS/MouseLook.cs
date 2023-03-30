using Configs;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] 
        private Transform _bodyTransform;
        private IDisposable _updateObservable;
        private PlayerConfig _playerConfig;
        private float _xRotation;
        private float _yRotation;
        private bool _isInit;
        private Vector2 _clampAxisX;
        private Vector2 _clampAxisY;
        private Controls _controls;
        
        public bool IsBlockControl
        {
            get => _isInit;
            set => _isInit = !value;
        }
        
        [Inject]
        private void Constructor(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
            _clampAxisX = _playerConfig.ClampAxisX;
            _clampAxisY = _playerConfig.ClampAxisY;
        }

        private void Awake()
        {
            _controls = new Controls();
            _updateObservable = Observable
                .EveryUpdate()
                .Subscribe(_ => OnUpdate());
        }
        
        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _controls.Enable();
            _isInit = true;
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void OnDestroy()
        {
            _updateObservable?.Dispose();
        }
        
        private void OnUpdate()
        {
            if (!_isInit) return;
            var valueX = _controls.Main.AxisX.ReadValue<float>() * Time.deltaTime * _playerConfig.MouseSensitivity;
            var valueY = _controls.Main.AxisY.ReadValue<float>() * Time.deltaTime * _playerConfig.MouseSensitivity;
            
            _xRotation -= valueY;
            _xRotation = Mathf.Clamp(_xRotation, _clampAxisX.x, _clampAxisX.y);
        
            _yRotation += valueX;
            _yRotation = Mathf.Clamp(_yRotation, _clampAxisY.x, _clampAxisY.y);

            _bodyTransform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
    }
}