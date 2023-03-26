using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class PointsInfo : MonoBehaviour
    {
        [SerializeField] private Image _colorPalette;
        [SerializeField] private TMP_InputField _pointsInputField;
        
        public Color GetColor => _colorPalette.color;

        public int GetPoints
        {
            get
            {
                int.TryParse(_pointsInputField.text, out var value);
                return value;
            }
        } 

        public void Init()
        {
            _colorPalette.color = Random.ColorHSV();
            _pointsInputField.text = "0";
            _pointsInputField.interactable = true;
        }
    }
}