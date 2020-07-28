using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Domain.Models
{
    public class Pizza
    {
        private List<Topping> _toppings = new List<Topping>();

        public string Name { get; }
        public List<Topping> Toppings
        {
            get
            {
                return _toppings;
            }
        }
        public Crust Crust { get; internal set; }
        public Size Size { get; internal set; }

        public Pizza() { }

        public Pizza(string name, Size size, Crust crust)
        {
            Name = name;
            Size = size;
            Crust = crust;
        }

        // for creating presets
        public Pizza(string name, List<Topping> toppings)
        {
            Name = name;
            Toppings.AddRange(toppings);
        }

        public void AddTopping(Topping topping)
        {
            Toppings.Add(topping);
        }

        public void AddToppings(List<Topping> toppings)
        {
            Toppings.AddRange(toppings);
        }

        public double CalculatePrice()
        {
            return Size.Price + Crust.Price + Toppings.Sum(topping => topping.Price);
        }

        public override string ToString()
        {
            return $"{Name}, {Size.Name}, {Crust.Name} ----- {CalculatePrice().ToString("C2")}\n" +
                $"Toppings: {string.Join(", ", Toppings.Select(topping => topping.Name).ToArray())}";
        }
    }
}