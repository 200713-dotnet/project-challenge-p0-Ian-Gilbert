using System;
using PizzaStore.Domain.Models;
using PizzaStore.Storing.Repositories;

namespace PizzaStore.Client
{
    public class StoreStory
    {
        private static Repository _db = new Repository();

        // entrypoint for store story
        public static void StoreOptions(Store store)
        {
            var exit = false;
            do
            {
                System.Console.WriteLine("\nHow can we help you today?");
                System.Console.WriteLine("1: View your order history");
                // System.Console.WriteLine("2: View your sales history");
                System.Console.WriteLine("2: Exit");

                int selection;
                System.Console.Write("Your selection: ");
                int.TryParse(Console.ReadLine(), out selection);

                switch (selection)
                {
                    case 1:
                        ViewOrderHistory(store);
                        break;
                    // case 2:
                    //     ViewSalesHistory(store);
                    //     break;
                    case 2:
                        exit = true;
                        System.Console.WriteLine("\nThank you, have a good day!");
                        break;
                }
            } while (!exit);
        }

        private static void ViewSalesHistory(Store store)
        {
            throw new NotImplementedException();
        }

        // select whether or not to filter by user
        private static void ViewOrderHistory(Store store)
        {
            System.Console.WriteLine("\nWould you like to see all orders, or only see orders placed by a specific user?");
            System.Console.WriteLine("1: See all orders");
            System.Console.WriteLine("2: Select a user");

            int selection;
            System.Console.Write("Your selection: ");
            int.TryParse(Console.ReadLine(), out selection);

            switch (selection)
            {
                case 1:
                    ViewAllOrders(store); // #nofilter
                    break;
                case 2:
                    ViewUserOrders(store); // select a user and filter
                    break;
            }
        }

        // select a user and filter
        private static void ViewUserOrders(Store store)
        {
            var userList = _db.ReadAllUsers();

            System.Console.WriteLine("Which user's orders would you like to see?");
            for (int i = 0; i < userList.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}: {userList[i].Name}");
            }

            int selection;
            System.Console.Write("Your selection: ");
            int.TryParse(Console.ReadLine(), out selection);

            var user = userList[selection - 1];

            _db.ViewOrdersByStore(store, user);
        }

        // #nofilter
        private static void ViewAllOrders(Store store)
        {
            _db.ViewOrdersByStore(store);
        }
    }
}