using System;
using System.Collections.Generic;
using PizzaStore.Domain.Models;
using PizzaStore.Storing.Repositories;

namespace PizzaStore.Client
{
    class Program
    {
        private static Repository _db = new Repository();

        static void Main()
        {
            System.Console.WriteLine("Welcome to Pizza World!\n");

            System.Console.WriteLine("Are you a user or a store?");
            System.Console.WriteLine("1: User");
            System.Console.WriteLine("2: Store");

            int selection;
            System.Console.Write("Your selection: ");
            int.TryParse(Console.ReadLine(), out selection);

            switch (selection)
            {
                case 1:
                    System.Console.Write("Your name: ");
                    var name = Console.ReadLine();
                    var user = _db.ReadUser(name);
                    System.Console.WriteLine($"\nHello {user.Name}!");
                    UserStory.UserOptions(user);
                    break;
                case 2:
                    System.Console.Write("Your name: ");
                    name = Console.ReadLine();
                    var store = _db.ReadPizzaStore(name);
                    System.Console.WriteLine($"\nHello {store.Name}!");
                    StoreStory.StoreOptions(store);
                    break;
            }

            // Repository db = new Repository();

            // var user = new User() { Name = "Ian" };
            // var store = new Store(
            //     db.ReadToppings(),
            //     db.ReadSizes(),
            //     db.ReadCrusts(),
            //     Starter.GeneratePresets()
            // );
            // store.Name = "Store1";
            // var order = store.CreateOrder(user);

            // db.ViewOrdersByUser(user);
            // System.Console.WriteLine();
            // db.ViewOrdersByStore(store);
            // System.Console.WriteLine();
            // db.ViewOrdersByStore(store, user);
        }
    }
}
