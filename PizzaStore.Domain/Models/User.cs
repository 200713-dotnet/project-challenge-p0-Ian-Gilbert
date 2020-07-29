using System.Collections.Generic;

namespace PizzaStore.Domain.Models
{
    public class User
    {
        public string Name { get; set; }

        private List<Order> _orders = new List<Order>();
        public List<Order> Orders { get { return _orders; } }
    }
}