using Store.Domain.Entities;

namespace Store.Domain
{
    public class Customer : Entity
    {
        public Customer(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}