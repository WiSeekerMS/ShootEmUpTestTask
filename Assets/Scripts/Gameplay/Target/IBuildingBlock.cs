using UnityEngine;

namespace Gameplay.Target
{
    public interface IBuildingBlock
    {
        Vector3 Size { get; set; }
        Vector3 LocalPosition { get; set; }
        Color SetColor { set; }
        int Points { get; set; }
        void Init();
    }
}