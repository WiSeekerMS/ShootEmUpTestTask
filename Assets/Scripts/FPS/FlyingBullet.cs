using DG.Tweening;
using System;
using System.Collections.Generic;
using Target;
using UniRx;
using UnityEngine;

namespace FPS
{
    public class FlyingBullet : MonoBehaviour
    {
        [SerializeField]
        private List<TriggerHandler> _triggerHandlers;
        private Action<float> _hitTarget;
        private float _points;

        public void Init(Action<float> triggerAction)
        {
            _hitTarget = triggerAction;
            _triggerHandlers.ForEach(h => h.EnterAction = OnEnterTrigger);
        }

        private void OnEnterTrigger(Collider other)
        {
            var block = other.GetComponent<IBuildingBlock>();
            if (block == null || block.Equals(null)) return;
            _points += block.Points;
        }

        public void MoveTo(float time, Vector3 targetPosition)
        {
            LookAt(targetPosition);

            transform
                .DOMove(targetPosition, time)
                .SetEase(Ease.Linear)
                .OnComplete(OnReachedTarget);
        }

        private void OnReachedTarget()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ => OnTimerStopped())
                .AddTo(this);
        }

        private void OnTimerStopped()
        {
            _hitTarget?.Invoke(_points);
            Destroy(gameObject);
        }

        private Vector3 GetDirection(Vector3 start, Vector3 target)
        {
            var heading = target - start;
            var distance = heading.magnitude;
            return heading / distance;
        }
    
        private void LookAt(Vector3 target)
        {
            var direction = GetDirection(transform.position, target);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}