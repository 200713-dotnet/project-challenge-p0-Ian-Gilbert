using PizzaStore.Client;
using PizzaStore.Domain.Models;
using Xunit;

namespace PizzaStore.Testing.Tests
{
    public class StarterTests
    {
        [Fact]
        public void Test_CreateOrder()
        {
            var sut = new Starter();
            var user = new User();
            var store = new Store();

            var actual = sut.CreateOrder(user, store);

            Assert.NotNull(actual);
        }
    }
}