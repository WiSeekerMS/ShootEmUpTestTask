using Gameplay.ShootSystem.Models;
using Gameplay.ShootSystem.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.ShootSystem.Presenters
{
    public class BobbingPresenter
    {
        private readonly BobbingModel _bobbingModel;
        private readonly SignalBus _signalBus;

        public BobbingPresenter(
            SignalBus signalBus,
            BobbingModel bobbingModel)
        {
            _signalBus = signalBus;
            _bobbingModel = bobbingModel;
        }

        public void SetDefaultPosition(Vector3 position)
        {
            _bobbingModel.DefaultPosition = position;
        }

        public void SetSightShiftSpeed(float speed)
        {
            _bobbingModel.SightShiftSpeed = speed;
        }

        public void BobbingDeltaShift(float delta)
        {
            _bobbingModel.BobbingDeltaShift = delta;
        }
        
        public void SetBobbingValue(bool value)
        {
            _bobbingModel.IsBobbing = value;
            if (!value) _signalBus.Fire<ShootSignals.ResetSwingPosition>();
        }

        private float GetRandomValue()
        {
            var r = Random.value;
            return r > 0.5f ? 1f : -1f;
        }

        public void OnUpdate()
        {
            if (!_bobbingModel.IsBobbing)
                return;
            
            var targetPosition = new Vector3
            {
                x = _bobbingModel.DefaultPosition.x + GetRandomValue() * _bobbingModel.BobbingDeltaShift,
                y = _bobbingModel.DefaultPosition.y + GetRandomValue() * _bobbingModel.BobbingDeltaShift,
                z = _bobbingModel.DefaultPosition.z
            };

            _signalBus.Fire(new ShootSignals.UpdateSwingPosition(_bobbingModel.SightShiftSpeed, targetPosition));
        }
    }
}