using System;
using UnityEngine;

namespace Common
{
    public class TriggerHandler : MonoBehaviour
    {
        private bool _isEntered;
        public Action<Collider> EnterAction;

        private void OnTriggerEnter(Collider other)
        {
            if (_isEntered) return;
            _isEntered = true;
            EnterAction?.Invoke(other);
        }
    }
}