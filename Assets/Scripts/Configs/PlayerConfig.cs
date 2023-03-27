using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Camera")] 
        [SerializeField] private float _fieldOfView;
        [SerializeField] private float _fieldOfViewAiming;
        [SerializeField] private float _viewFieldShiftSpeed;
        [SerializeField] private float _aimingSpeed;

        [Header("Mouse Look")] 
        [SerializeField] private float _mouseSensitivity;
        [SerializeField] private Vector2 _clampAxisX;
        [SerializeField] private Vector2 _clampAxisY;

        public float FieldOfView => _fieldOfView;
        public float FieldOfViewAiming => _fieldOfViewAiming;
        public float ViewFieldShiftSpeed => _viewFieldShiftSpeed;
        public float AimingSpeed => _aimingSpeed;
        public float MouseSensitivity => _mouseSensitivity;
        public Vector2 ClampAxisX => _clampAxisX;
        public Vector2 ClampAxisY => _clampAxisY;
    }
}