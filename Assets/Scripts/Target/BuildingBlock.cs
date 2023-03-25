using TMPro;
using UnityEngine;

namespace Target
{
    public class BuildingBlock : MonoBehaviour, IBuildingBlock
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private TextMeshPro _pointsTMP;
        
        public Vector3 Size { get; set; }
        public Vector3 LocalPosition { get; set; }
        public Material BlockMaterial { get; set; }
        public Color SetColor
        {
            set => _renderer.material.color = value;
        }
        
        public string SetPoints
        {
            set => _pointsTMP.text = value;
        }

        public void Init()
        {
            transform.localPosition = LocalPosition;
            transform.localScale = Size;
            //BlockMaterial.color = Random.ColorHSV();
            _renderer.material = BlockMaterial;
        }
    }
}