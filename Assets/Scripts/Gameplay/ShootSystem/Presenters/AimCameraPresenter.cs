using Configs;
using Gameplay.ShootSystem.Models;
using Gameplay.ShootSystem.Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.ShootSystem.Presenters
{
    public class AimCameraPresenter
    {
        private readonly AimCameraModel _aimCameraModel;
        private readonly PlayerConfig _playerConfig;
        private readonly SignalBus _signalBus;

        public AimCameraPresenter(
            SignalBus signalBus,
            AimCameraModel aimCameraModel, 
            PlayerConfig playerConfig)
        {
            _signalBus = signalBus;
            _aimCameraModel = aimCameraModel;
            _playerConfig = playerConfig;
        }

        public void SetCameraOriginalPosition(Vector3 position)
        {
            _aimCameraModel.OriginalPosition = position;
        }
        
        public void SetCameraAimingPosition(Vector3 position)
        {
            _aimCameraModel.AimingPosition = position;
        }

        public void OnUpdate()
        {
            var cameraPosition = _aimCameraModel.IsAim 
                ? _aimCameraModel.AimingPosition 
                : _aimCameraModel.OriginalPosition;
            
            _signalBus.Fire(new ShootSignals.UpdateAimCameraPosition(cameraPosition));
            
            var fieldOfView = _aimCameraModel.IsAim 
                ? _playerConfig.FieldOfViewAiming 
                : _playerConfig.FieldOfView;
            
            _signalBus.Fire(new ShootSignals.UpdateAimCameraFieldOfView(fieldOfView));
            _signalBus.Fire(new ShootSignals.AimingStatus(_aimCameraModel.IsAim));
        }
    }
}