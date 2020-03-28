using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandTests
    {
        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandInvalidOPedidoNaoDeveSerGerado()
        {
            // FailFastValidation
            var command = new Domain.Commands.CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "13454424";
            command.PromoCode = "123456";
            command.Items.Add(new Domain.Commands.CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new Domain.Commands.CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(false, command.Valid);
        }
    }
}