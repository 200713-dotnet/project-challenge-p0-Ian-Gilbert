using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Orders>();
        }

        public int StoreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
