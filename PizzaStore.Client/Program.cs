using System;
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
                // Login user
                case 1:
                    System.Console.Write("Your name: ");
                    var name = Console.ReadLine(); // user enters name

                    var user = _db.ReadUser(name); // get user from database
                    System.Console.WriteLine($"\nHello {user.Name}!");

                    UserStory.UserOptions(user); // begin user story
                    break;

                // Login store
                case 2:
                    System.Console.Write("Your name: ");
                    name = Console.ReadLine(); // user enters store name

                    var store = _db.ReadPizzaStore(name); // get store from database
                    System.Console.WriteLine($"\nHello {store.Name}!");

                    StoreStory.StoreOptions(store); // begin store story
                    break;
            }
        }
    }
}
