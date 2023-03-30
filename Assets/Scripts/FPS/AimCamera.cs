using Configs;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class AimCamera : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private WeaponBobbing _weaponBobbing;
        [SerializeField] private Vector3 _aimingPosition;
        private IDisposable _updateObservable;
        private PlayerConfig _playerConfig;
        private Controls _controls;
        private Transform _cameraTransform;
        private Vector3 _originalPosition;
        private bool _isInit;
        private bool _isAim;

        public bool IsBlockControl
        {
            get => _isInit;
            set => _isInit = !value;
        }

        [Inject]
        private void Constructor(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
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
            _cameraTransform = _playerCamera.transform;
            _originalPosition = _cameraTransform.localPosition;
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
            _isAim = _controls.Main.Aim.IsPressed();
            _weaponBobbing.IsBobbing = _isAim;
            
            var cameraPosition = _isAim ? _aimingPosition : _originalPosition;
            var value = _isAim ? _playerConfig.FieldOfViewAiming : _playerConfig.FieldOfView;
            
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, 
                cameraPosition, _playerConfig.AimingSpeed * Time.deltaTime);

            _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, 
                value, _playerConfig.ViewFieldShiftSpeed * Time.deltaTime);
        }
    }
}