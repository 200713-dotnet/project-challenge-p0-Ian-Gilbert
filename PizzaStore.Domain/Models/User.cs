using System.Collections.Generic;

namespace PizzaStore.Domain.Models
{
    public class User
    {
        private List<Order> _orders = new List<Order>();
        public string Name { get; set; }

        public List<Order> Orders
        {
            get
            {
                return _orders;
            }
        }
    }
}