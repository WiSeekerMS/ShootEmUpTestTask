using Common.Extensions;
using FPS.Factories;
using FPS.Models;
using FPS.Presenters;
using FPS.Signals;
using Zenject;

namespace FPS.Installers
{
    public class ShootInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<MouseLookPresenter>();
            Container.InstallService<AimCameraPresenter>();
            
            Container.InstallModel<MouseLookModel>();
            Container.InstallModel<AimCameraModel>();

            Container.DeclareSignal<ShootSignals.UpdateRotation>();
            Container.DeclareSignal<ShootSignals.UpdateAimCameraPosition>();
            Container.DeclareSignal<ShootSignals.UpdateAimCameraFieldOfView>();
            
            Container.InstallFactory<FlyingBullet, BulletFactory>();
        }
    }
}