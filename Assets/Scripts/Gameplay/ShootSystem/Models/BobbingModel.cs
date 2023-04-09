using UnityEngine;

namespace Gameplay.ShootSystem.Models
{
    public class BobbingModel
    {
        public bool IsBobbing { get; set; }
        public Vector3 DefaultPosition { get; set; }
        public float BobbingDeltaShift { get; set; }
        public float SightShiftSpeed { get; set; }
    }
}