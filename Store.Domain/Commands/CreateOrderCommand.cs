using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable, ICommand
    {
        public string Customer { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }

        public CreateOrderCommand()
        {
            this.Items = new List<CreateOrderItemCommand>();
        }
        public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
        {
            this.Customer = customer;
            this.ZipCode = zipCode;
            this.PromoCode = promoCode;
            this.Items = items;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasLen(Customer, 11, "Customer", "Cliente inválido")
                    .HasLen(ZipCode, 8, "ZipCode", "CEP inválido")
            );
        }
    }
}