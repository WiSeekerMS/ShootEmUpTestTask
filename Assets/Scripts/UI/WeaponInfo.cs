using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameTMP;
        [SerializeField] private TextMeshProUGUI _bulletAmountTMP;
        [SerializeField] private TextMeshProUGUI _amountPerShotTMP;
        [SerializeField] private TextMeshProUGUI _shiftSpeedTMP;
        [SerializeField] private TextMeshProUGUI _scoringRatioTMP;
        [SerializeField] private Image _icon;
        [SerializeField] private Toggle _toggle;
        private WeaponConfig _config;

        public WeaponConfig Config => _config;
        public Toggle Toggle => _toggle;

        public bool IsOn
        {
            get => _toggle;
            set => _toggle.isOn = value;
        }

        public void Init(WeaponConfig config, bool isOn = false)
        {
            _config = config;
            _icon.sprite = config.Icon;
            _nameTMP.text = config.WeaponName;
            _bulletAmountTMP.text += $" {config.BulletAmount}";
            _amountPerShotTMP.text += $" {config.BulletAmountPerShot}";
            _shiftSpeedTMP.text += $" {config.SightShiftSpeed}";
            _scoringRatioTMP.text += $" {config.ScoringRatio}";
            IsOn = isOn;
        }
    }
}