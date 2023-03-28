﻿using Common;
using Configs;
using Factories;
using System;
using UI;
using UniRx;
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
        [SerializeField] private Transform _bulletParent;
        [SerializeField] private float _bulletSpeed = 50f;
        private IDisposable _updateObservable;
        private IDisposable _fixedUpdateObservable;
        private FlyingBullet _bulletPrefab;
        private BulletFactory _bulletFactory;
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
            GameUIController gameUIController,
            BulletFactory bulletFactory)
        {
            _gameManager = gameManager;
            _gameUIController = gameUIController;
            _bulletFactory = bulletFactory;
        }

        private void Awake()
        {
            _updateObservable = Observable
                .EveryUpdate()
                .Subscribe(_ => OnUpdate());

            _fixedUpdateObservable = Observable
                .EveryFixedUpdate()
                .Subscribe(_ => OnFixedUpdate());
        }

        private void OnDestroy()
        {
            _updateObservable?.Dispose();
            _fixedUpdateObservable?.Dispose();
        }

        private void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _isInit && _isHit)
            {
                if (--_bulletAmount >= 0)
                {
                    _gameUIController.HideLastBullet();

                    var bullet = CreateBullet();
                    bullet.Init(OnHitTarget);
                    bullet.MoveTo(_bulletSpeed, _hitInfo.point);
                }
            }
        }

        private void OnFixedUpdate()
        {
            if (!_isInit) return;
            var ray = _fpsCamera.ScreenPointToRay(Input.mousePosition);
            _isHit = Physics.Raycast(ray, out _hitInfo, Mathf.Infinity);

            if (!_isHit) return;
            var forward = _fpsCamera.transform.TransformDirection(Vector3.forward) * _hitInfo.distance;
            Debug.DrawRay(ray.origin, forward, Color.red);
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

        private FlyingBullet CreateBullet()
        {
            return _bulletFactory.Create(_bulletPrefab, 
                _muzzleTransform.position, _bulletParent);
        }

        private void OnHitTarget(float points)
        {
            _gameManager.OnHitTarget(points);
        }
    }
}