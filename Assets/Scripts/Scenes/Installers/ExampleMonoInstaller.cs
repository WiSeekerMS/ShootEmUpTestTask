using Common;
using Common.Audio;
using Common.Extensions;
using Target;
using UI;
using UnityEngine;
using Zenject;

namespace Scenes.Installers
{
    public class ExampleMonoInstaller : MonoInstaller
    {
        [SerializeField] private SettingsPanel _settingsPanel;
        [SerializeField] private GameUIController _gameUIController;
        [SerializeField] private AudioController _audioController;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private TargetCreator _targetCreator;

        public override void InstallBindings()
        {
            Container.InstallRegistry(_settingsPanel);
            Container.InstallRegistry(_gameUIController);
            Container.InstallRegistry(_audioController);
            Container.InstallRegistry(_gameManager);
            Container.InstallRegistry(_targetCreator);
        }
    }
}