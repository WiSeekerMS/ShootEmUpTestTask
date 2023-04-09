using System.Collections.Generic;
using Gameplay.ShootSystem.Configs;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Configs
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Configs/MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [SerializeField] private PointsInfo _pointsInfoPrefab;
        [SerializeField] private List<WeaponConfig> _weaponConfigs;
        [SerializeField] private WeaponInfo _weaponInfoPrefab;
        [SerializeField] private List<LevelConfig> _levelConfigs;
        [SerializeField] private Image _bulletIconPrefab;

        public PointsInfo PointsInfoPrefab => _pointsInfoPrefab;
        public List<WeaponConfig> WeaponConfigs => _weaponConfigs;
        public WeaponInfo WeaponInfoPrefab => _weaponInfoPrefab;
        public List<LevelConfig> LevelConfigs => _levelConfigs;
        public Image BulletIconPrefab => _bulletIconPrefab;
    }
}