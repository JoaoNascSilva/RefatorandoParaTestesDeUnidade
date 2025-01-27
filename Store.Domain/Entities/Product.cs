namespace Store.Domain.Entities
{
    public class Product : Entity
    {
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public bool Active { get; private set; }

        public Product(string title, decimal price, bool active)
        {
            this.Title = title;
            this.Price = price;
            this.Active = active;
        }
    }
}