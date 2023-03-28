using Common;
using System;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace FPS
{
    public class HeadBobbing : MonoBehaviour
    {
        [SerializeField] private float _shift = 0.05f;
        [SerializeField] private float _speed;
        private IDisposable _updateObservable;
        private GameManager _gameManager;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _defaultPosition;
        private bool _isBobbing;
        
        public bool IsBobbing
        {
            get => _isBobbing;
            set
            {
                _isBobbing = value;
                if (!value) transform.localPosition = _defaultPosition;
            } 
        }

        [Inject]
        private void Constructor(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _updateObservable = Observable
                .EveryUpdate()
                .Subscribe(_ => OnUpdate());

            _defaultPosition = transform.localPosition;
        }

        private void OnDestroy()
        {
            _updateObservable?.Dispose();
        }

        private float GetRandomValue()
        {
            var r = Random.value;
            return r > 0.5f ? 1f : -1f;
        }

        private void OnUpdate()
        {
            if (!IsBobbing) return;
            
            var x = _defaultPosition.x + GetRandomValue() * _shift;
            var y = _defaultPosition.y + GetRandomValue() * _shift;
            var z = _defaultPosition.z;
            
            var targetPosition = new Vector3(x, y, z);
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, 
                targetPosition, ref _velocity, _gameManager.CurrentWeaponConfig.SightShiftSpeed);
        }
    }
}