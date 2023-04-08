using System;

namespace Services
{
    public class InputService : IDisposable
    {
        private Controls _controls;

        public bool IsAim => _controls != null && _controls.Main.Aim.IsPressed();
        public float AxisXDelta => _controls != null ? _controls.Main.AxisX.ReadValue<float>() : 0f;
        public float AxisYDelta => _controls != null ? _controls.Main.AxisY.ReadValue<float>() : 0f;

        public InputService()
        {
            _controls = new Controls();
            _controls.Enable();
        }

        public void Dispose()
        {
            _controls.Enable();
        }
    }
}