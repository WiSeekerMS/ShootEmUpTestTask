using DG.Tweening;
using System;
using System.Collections.Generic;
using Common;
using Gameplay.Target;
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

        public void MoveTo(float speed, Vector3 targetPosition)
        {
            var duration = Vector3.Distance(transform.position, targetPosition) * speed;
            transform.LookAt(targetPosition);

            transform
                .DOMove(targetPosition, duration)
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
    }
}