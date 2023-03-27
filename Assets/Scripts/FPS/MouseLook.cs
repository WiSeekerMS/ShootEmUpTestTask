using UnityEngine;

namespace FPS
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private float _mouseSensitivity;
        private float _xRotation;
        private float _yRotation;
        private bool _isInit;
    
        private const string AXIS_X = "Mouse X";
        private const string AXIS_Y = "Mouse Y";

        public bool IsBlockControl
        {
            get => _isInit;
            set => _isInit = !value;
        }

        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _isInit = true;
        }

        private void Update()
        {
            if (!_isInit) return;
            var valueX = Input.GetAxis(AXIS_X) * Time.deltaTime * _mouseSensitivity;
            var valueY = Input.GetAxis(AXIS_Y) * Time.deltaTime * _mouseSensitivity;

            _xRotation -= valueY;
            _xRotation = Mathf.Clamp(_xRotation, -15f, 15f);
        
            _yRotation += valueX;
            _yRotation = Mathf.Clamp(_yRotation, -35f, 35f);

            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
    }
}