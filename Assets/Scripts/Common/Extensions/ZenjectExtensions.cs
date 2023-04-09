using Zenject;

namespace Common.Extensions
{
    public static class ZenjectExtensions
    {
        public static void InstallService<TService>(this DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<TService>()
                .AsSingle()
                .NonLazy();
        }
        
        public static void InstallRegistry<TRegistry>(this DiContainer container, TRegistry registry)
        {
            container
                .BindInterfacesAndSelfTo<TRegistry>()
                .FromInstance(registry)
                .AsSingle();
        }
        
        public static void InstallFactory<TContext, TFactory>(this DiContainer container) 
            where TFactory : PlaceholderFactory<TContext>
        {
            container
                .BindFactory<TContext, TFactory>()
                .AsSingle();
        }
        
        public static void InstallModel<TModel>(this DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<TModel>()
                .AsSingle();
        }
    }
}