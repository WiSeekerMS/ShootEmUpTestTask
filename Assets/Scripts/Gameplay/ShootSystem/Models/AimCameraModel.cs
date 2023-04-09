using Common.InputSystem.Services;
using UnityEngine;

namespace Gameplay.ShootSystem.Models
{
    public class AimCameraModel
    {
        private readonly InputService _inputService;
        public bool IsAim => _inputService.IsAim;
        public Vector3 OriginalPosition { get; set; }
        public Vector3 AimingPosition { get; set; }
        
        public AimCameraModel(InputService inputService)
        {
            _inputService = inputService;
        }
    }
}