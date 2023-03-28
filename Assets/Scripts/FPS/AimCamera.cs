using Common;
using Configs;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class AimCamera : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private HeadBobbing _headBobbing;
        [SerializeField] private Vector3 _aimingPosition;
        private PlayerConfig _playerConfig;
        private GameManager _gameManager;
        private Transform _cameraTransform;
        private Vector3 _originalPosition;
        private bool _isInit;
        private bool _isAim;

        private const float RadiusRandomPointsOnCircle = 5f;
        
        public bool IsBlockControl
        {
            get => _isInit;
            set => _isInit = !value;
        }

        [Inject]
        private void Constructor(
            PlayerConfig playerConfig, 
            GameManager gameManager)
        {
            _playerConfig = playerConfig;
            _gameManager = gameManager;
        }

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
            _headBobbing.IsBobbing = _isAim;
        }

        private void FixedUpdate()
        {
            if (!_isInit) return;
            var cameraPosition = _isAim ? _aimingPosition : _originalPosition;
            var value = _isAim ? _playerConfig.FieldOfViewAiming : _playerConfig.FieldOfView;
            
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, 
                cameraPosition, _playerConfig.AimingSpeed * Time.deltaTime);

            /*if (_isAim)
            {
                var p = Random.insideUnitCircle * RadiusRandomPointsOnCircle;
                cameraPosition += new Vector3(p.x, p.y, 0f);
                
                _cameraTransform.localPosition = Vector3.MoveTowards(_cameraTransform.localPosition, 
                    cameraPosition, _gameManager.CurrentWeaponConfig.SightShiftSpeed * Time.deltaTime);
            }*/
   
            _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, 
                value, _playerConfig.ViewFieldShiftSpeed * Time.deltaTime);
        }
    }
}