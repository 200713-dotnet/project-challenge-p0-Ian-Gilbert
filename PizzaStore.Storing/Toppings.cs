using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Toppings
    {
        public Toppings()
        {
            PizzaTopping = new HashSet<PizzaTopping>();
        }

        public short ToppingId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<PizzaTopping> PizzaTopping { get; set; }
    }
}
