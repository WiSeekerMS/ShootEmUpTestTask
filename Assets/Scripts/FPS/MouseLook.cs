using Configs;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        private PlayerConfig _playerConfig;
        private float _xRotation;
        private float _yRotation;
        private bool _isInit;
        private Vector2 _clampAxisX;
        private Vector2 _clampAxisY;
    
        private const string AxisX = "Mouse X";
        private const string AxisY = "Mouse Y";

        public bool IsBlockControl
        {
            get => _isInit;
            set => _isInit = !value;
        }
        
        [Inject]
        private void Constructor(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
            _clampAxisX = _playerConfig.ClampAxisX;
            _clampAxisY = _playerConfig.ClampAxisY;
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
            var valueX = Input.GetAxis(AxisX) * Time.deltaTime * _playerConfig.MouseSensitivity;
            var valueY = Input.GetAxis(AxisY) * Time.deltaTime * _playerConfig.MouseSensitivity;
            
            _xRotation -= valueY;
            _xRotation = Mathf.Clamp(_xRotation, _clampAxisX.x, _clampAxisX.y);
        
            _yRotation += valueX;
            _yRotation = Mathf.Clamp(_yRotation, _clampAxisY.x, _clampAxisY.y);

            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
    }
}