namespace Store.Domain.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer Get(string document);         
    }
}