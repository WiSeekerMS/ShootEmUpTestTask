using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _weaponName;
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private Vector3 _positionWhenAiming;
        [SerializeField] private int _bulletAmount;
        [SerializeField] private int _bulletAmountPerShot;
        [SerializeField] private float _sightShiftSpeed;
        [SerializeField] private float _scoringRatio;
    }
}