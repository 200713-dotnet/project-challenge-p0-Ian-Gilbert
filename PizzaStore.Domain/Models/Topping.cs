namespace PizzaStore.Domain.Models
{
    public class Topping
    {
        public decimal Price { get; set; }
        public string Name { get; set; }

        public Topping(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name} ----- {Price.ToString("C2")}";
        }
    }
}