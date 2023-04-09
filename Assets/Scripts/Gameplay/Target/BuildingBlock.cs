using TMPro;
using UnityEngine;

namespace Gameplay.Target
{
    public class BuildingBlock : MonoBehaviour, IBuildingBlock
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private TextMeshPro _pointsTMP;
        private int _points;
        
        public Vector3 Size { get; set; }
        public Vector3 LocalPosition { get; set; }
        public Material BlockMaterial { get; set; }

        public Color SetColor
        {
            set => _renderer.material.color = value;
        }

        public int Points
        {
            get => _points;
            set
            {
                _points = value;
                _pointsTMP.text = value.ToString();
            }
        }

        public void Init()
        {
            transform.localPosition = LocalPosition;
            transform.localScale = Size;
            _renderer.material = BlockMaterial;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ResetParams()
        {
            Show();
        }
    }
}