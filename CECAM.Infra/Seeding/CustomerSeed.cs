using Microsoft.EntityFrameworkCore;
using CECAM.Domain.Models;

namespace CECAM.Infra.Seeding
{
	public static class CustomerSeed
	{
		public static void SeedCustomer(this ModelBuilder builder)
		{
			builder.Entity<Customer>().HasData(
				new Customer(1, "Google", "69399471000110"),
				new Customer(2, "Facebook", "98869420000103"),
				new Customer(3, "Amazon", "83052668000100"));
		}
	}
}
