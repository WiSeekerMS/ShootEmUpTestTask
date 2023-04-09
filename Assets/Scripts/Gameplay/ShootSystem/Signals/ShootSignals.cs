using Gameplay.Target;
using UnityEngine;

namespace Gameplay.ShootSystem.Signals
{
    public static class ShootSignals
    {
        public sealed class HitTarget
        {
            public IBuildingBlock Block { get; }
            public HitTarget(IBuildingBlock block)
            {
                Block = block;
            }
        }
        
        public sealed class UpdateRotation
        {
            public Vector3 Euler { get; }
            public UpdateRotation(Vector3 euler)
            {
                Euler = euler;
            }
        }
        
        public sealed class UpdateAimCameraPosition
        {
            public Vector3 Position { get; }
            public UpdateAimCameraPosition(Vector3 position)
            {
                Position = position;
            }
        }

        public sealed class UpdateAimCameraFieldOfView
        {
            public float FieldOfView { get; }
            public UpdateAimCameraFieldOfView(float fieldOfView)
            {
                FieldOfView = fieldOfView;
            }
        }
        
        public sealed class UpdateSwingPosition
        {
            public float SightShiftSpeed { get; }
            public Vector3 TargetPosition { get; }
            public UpdateSwingPosition(float speed, Vector3 position)
            {
                SightShiftSpeed = speed;
                TargetPosition = position;
            }
        }
        
        public sealed class AimingStatus
        {
            public bool IsAiming { get; }

            public AimingStatus(bool isAiming)
            {
                IsAiming = isAiming;
            }
        }
        
        public sealed class ResetSwingPosition
        {
        }
    }
}