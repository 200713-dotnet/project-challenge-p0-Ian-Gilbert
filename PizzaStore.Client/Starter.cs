using System;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client
{
    public class Starter
    {
        public Order CreateOrder(User user, Store store)
        {
            try
            {
                var order = new Order();

                user.Orders.Add(order);
                store.Orders.Add(order);

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

        internal static void PrintMenu()
        {
            System.Console.WriteLine("Select 1 for Cheese Pizza");
            System.Console.WriteLine("Select 2 for Pepperoni Pizza");
            System.Console.WriteLine("Select 3 for Hawaiian Pizza");
            System.Console.WriteLine("Select 4 for Custom Pizza");
            System.Console.WriteLine("Select 5 to view your cart");
            System.Console.WriteLine("Select 6 to save your cart");
            System.Console.WriteLine("Select 7 to retireve your last saved cart");
            System.Console.WriteLine("Select 8 to exit");
            System.Console.WriteLine();
        }
    }
}