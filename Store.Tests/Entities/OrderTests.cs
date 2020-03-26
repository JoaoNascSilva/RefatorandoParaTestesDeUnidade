using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain;
using Store.Domain.Entities;
using Store.Domain.Entities.Enums;

namespace Store.Tests.Entities
{

    // Utilizar a Técnica de Red, Green, Refactor

    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("João Silva", "joao@gmail.com");
        private readonly Discount _discount = new Discount(0, DateTime.Now.AddDays(5));
        private readonly Product _product = new Product("Mouses wirelles", 100, true);


        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_Novo_Pedido_Valido_Ele_Deve_Gerar_Um_Numero_Com_8_Caracteres()
        {
            var order = new Order(_customer, 0, null);

            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_Novo_Pedido_Seu_Status_Deve_Ser_Aguardando_Pagamento()
        {
            var order = new Order(_customer, 100, null);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_Um_Pagamento_Do_Pedido_Seu_Status_Deve_Ser_Aguardando_Entrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);
            order.Pay(100);

            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }

    }
}