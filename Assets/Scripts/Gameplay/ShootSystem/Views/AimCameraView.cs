﻿using Configs;
using Gameplay.ShootSystem.Presenters;
using Gameplay.ShootSystem.Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.ShootSystem.Views
{
    public class AimCameraView : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private Vector3 _aimingPosition;
        private AimCameraPresenter _aimCameraPresenter;
        private SignalBus _signalBus;
        private Transform _cameraTransform;
        private PlayerConfig _playerConfig;

        [Inject]
        private void Constructor(
            SignalBus signalBus,
            PlayerConfig playerConfig,
            AimCameraPresenter aimCameraPresenter)
        {
            _signalBus = signalBus;
            _playerConfig = playerConfig;
            _aimCameraPresenter = aimCameraPresenter;
        }

        private void Awake()
        {
            _signalBus.Subscribe<ShootSignals.UpdateAimCameraPosition>(OnUpdateCameraPosition);
            _signalBus.Subscribe<ShootSignals.UpdateAimCameraFieldOfView>(OnUpdateCameraFieldOfView);
        }

        private void Start()
        {
            _cameraTransform = _playerCamera.transform;
            var originalPosition = _cameraTransform.localPosition;
            _aimCameraPresenter.SetCameraOriginalPosition(originalPosition);
            _aimCameraPresenter.SetCameraAimingPosition(_aimingPosition);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ShootSignals.UpdateAimCameraPosition>(OnUpdateCameraPosition);
            _signalBus.Unsubscribe<ShootSignals.UpdateAimCameraFieldOfView>(OnUpdateCameraFieldOfView);
        }

        private void OnUpdateCameraPosition(ShootSignals.UpdateAimCameraPosition signal)
        {
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, 
                signal.Position, _playerConfig.AimingSpeed * Time.deltaTime);
        }

        private void OnUpdateCameraFieldOfView(ShootSignals.UpdateAimCameraFieldOfView signal)
        {
            _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, 
                signal.FieldOfView, _playerConfig.ViewFieldShiftSpeed * Time.deltaTime);
        }
    }
}