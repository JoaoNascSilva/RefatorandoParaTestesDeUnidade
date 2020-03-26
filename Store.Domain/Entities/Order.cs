using System;
using System.Collections.Generic;
using Flunt.Validations;
using Store.Domain.Entities.Enums;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public Discount Discount { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public string Number { get; private set; }
        public EOrderStatus Status { get; private set; }

        public Order(Customer customer, decimal deliveryFee, Discount discount)
        {
            // Design By Context
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNull(customer, "Customer", "Cliente inválido")
            );

            this.Number = Guid.NewGuid().ToString().Substring(0, 8);
            this.Date = DateTime.Now;
            this.Status = EOrderStatus.WaitingPayment;
            this.Customer = customer;
            this.DeliveryFee = deliveryFee;
            this.Discount = discount;
            this.Items = new List<OrderItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            if (item.Valid)
                Items.Add(item);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var item in Items)
                total += item.Total();

            total += DeliveryFee;
            total -= Discount != null ? Discount.Value() : 0;
            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total())
                this.Status = EOrderStatus.WaitingDelivery;
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
        }
    }
}