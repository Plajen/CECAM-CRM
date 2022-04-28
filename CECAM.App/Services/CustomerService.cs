using CECAM.App.Interfaces;
using CECAM.Domain.Models;
using CECAM.Infra.Interfaces;

namespace CECAM.App.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        public CustomerService(ICustomerRepository repository) : base(repository) { }
    }
}
