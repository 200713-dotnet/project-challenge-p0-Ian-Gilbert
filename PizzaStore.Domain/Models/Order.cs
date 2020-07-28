using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Domain.Models
{
    public class Order
    {
        private List<Pizza> _pizzas = new List<Pizza>();

        public List<Pizza> Pizzas
        {
            get
            {
                return _pizzas;
            }
        }

        public Pizza CreatePizza(string name, List<Topping> toppings)
        {
            var pizza = new Pizza(name, toppings);
            Pizzas.Add(pizza);
            return pizza;
        }

        public decimal CalculatePrice()
        {
            return Pizzas.Sum(Pizza => Pizza.CalculatePrice());
        }

        public void DisplayOrder()
        {
            System.Console.WriteLine($"Total Price: {CalculatePrice().ToString("C2")}");
            for (int i = 0; i < Pizzas.Count; i++)
            {
                System.Console.Write(i + 1 + ": ");
                System.Console.WriteLine(Pizzas[i]);
                System.Console.WriteLine();
            }
        }
    }
}