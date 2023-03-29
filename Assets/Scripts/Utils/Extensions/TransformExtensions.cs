using UnityEngine;

namespace Utils.Extensions
{
    public static class TransformExtensions
    {
        public static Vector3 GetDirection(Vector3 position, Vector3 target)
        {
            var heading = target - position;
            var distance = heading.magnitude;
            return heading / distance;
        }
        
        public static void LookAt(this Transform transform, Vector3 target)
        {
            var direction = GetDirection(transform.position, target);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}