using FPS;
using UnityEngine;

namespace Gameplay.ShootSystem.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _weaponName;
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private FlyingBullet _bulletPrefab;
        [SerializeField] private Vector3 _positionWhenAiming;
        [SerializeField] private int _bulletAmount;
        [SerializeField] private int _bulletAmountPerShot;
        [SerializeField] private float _sightShiftSpeed;
        [SerializeField] private float _scoringRatio;
        [SerializeField] private float _bulletSpeed;

        public Sprite Icon => _icon;
        public string WeaponName => _weaponName;
        public GameObject WeaponPrefab => _weaponPrefab;
        public FlyingBullet BulletPrefab => _bulletPrefab;
        public Vector3 PositionWhenAiming => _positionWhenAiming;
        public int BulletAmount => _bulletAmount;
        public int BulletAmountPerShot => _bulletAmountPerShot;
        public float SightShiftSpeed => _sightShiftSpeed;
        public float ScoringRatio => _scoringRatio;
        public float BulletSpeed => _bulletSpeed;
    }
}