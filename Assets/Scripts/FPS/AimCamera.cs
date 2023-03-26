using UnityEngine;

namespace FPS
{
    public class AimCamera : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private Vector3 _aimingPosition;
        [SerializeField] private float _smoothSpeed;
        private Transform _cameraTransform;
        private Vector3 _originalPosition;
        private bool _isInit;
        private bool _isAim;

        public void Init()
        {
            _cameraTransform = _playerCamera.transform;
            _originalPosition = _cameraTransform.localPosition;
            _isInit = true;
        }

        private void Update()
        {
            if (!_isInit) return;
            _isAim = Input.GetMouseButton(1);
        }

        private void FixedUpdate()
        {
            if (!_isInit) return;
            var cameraPosition = _isAim ? _aimingPosition : _originalPosition;
            var value = _isAim ? 40f : 60f;
            
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, 
                cameraPosition, _smoothSpeed * Time.deltaTime);
            
            _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, value, 5.5f * Time.deltaTime);
        }
    }
}