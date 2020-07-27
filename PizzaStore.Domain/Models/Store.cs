using System;
using System.Collections.Generic;

namespace PizzaStore.Domain.Models
{
    public class Store
    {
        private List<Order> _orders = new List<Order>();

        public List<Order> Orders
        {
            get
            {
                return _orders;
            }
        }
        public string Name { get; set; }
        public string Slogan { get; set; }

        public Store() { }


        public Store(string name, string slogan)
        {
            Name = name;
            Slogan = slogan;
        }

        public Order CreateOrder(User user)
        {
            try
            {
                var order = new Order();

                user.Orders.Add(order);
                Orders.Add(order);

                return order;
            }
            catch
            {
                throw new System.Exception("we messed up");
            }
            finally
            {
                GC.Collect();
            }
        }

        public void viewMenu()
        {

        }

        public void viewOrders()
        {

        }


    }
}