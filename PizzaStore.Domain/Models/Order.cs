using System.Collections.Generic;

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

        public void CreatePizza(string name, Size size, Crust crust, List<Topping> toppings)
        {
            Pizzas.Add(new Pizza(name, size, crust, toppings));
        }

        public double CalculatePrice()
        {
            double price = 0;
            foreach (var Pizza in Pizzas)
            {
                price += Pizza.CalculatePrice();
            }
            return price;
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