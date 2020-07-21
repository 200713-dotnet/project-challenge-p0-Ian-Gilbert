using System.Collections.Generic;
using PizzaStore.Domain.Models;
using Xunit;

namespace PizzaStore.Testing.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Test_CreatePizza()
        {
            var sut = new Order();
            string size = "S";
            string curst = "C";
            List<string> toppings = new List<string> { "T" };

            sut.CreatePizza(size, curst, toppings);

            Assert.True(sut.Pizzas.Count > 0);
        }
    }
}