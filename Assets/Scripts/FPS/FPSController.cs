using System;
using Common;
using Configs;
using Target;
using UI;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField] private Camera _fpsCamera;
        [SerializeField] private MouseLook _mouseLook;
        [SerializeField] private AimCamera _aimCamera;
        [SerializeField] private Transform _muzzleTransform;
        private FlyingBullet _bulletPrefab;
        private GameManager _gameManager;
        private GameUIController _gameUIController;
        private WeaponConfig _weaponConfig;
        private RaycastHit _hitInfo;
        private int _bulletAmount;
        private bool _isInit;
        private bool _isHit;

        public Vector3 MuzzleWorldPosition => _muzzleTransform.position;

        [Inject]
        private void Constructor(
            GameManager gameManager, 
            GameUIController gameUIController)
        {
            _gameManager = gameManager;
            _gameUIController = gameUIController;
        }

        public void Init(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
            _bulletPrefab = weaponConfig.BulletPrefab;
            _bulletAmount = weaponConfig.BulletAmount;
            
            _mouseLook.Init();
            _aimCamera.Init();
            _isInit = true;
        }

        public void ResetParams()
        {
            _bulletAmount = _weaponConfig.BulletAmount;
        }

        public void BlockPlayerControl()
        {
            _isInit = false;
            _mouseLook.IsBlockControl = true;
        }
        
        public void UnlockPlayerControl()
        {
            _isInit = true;
            _mouseLook.IsBlockControl = false;
        }

        private void CreateBullet()
        {
            _gameUIController.HideLastBullet();
            
            var bullet = Instantiate(_bulletPrefab);
            bullet.transform.position = _muzzleTransform.position;
            
            bullet.Init(OnHitTarget);
            bullet.MoveTo(50f, _hitInfo.point);
        }

        private void OnHitTarget(float points)
        {
            _gameManager.OnHitTarget(points);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _isInit && _isHit)
            {
                if (--_bulletAmount >= 0)
                {
                    CreateBullet(); 
                }
            }    
        }
        
        private void FixedUpdate()
        {
            if(!_isInit) return;
            var ray = _fpsCamera.ScreenPointToRay(Input.mousePosition);
            _isHit = Physics.Raycast(ray, out _hitInfo, Mathf.Infinity);
            
            if (!_isHit) return;
            var forward = _fpsCamera.transform.TransformDirection(Vector3.forward) * _hitInfo.distance;
            Debug.DrawRay(ray.origin, forward, Color.red);
        }
    }
}