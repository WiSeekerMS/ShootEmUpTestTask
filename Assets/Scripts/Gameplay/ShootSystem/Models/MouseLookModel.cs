using Configs;
using UnityEngine;

namespace Gameplay.ShootSystem.Models
{
    public class MouseLookModel
    {
        private PlayerConfig _playerConfig;
        private Vector2 _clampAxisX;
        private Vector2 _clampAxisY;
        private float _xRotation;
        private float _yRotation;
        
        public MouseLookModel(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
            _clampAxisX = _playerConfig.ClampAxisX;
            _clampAxisY = _playerConfig.ClampAxisY;
        }

        public Vector3 GetRotation(float valueX, float valueY)
        {
            _xRotation -= valueY;
            _xRotation = Mathf.Clamp(_xRotation, _clampAxisX.x, _clampAxisX.y);
        
            _yRotation += valueX;
            _yRotation = Mathf.Clamp(_yRotation, _clampAxisY.x, _clampAxisY.y);

            return new Vector3(_xRotation, _yRotation, 0f);
        }
    }
}