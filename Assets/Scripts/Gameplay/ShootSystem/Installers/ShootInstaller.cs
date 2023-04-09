using Common.Extensions;
using FPS;
using Gameplay.ShootSystem.Factories;
using Gameplay.ShootSystem.Models;
using Gameplay.ShootSystem.Presenters;
using Gameplay.ShootSystem.Signals;
using Zenject;

namespace Gameplay.ShootSystem.Installers
{
    public class ShootInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<ShootPresenter>();
            Container.InstallService<MouseLookPresenter>();
            Container.InstallService<AimCameraPresenter>();
            
            Container.InstallModel<ShootModel>();
            Container.InstallModel<MouseLookModel>();
            Container.InstallModel<AimCameraModel>();

            Container.DeclareSignal<ShootSignals.HitTarget>();
            Container.DeclareSignal<ShootSignals.UpdateRotation>();
            Container.DeclareSignal<ShootSignals.UpdateAimCameraPosition>();
            Container.DeclareSignal<ShootSignals.UpdateAimCameraFieldOfView>();
            
            Container.InstallFactory<FlyingBullet, BulletFactory>();
        }
    }
}