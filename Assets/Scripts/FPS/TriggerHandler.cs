using System;
using UnityEngine;

namespace FPS
{
    public class TriggerHandler : MonoBehaviour
    {
        public Action<Collider> EnterAction;

        private void OnTriggerEnter(Collider other)
        {
            EnterAction?.Invoke(other);
        }
    }
}