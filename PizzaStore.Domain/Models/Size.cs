namespace PizzaStore.Domain.Models
{
    public class Size
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Size() { }

        public Size(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name} - {Price.ToString("C2")}";
        }
    }
}