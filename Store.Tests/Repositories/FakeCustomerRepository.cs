using Store.Domain;
using Store.Domain.Repositories.Interfaces;

// Mocks

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "12345678911")
                return new Customer("Tony Stark", "stark.tony@industriesstark.com");

            return null;
        }
    }
}