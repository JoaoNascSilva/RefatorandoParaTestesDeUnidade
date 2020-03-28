using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderHandlerTests()
        {
            this._customerRepository = new FakeCustomerRepository();
            this._deliveryFeeRepository = new FakeDeliveryFeeRepository();
            this._discountRepository = new FakeDiscountRepository();
            this._orderRepository = new FakeOrderRepository();
            this._productRepository = new FakeProductsRepository();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmClienteInexistenteOPedidoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "13454424";
            command.PromoCode = "12345678";            
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmCepInvalidoOPedidoDeveSerGeradoNormalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "João Silvaa";
            command.ZipCode = "13454424";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.Valid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmPromoCodeInexistenteOPedidoDeveSerGeradoNormalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "João Silva";
            command.ZipCode = "13454424";
            command.PromoCode = "";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmPedidoSemItensOMesmoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "João Silva";
            command.ZipCode = "13454424";
            command.PromoCode = "12345678";
            // command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            // command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = string.Empty;
            command.ZipCode = "13454424";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoValidoOPedidoDeveSerGerado()
        {
            // Somente Validação de Geração do Pedido Neste momento -> Fail Fast Validate
            var command = new CreateOrderCommand();
            command.Customer = "João Silva";
            command.ZipCode = "13454424";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            // Geração do Comando de -> Save
            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
            handler.Handle(command);

            Assert.AreEqual(handler.Valid, true);
        }
    }

}
