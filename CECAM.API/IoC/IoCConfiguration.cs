using CECAM.CrossCutting.IoC;

namespace CECAM.API.IoC
{
    public static class IoCConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            Bootstrapper.RegisterServices(services, configuration);
            Bootstrapper.RegisterServices(services, ApiModule.GetSingleTypes());
            Bootstrapper.RegisterServices(services, ApiModule.GetTypes(), true);
        }
    }
}
