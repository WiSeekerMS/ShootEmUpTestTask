using Common;
using FPS;
using Target;
using UI;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private SettingsPanel _settingsPanel;
        [SerializeField] private GameUIController _gameUIController;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private TargetCreator _targetCreator;
        [SerializeField] private FPSController _fpsController;
        
        public override void InstallBindings()
        {
            Container
                .Bind<SettingsPanel>()
                .FromInstance(_settingsPanel)
                .AsSingle();
            
            Container
                .Bind<GameUIController>()
                .FromInstance(_gameUIController)
                .AsSingle();
            
            Container
                .Bind<GameManager>()
                .FromInstance(_gameManager)
                .AsSingle();
            
            Container
                .Bind<TargetCreator>()
                .FromInstance(_targetCreator)
                .AsSingle();
            
            Container
                .Bind<FPSController>()
                .FromInstance(_fpsController)
                .AsSingle();
        }
    }
}