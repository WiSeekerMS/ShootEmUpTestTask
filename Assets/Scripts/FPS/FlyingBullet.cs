using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class FlyingBullet : MonoBehaviour
    {
        [SerializeField]
        private List<TriggerHandler> _triggerHandlers;
        private Coroutine _moveToCor;
        private Coroutine _timerCor;

        public void Init(Action<Collider> triggerAction)
        {
            _triggerHandlers.ForEach(h => h.EnterAction = triggerAction);
        }

        public void MoveTo(float speed, Vector3 targetPosition)
        {
            LookAt(targetPosition);
            
            if (_moveToCor != null) StopCoroutine(_moveToCor);
            _moveToCor = StartCoroutine(MoveToCor(speed, targetPosition, ReachedTarget));
        }
    
        private IEnumerator MoveToCor(float speed, Vector3 targetPosition, Action action = null)
        {
            while (transform.position != targetPosition)
            {
                var delta = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
                yield return null;
            }

            action?.Invoke();
            _moveToCor = null;
        }

        private void ReachedTarget()
        {
            if(_timerCor != null) StopCoroutine(_timerCor);
            _timerCor = StartCoroutine(TimerCor(() => Destroy(gameObject)));
        }

        private IEnumerator TimerCor(Action action)
        {
            var t = 0f;
            while (t < 0.5f)
            {
                t += Time.deltaTime;
                yield return null;
            }
            
            action?.Invoke();
            _timerCor = null;
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