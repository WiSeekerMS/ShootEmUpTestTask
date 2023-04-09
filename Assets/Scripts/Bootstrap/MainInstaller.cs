using Common.InputSystem.Installers;
using Gameplay.ShootSystem.Installers;
using Zenject;

namespace Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.Install<InputInstaller>();
            Container.Install<ShootInstaller>();
        }
    }
}