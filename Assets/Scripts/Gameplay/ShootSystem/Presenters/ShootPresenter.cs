using System;
using Common;
using Common.Audio;
using Common.InputSystem.Signals;
using FPS.Factories;
using Gameplay.ShootSystem.Configs;
using Target;
using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace FPS.Presenters
{
    public class ShootPresenter : MonoBehaviour
    {
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private Transform _bulletParent;
        [SerializeField] private AudioClip _shotAudioClip;

        private IDisposable _fixedUpdateObservable;
        private FlyingBullet _bulletPrefab;
        private BulletFactory _bulletFactory;
        private AudioController _audioController;
        private MouseLookPresenter _mouseLookPresenter;
        private AimCameraPresenter _aimCameraPresenter;
        private GameManager _gameManager;
        private GameUIController _gameUIController;
        private WeaponConfig _weaponConfig;
        private RaycastHit _hitInfo;
        private SignalBus _signalBus;
        
        private int _bulletAmount;
        private bool _isBulletRelease;
        private bool _isInit;
        private bool _isHit;
 
        public Vector3 MuzzleWorldPosition => _muzzleTransform.position;

        [Inject]
        private void Constructor(
            SignalBus signalBus,
            AudioController audioController,
            MouseLookPresenter mouseLookPresenter,
            AimCameraPresenter aimCameraPresenter,
            GameManager gameManager, 
            GameUIController gameUIController,
            BulletFactory bulletFactory)
        {
            _signalBus = signalBus;
            _audioController = audioController;
            _mouseLookPresenter = mouseLookPresenter;
            _aimCameraPresenter = aimCameraPresenter;
            _gameManager = gameManager;
            _gameUIController = gameUIController;
            _bulletFactory = bulletFactory;

            Prepare();
        }

        private void Prepare()
        {
            _signalBus.Subscribe<InputSignals.Shot>(OnReleaseBullet);
            _signalBus.Subscribe<InputSignals.Reload>(OnReload);
            
            _fixedUpdateObservable = Observable
                .EveryFixedUpdate()
                .Subscribe(_ => OnFixedUpdate());
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<InputSignals.Reload>(OnReload);
            _fixedUpdateObservable?.Dispose();
        }

        public void Init(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
            _bulletPrefab = weaponConfig.BulletPrefab;
            _bulletAmount = weaponConfig.BulletAmount;

            UnlockPlayerControl();
        }

        public void ResetParams()
        {
            OnReload();
        }

        private void OnReleaseBullet()
        {
            if (!_isInit
                //|| !_isHit
                //|| _isBulletRelease
                || _bulletAmount <= 0)
            {
                return;
            }

            //_isBulletRelease = true;
            _bulletAmount--;
            
            /*var bullet = CreateBullet();
            bullet.Init(OnHitTarget);
            bullet.MoveTo(_weaponConfig.BulletSpeed, _hitInfo.point);*/

            _audioController.PlayClip(_shotAudioClip);
            _gameUIController.HideLastBullet();
            //_revolverDrum.HideBullet();

            if (!_isHit)
            {
                return;
            }
            
            var block = _hitInfo.transform.GetComponent<IBuildingBlock>();
            if (block != null && !block.Equals(null))
            {
                OnHitTarget(block.Points);
            }
        }

        private void OnReload()
        {
            if (_bulletAmount < _weaponConfig.BulletAmount)
            {
                _gameUIController.ShowAllBullets();
                _bulletAmount = _weaponConfig.BulletAmount;
            }
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
            _mouseLookPresenter.Disable();
            _aimCameraPresenter.Disable();
        }
        
        public void UnlockPlayerControl()
        {
            _isInit = true;
            _mouseLookPresenter.Enable();
            _aimCameraPresenter.Enable();
        }

        private FlyingBullet CreateBullet()
        {
            return _bulletFactory.Create(_bulletPrefab, 
                _muzzleTransform.position, _bulletParent);
        }
    }
}