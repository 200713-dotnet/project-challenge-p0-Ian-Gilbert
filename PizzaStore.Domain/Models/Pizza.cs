using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Domain.Models
{
    public class Pizza
    {
        // fields
        private readonly string _imageUrl;
        private const double _diameter = 0;
        private List<string> _toppings = new List<string>();

        //properties
        public string Size { get; set; }
        public string Crust { get; set; }
        public List<string> Toppings
        {
            get
            {
                return _toppings;
            }
        }

        public Pizza() { }

        public Pizza(string size, string crust, List<string> toppings)
        {
            Size = size;
            Crust = crust;
            // Toppings = new List<string>();
            Toppings.AddRange(toppings);
        }

        // methods
        void AddToppings(string topping)
        {
            Toppings.Add(topping);
        }

        public override string ToString()
        {
            // var sb = new StringBuilder();
            // foreach (var t in toppings)
            // {
            //     sb.Append(t);
            // }

            // return $"{crust} {size}\nToppings: {sb.ToString()}";

            return $"{Crust} {Size}\nToppings: {string.Join(", ", Toppings)}";
        }
    }
}