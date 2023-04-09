using FPS;
using UnityEngine;

namespace Gameplay.ShootSystem.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _weaponName;
        [SerializeField] private FlyingBullet _bulletPrefab;
        [SerializeField] private int _bulletAmount;
        [SerializeField] private int _bulletAmountPerShot;
        [SerializeField] private float _sightShiftSpeed;
        [SerializeField] private float _bobbingDeltaShift;
        [SerializeField] private float _scoringRatio;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private AudioClip _shotAudioClip;

        public Sprite Icon => _icon;
        public string WeaponName => _weaponName;
        public FlyingBullet BulletPrefab => _bulletPrefab;
        public int BulletAmount => _bulletAmount;
        public int BulletAmountPerShot => _bulletAmountPerShot;
        public float SightShiftSpeed => _sightShiftSpeed;
        public float BobbingDeltaShift => _bobbingDeltaShift;
        public float ScoringRatio => _scoringRatio;
        public float BulletSpeed => _bulletSpeed;
        public AudioClip ShotAudioClip => _shotAudioClip;
    }
}