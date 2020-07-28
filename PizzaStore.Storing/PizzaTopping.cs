using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class PizzaTopping
    {
        public short PizzaToppingId { get; set; }
        public int PizzaId { get; set; }
        public short ToppingId { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Toppings Topping { get; set; }
    }
}
