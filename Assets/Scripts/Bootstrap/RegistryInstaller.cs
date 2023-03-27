using Configs;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(fileName = "RegistryInstaller", menuName = "Installers/RegistryInstaller")]
    public class RegistryInstaller : ScriptableObjectInstaller<RegistryInstaller>
    {
        [SerializeField] private MainConfig _mainConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<MainConfig>()
                .FromInstance(_mainConfig)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<PlayerConfig>()
                .FromInstance(_playerConfig)
                .AsSingle();
        }
    }
}