using UnityEngine;

namespace Target
{
    public interface IBuildingBlock
    {
        Vector3 Size { get; set; }
        Vector3 LocalPosition { get; set; }
        Color SetColor { set; }
        string SetPoints { set; }
        void Init();
    }
}