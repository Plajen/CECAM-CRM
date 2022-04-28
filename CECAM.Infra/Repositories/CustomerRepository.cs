using CECAM.Domain.Models;
using CECAM.Infra.Context;
using CECAM.Infra.Interfaces;

namespace CECAM.Infra.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CECAMDbContext context) : base(context) { }
    }
}
