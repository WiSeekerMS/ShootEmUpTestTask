using FPS;
using UnityEngine;
using Zenject;

namespace Gameplay.ShootSystem.Factories
{
    public class BulletFactory : PlaceholderFactory<FlyingBullet>
    {
        private DiContainer _diContainer;

        public BulletFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public FlyingBullet Create(FlyingBullet prefab, Vector3 worldPosition, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<FlyingBullet>(prefab,
                worldPosition, Quaternion.identity, parent);
        }
    }
}