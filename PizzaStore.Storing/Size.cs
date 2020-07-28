using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Size
    {
        public Size()
        {
            Pizza = new HashSet<Pizza>();
        }

        public byte SizeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Pizza> Pizza { get; set; }
    }
}
