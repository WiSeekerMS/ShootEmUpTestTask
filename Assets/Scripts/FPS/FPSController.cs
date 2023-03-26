using UnityEngine;

namespace FPS
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField] private Camera _fpsCamera;
        [SerializeField] private MouseLook _mouseLook;
        [SerializeField] private AimCamera _aimCamera;
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private FlyingBullet _bulletPrefab;
        private RaycastHit _hitInfo;
        private bool _isInit;
        private bool _isHit;

        public void Init()
        {
            _mouseLook.Init();
            _aimCamera.Init();
            _isInit = true;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _isInit && _isHit)
            {
                var bullet = Instantiate(_bulletPrefab);
                bullet.transform.position = _muzzleTransform.position;
                bullet.MoveTo(35f, _hitInfo.point);
            }    
        }
        
        private void FixedUpdate()
        {
            if(!_isInit) return;
            var ray = _fpsCamera.ScreenPointToRay(Input.mousePosition);
            _isHit = Physics.Raycast(ray, out _hitInfo, Mathf.Infinity);
            
            if (!_isHit) return;
            var forward = _fpsCamera.transform.TransformDirection(Vector3.forward) * _hitInfo.distance;
            Debug.DrawRay(ray.origin, forward, Color.red);
        }
    }
}