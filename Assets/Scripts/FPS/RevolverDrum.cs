using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPS
{
    public class RevolverDrum : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _bullets;

        public void HideBullet()
        {
            _bullets
                .FirstOrDefault(b => b.activeSelf)
                ?.SetActive(false);
        }

        public void ShowAllBullets()
        {
            _bullets.ForEach(b => b.SetActive(true));
        }
    }
}