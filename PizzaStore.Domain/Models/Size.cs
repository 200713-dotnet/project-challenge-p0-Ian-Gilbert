namespace PizzaStore.Domain.Models
{
    public class Size
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Size() { }

        public Size(string name, double price)
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