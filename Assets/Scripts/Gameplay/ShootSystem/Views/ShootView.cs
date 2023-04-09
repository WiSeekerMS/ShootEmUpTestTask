using Gameplay.ShootSystem.Presenters;
using UnityEngine;
using Zenject;

namespace Gameplay.ShootSystem.Views
{
    public class ShootView : MonoBehaviour
    {
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private Transform _bulletParent;
        [Inject] private ShootPresenter _shootPresenter;

        private void Start()
        {
            _shootPresenter.SetMuzzleTransform(_muzzleTransform);
        }
    }
}