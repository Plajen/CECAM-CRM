using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CECAM.App.IoC;
using CECAM.Infra.IoC;

namespace CECAM.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterTypes(InfraModule.GetTypes());
            services.RegisterTypes(AppModule.GetTypes());
        }

        public static void RegisterServices(IServiceCollection services, Dictionary<Type, Type> types, bool isSingleton = false)
        {
            services.RegisterTypes(types, isSingleton);
        }

        public static void RegisterServices(IServiceCollection services, List<Type> types)
        {
            services.RegisterTypes(types);
        }

        private static void RegisterTypes(this IServiceCollection container, List<Type> types)
        {
            foreach (var item in types)
            {
                container.AddScoped(item, item);
            }
        }

        private static void RegisterTypes(this IServiceCollection container, Dictionary<Type, Type> types, bool isSingleton = false)
        {
            foreach (var item in types)
            {
                if (isSingleton)
                {
                    container.AddSingleton(item.Key, item.Value);
                    continue;
                }

                container.AddScoped(item.Key, item.Value);
            }
        }
    }
}