using System.Linq;
using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;
using Store.Domain.Utils;

namespace Store.Domain.Handlers
{
    public class OrderHandler :
        Notifiable,
        IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _produtctRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(
            ICustomerRepository customerRepository,
            IDeliveryFeeRepository deliveryFeeRepositor,
            IDiscountRepository discountRepository,
            IProductRepository produtctRepository,
            IOrderRepository orderRepository)
        {
            this._customerRepository = customerRepository;
            this._deliveryFeeRepository = deliveryFeeRepositor;
            this._discountRepository = discountRepository;
            this._orderRepository = orderRepository;
            this._produtctRepository = produtctRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            //* Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

            //* Recupera o Cliente
            var customer = _customerRepository.Get(command.Customer);

            //* Calcula a taxa de entrega
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

            //* Obtém o cupom de desconto
            var discount = _discountRepository.Get(command.PromoCode);

            //* Gera o Pedido
            var products = _produtctRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
            var order = new Order(customer, deliveryFee, discount);

            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            //* Agrupa as notificações
            AddNotifications(order.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar pedido", Notifications);

            _orderRepository.Save(order);
                return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso!", order);
        }
    }
}