using Common.Extensions;
using Common.InputSystem.Services;
using Common.InputSystem.Signals;
using Zenject;

namespace Common.InputSystem.Installers
{
    public class InputInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<InputService>();
            Container.DeclareSignal<InputSignals.Shot>();
            Container.DeclareSignal<InputSignals.Reload>();
        }
    }
}