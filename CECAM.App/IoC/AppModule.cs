using CECAM.App.Interfaces;
using CECAM.App.Services;

namespace CECAM.App.IoC
{
    public static class AppModule
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var result = new Dictionary<Type, Type>
            {
                { typeof(ICustomerService), typeof(CustomerService) }
            };

            return result;
        }
    }
}
