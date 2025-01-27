using System;
using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderItemCommand : Notifiable, ICommand
    {
        public Guid Product { get; set; }
        public int Quantity { get; set; }

        public CreateOrderItemCommand() { }
        public CreateOrderItemCommand(Guid product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        
        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasLen(Product.ToString(), 32, "Product", "Produto Inválido")
                    .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade Inválida")
            );
        }
    }
}