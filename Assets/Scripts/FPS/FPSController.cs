using Common;
using Configs;
using Factories;
using System;
using Common.Audio;
using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField] private MouseLook _mouseLook;
        [SerializeField] private AimCamera _aimCamera;
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private Transform _bulletParent;
        
        [SerializeField] private RevolverDrum _revolverDrum;
        [SerializeField] private AudioClip _shotAudioClip;

        private IDisposable _fixedUpdateObservable;
        private FlyingBullet _bulletPrefab;
        private BulletFactory _bulletFactory;
        private AudioController _audioController;
        private GameManager _gameManager;
        private GameUIController _gameUIController;
        private WeaponConfig _weaponConfig;
        private RaycastHit _hitInfo;
        private Controls _controls;
        
        private int _bulletAmount;
        private bool _isBulletRelease;
        private bool _isInit;
        private bool _isHit;

        public Vector3 MuzzleWorldPosition => _muzzleTransform.position;

        [Inject]
        private void Constructor(
            AudioController audioController,
            GameManager gameManager, 
            GameUIController gameUIController,
            BulletFactory bulletFactory)
        {
            _audioController = audioController;
            _gameManager = gameManager;
            _gameUIController = gameUIController;
            _bulletFactory = bulletFactory;
        }

        private void Awake()
        {
            _controls = new Controls();
            _controls.Main.Shot.performed += _ => OnReleaseBullet();
            
            _fixedUpdateObservable = Observable
                .EveryFixedUpdate()
                .Subscribe(_ => OnFixedUpdate());
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void OnDestroy()
        {
            _fixedUpdateObservable?.Dispose();
        }
        
        public void Init(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
            _bulletPrefab = weaponConfig.BulletPrefab;
            _bulletAmount = weaponConfig.BulletAmount;
            
            _mouseLook.Init();
            _aimCamera.Init();
            _controls.Enable();
            
            _isInit = true;
        }

        public void ResetParams()
        {
            _bulletAmount = _weaponConfig.BulletAmount;
            _revolverDrum.ShowAllBullets();
        }

        private void OnReleaseBullet()
        {
            if (!_isInit
                || !_isHit
                || _isBulletRelease
                || _bulletAmount <= 0)
            {
                return;
            }

            _isBulletRelease = true;
            _bulletAmount--;
            
            var bullet = CreateBullet();
            bullet.Init(OnHitTarget);
            bullet.MoveTo(_weaponConfig.BulletSpeed, _hitInfo.point);

            _audioController.PlayClip(_shotAudioClip);
            _gameUIController.HideLastBullet();
            _revolverDrum.HideBullet();
        }

        private void OnFixedUpdate()
        {
            if (!_isInit) return;
            var ray = new Ray(_muzzleTransform.position, _muzzleTransform.forward);
            _isHit = Physics.Raycast(ray, out _hitInfo, Mathf.Infinity);

            if (!_isHit) return;
            var forward = _muzzleTransform.forward * _hitInfo.distance;
            Debug.DrawRay(ray.origin, forward, Color.blue);
        }
        
        private void OnHitTarget(float points)
        {
            _gameManager.OnHitTarget(points);
            _isBulletRelease = false;
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

        private FlyingBullet CreateBullet()
        {
            return _bulletFactory.Create(_bulletPrefab, 
                _muzzleTransform.position, _bulletParent);
        }
    }
}