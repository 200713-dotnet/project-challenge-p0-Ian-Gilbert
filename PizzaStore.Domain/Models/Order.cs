using System.Collections.Generic;

namespace PizzaStore.Domain.Models
{
    public class Order
    {
        public List<Pizza> Pizzas { get; }

        public Order()
        {
            Pizzas = new List<Pizza>();
        }

        public void CreatePizza(string size, string crust, List<string> toppings)
        {
            Pizzas.Add(new Pizza(size, crust, toppings));
        }
    }
}