using FPS.Signals;
using UnityEngine;
using Zenject;

namespace FPS.Views
{
    public class MouseLookView : MonoBehaviour
    {
        [SerializeField] private Transform _bodyTransform;
        [Inject] private SignalBus _signalBus;

        private void Awake()
        {
            _signalBus.Subscribe<ShootSignals.UpdateRotation>(OnUpdateRotation);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ShootSignals.UpdateRotation>(OnUpdateRotation);
        }

        private void OnUpdateRotation(ShootSignals.UpdateRotation signal)
        {
            _bodyTransform.localRotation = Quaternion.Euler(signal.Euler);
        }
    }
}