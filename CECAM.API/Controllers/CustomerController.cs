using CECAM.App.Interfaces;
using CECAM.Domain.Models;

namespace CECAM.API.Controllers
{
    public class CustomerController : BaseController<Customer>
    {
        public CustomerController(ICustomerService service) : base(service) { }
    }
}
