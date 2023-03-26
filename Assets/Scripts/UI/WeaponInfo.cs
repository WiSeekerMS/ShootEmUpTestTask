using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameTMP;
        [SerializeField] private Image _icon;
        [SerializeField] private Toggle _toggle;
    }
}