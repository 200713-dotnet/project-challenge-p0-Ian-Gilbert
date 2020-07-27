using System.Collections.Generic;

namespace PizzaStore.Domain.Models
{
    public class Pizza
    {
        private List<Topping> _toppings = new List<Topping>();

        public string Name { get; set; }
        public List<Topping> Toppings
        {
            get
            {
                return _toppings;
            }
        }
        public Crust Crust { get; }
        public Size Size { get; }

        public Pizza() { }

        public Pizza(string name, Size size, Crust crust, List<Topping> toppings)
        {
            Name = name;
            Size = size;
            Crust = crust;
            Toppings.AddRange(toppings);
        }

        void AddToppings(Topping topping)
        {
            Toppings.Add(topping);
        }

        public double CalculatePrice()
        {
            double price = 0;

            price += Size.Price;
            price += Crust.Price;
            foreach (var topping in Toppings)
            {
                price += topping.Price;
            }

            return price;
        }

        public override string ToString()
        {
            return $"{Name}, {Size.Name}, {Crust.Name} ----- {CalculatePrice().ToString("C2")}\n" +
                $"Toppings: {string.Join(", ", Toppings)}";
        }
    }
}