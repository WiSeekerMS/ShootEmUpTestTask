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
        private bool _isAim;

        private void Start()
        {
            _cameraTransform = _playerCamera.transform;
            _originalPosition = _cameraTransform.localPosition;
        }

        private void Update()
        {
            _isAim = Input.GetMouseButton(1);
        }

        private void FixedUpdate()
        {
            var cameraPosition = _isAim ? _aimingPosition : _originalPosition;
            var value = _isAim ? 40f : 60f;
            
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, 
                cameraPosition, _smoothSpeed * Time.deltaTime);
            
            _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, value, 5.5f * Time.deltaTime);
        }
    }
}