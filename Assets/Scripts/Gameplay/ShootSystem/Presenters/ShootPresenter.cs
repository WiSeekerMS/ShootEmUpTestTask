using System;
using Common.Audio;
using Common.InputSystem.Signals;
using Gameplay.ShootSystem.Configs;
using Gameplay.ShootSystem.Models;
using Gameplay.ShootSystem.Signals;
using Target;
using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.ShootSystem.Presenters
{
    public class ShootPresenter : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly ShootModel _shootModel;
        private readonly MouseLookPresenter _mouseLookPresenter;
        private readonly AimCameraPresenter _aimCameraPresenter;
        private readonly GameUIController _gameUIController;
        private readonly  AudioController _audioController;
        private IDisposable _fixedUpdateObservable;

        public Vector3 MuzzleWorldPosition => _shootModel.MuzzlePosition;

        public ShootPresenter(
            SignalBus signalBus,
            ShootModel shootModel, 
            MouseLookPresenter mouseLookPresenter,
            AimCameraPresenter aimCameraPresenter,
            GameUIController gameUIController,
            AudioController audioController)
        {
            _signalBus = signalBus;
            _shootModel = shootModel;
            _mouseLookPresenter = mouseLookPresenter;
            _aimCameraPresenter = aimCameraPresenter;
            _gameUIController = gameUIController;
            _audioController = audioController;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<InputSignals.Shot>(OnReleaseBullet);
            _signalBus.Subscribe<InputSignals.Reload>(OnReload);
        }

        public void Enable()
        {
            _fixedUpdateObservable = Observable
                .EveryFixedUpdate()
                .Subscribe(_ => OnFixedUpdate());
        }

        public void Disable()
        {
            _fixedUpdateObservable?.Dispose();
        }

        public void Dispose()
        {
            _fixedUpdateObservable?.Dispose();
            _signalBus.Unsubscribe<InputSignals.Shot>(OnReleaseBullet);
            _signalBus.Unsubscribe<InputSignals.Reload>(OnReload);
        }

        public void Prepare(WeaponConfig config)
        {
            _shootModel.WeaponConfig = config;
            _shootModel.BulletAmount = config.BulletAmount;
            _shootModel.BulletPrefab = config.BulletPrefab;
        }

        public void SetMuzzleTransform(Transform transform)
        {
            _shootModel.MuzzleTransform = transform;
        }
        
        public void BlockPlayerControl()
        {
            Disable();
            _mouseLookPresenter.Disable();
            _aimCameraPresenter.Disable();
        }
        
        public void UnlockPlayerControl()
        {
            Enable();
            _mouseLookPresenter.Enable();
            _aimCameraPresenter.Enable();
        }
        
        public void ResetParams()
        {
            OnReload();
        }

        private void OnFixedUpdate()
        {
            var ray = new Ray(_shootModel.MuzzlePosition, _shootModel.MuzzleForward);
            _shootModel.IsHit = Physics.Raycast(ray, out var hitInfo, Mathf.Infinity);

            if (!_shootModel.IsHit) return;
            _shootModel.HitInfo = hitInfo;
            
            var forward = _shootModel.MuzzleForward * hitInfo.distance;
            Debug.DrawRay(ray.origin, forward, Color.blue);
        }
        
        /*private FlyingBullet CreateBullet()
        {
            return _bulletFactory.Create(_bulletPrefab, 
                _muzzleTransform.position, _bulletParent);
        }*/
        
        private void OnReleaseBullet()
        {
            if (_shootModel.BulletAmount <= 0)
            {
                return;
            }

            _shootModel.BulletAmount--;
            _gameUIController.HideLastBullet();

            var clip = _shootModel.WeaponConfig.ShotAudioClip;
            _audioController.PlayClip(clip);

            if (!_shootModel.IsHit)
            {
                return;
            }
            
            var block = _shootModel.HitInfo.transform.GetComponent<IBuildingBlock>();
            if (block != null && !block.Equals(null))
            {
                _signalBus.Fire(new ShootSignals.HitTarget(block.Points));
            }
        }

        private void OnReload()
        {
            if (_shootModel.BulletAmount <_shootModel.WeaponConfig.BulletAmount)
            {
                _gameUIController.ShowAllBullets();
                _shootModel.BulletAmount = _shootModel.WeaponConfig.BulletAmount;
            }
        }
    }
}