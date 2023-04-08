﻿using Common.Extensions;
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
            Container.InstallRegistry(_mainConfig);
            Container.InstallRegistry(_playerConfig);
        }
    }
}