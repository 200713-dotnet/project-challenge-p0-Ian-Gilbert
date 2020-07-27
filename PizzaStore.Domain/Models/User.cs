using System.Collections.Generic;

namespace PizzaStore.Domain.Models
{
    public class User
    {
        private List<Order> _orders = new List<Order>();

        public List<Order> Orders
        {
            get
            {
                return _orders;
            }
        }
    }
}