using Configs;
using System;
using Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform _bodyTransform;
        private IDisposable _updateObservable;
        private InputService _inputService;
        private PlayerConfig _playerConfig;
        private Vector2 _clampAxisX;
        private Vector2 _clampAxisY;
        private float _xRotation;
        private float _yRotation;
        private bool _isInit;
        
        public bool IsBlockControl
        {
            get => _isInit;
            set => _isInit = !value;
        }
        
        [Inject]
        private void Constructor(
            InputService inputService, 
            PlayerConfig playerConfig)
        {
            _inputService = inputService;
            
            _playerConfig = playerConfig;
            _clampAxisX = _playerConfig.ClampAxisX;
            _clampAxisY = _playerConfig.ClampAxisY;
        }

        private void Awake()
        {
            _updateObservable = Observable
                .EveryUpdate()
                .Subscribe(_ => OnUpdate());
        }
        
        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _isInit = true;
        }

        private void OnDestroy()
        {
            _updateObservable?.Dispose();
        }
        
        private void OnUpdate()
        {
            if (!_isInit) return;
            var valueX = _inputService.AxisXDelta * _playerConfig.MouseSensitivity * Time.deltaTime;
            var valueY = _inputService.AxisYDelta * _playerConfig.MouseSensitivity * Time.deltaTime;
            
            _xRotation -= valueY;
            _xRotation = Mathf.Clamp(_xRotation, _clampAxisX.x, _clampAxisX.y);
        
            _yRotation += valueX;
            _yRotation = Mathf.Clamp(_yRotation, _clampAxisY.x, _clampAxisY.y);

            _bodyTransform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
    }
}