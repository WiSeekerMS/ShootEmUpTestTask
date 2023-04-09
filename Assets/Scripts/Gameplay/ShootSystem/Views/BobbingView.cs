using Gameplay.ShootSystem.Presenters;
using Gameplay.ShootSystem.Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.ShootSystem.Views
{
    public class BobbingView : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private BobbingPresenter _bobbingPresenter;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _defaultPosition;

        private void Awake()
        {
            _defaultPosition = transform.localPosition;
            _bobbingPresenter.SetDefaultPosition(_defaultPosition);
            _signalBus.Subscribe<ShootSignals.UpdateSwingPosition>(OnUpdateSwingPosition);
            _signalBus.Subscribe<ShootSignals.ResetSwingPosition>(ResetPosition);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ShootSignals.UpdateSwingPosition>(OnUpdateSwingPosition);
            _signalBus.Unsubscribe<ShootSignals.ResetSwingPosition>(ResetPosition);
        }

        private void ResetPosition()
        {
            transform.localPosition = _defaultPosition;
        }

        private void OnUpdateSwingPosition(ShootSignals.UpdateSwingPosition signal)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, 
                signal.TargetPosition, ref _velocity, signal.SightShiftSpeed);
        }
    }
}