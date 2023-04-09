using UnityEngine;

namespace FPS.Signals
{
    public static class ShootSignals
    {
        public sealed class UpdateRotation
        {
            public Vector3 Euler { get; private set; }
            public UpdateRotation(Vector3 euler)
            {
                Euler = euler;
            }
        }
        
        public sealed class UpdateAimCameraPosition
        {
            public Vector3 Position { get; private set; }
            public UpdateAimCameraPosition(Vector3 position)
            {
                Position = position;
            }
        }

        public sealed class UpdateAimCameraFieldOfView
        {
            public float FieldOfView { get; private set; }
            public UpdateAimCameraFieldOfView(float fieldOfView)
            {
                FieldOfView = fieldOfView;
            }
        }
    }
}