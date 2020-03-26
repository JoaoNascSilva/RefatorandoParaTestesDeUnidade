using System;
using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class OrderItem : Entity
    {
        public Product Product { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(Product product, int quantity)
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNull(product, "Product", "Produto inválido")
                .IsGreaterThan(quantity, 0, "Quantity", "A quantidade deve ser maior que zero")
            );

            this.Product = product;
            this.Price = Product != null ? product.Price : 0;
            this.Quantity = quantity;
        }

        // Cálculo de Sub-Total
        public decimal Total()
        {
            return this.Price * this.Quantity;
        }
    }
}