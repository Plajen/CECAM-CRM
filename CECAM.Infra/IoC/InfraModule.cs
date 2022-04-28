using CECAM.Infra.Interfaces;
using CECAM.Infra.Repositories;

namespace CECAM.Infra.IoC
{
    public static class InfraModule
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var result = new Dictionary<Type, Type>
            {
                { typeof(ICustomerRepository), typeof(CustomerRepository) }
            };

            return result;
        }
    }
}
