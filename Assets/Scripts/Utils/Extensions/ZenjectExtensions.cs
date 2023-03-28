using Zenject;

namespace Utils.Extensions
{
    public static class ZenjectExtensions
    {
        public static void InstallRegistry<TRegistry>(this DiContainer container, TRegistry registry)
        {
            container
                .BindInterfacesAndSelfTo<TRegistry>()
                .FromInstance(registry)
                .AsSingle();
        }
    }
}