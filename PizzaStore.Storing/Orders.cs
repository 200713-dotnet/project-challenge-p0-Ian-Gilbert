﻿using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Orders
    {
        public Orders()
        {
            PizzaOrders = new HashSet<PizzaOrders>();
        }

        public int OrderId { get; set; }
        public int UserSubmittedId { get; set; }
        public int StoreSubmittedId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }

        public virtual Store StoreSubmitted { get; set; }
        public virtual Users UserSubmitted { get; set; }
        public virtual ICollection<PizzaOrders> PizzaOrders { get; set; }
    }
}
