using UnityEngine;

namespace DefaultNamespace
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private FlyingBullet _bulletPrefab;
        private bool _isHit;
        private RaycastHit _hitInfo;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _isHit)
            {
                
            }    
        }
        
        private void FixedUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _isHit = Physics.Raycast(ray, out _hitInfo, Mathf.Infinity);
            if (!_isHit) return;
            
            var forward = Camera.main.transform.TransformDirection(Vector3.forward) * _hitInfo.distance;
            Debug.DrawRay(ray.origin, forward, Color.red);
        }
    }
}