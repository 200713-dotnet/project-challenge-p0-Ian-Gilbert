﻿using System;
using System.Collections.Generic;
using PizzaStore.Domain.Models;
using PizzaStore.Storing.Repositories;

namespace PizzaStore.Client
{
    class Program
    {
        static void Main()
        {
            Welcome();

            Repository db = new Repository();

            var user = new User() { Name = "Ian" };
            var store = new Store(
                db.ReadToppings(),
                db.ReadSizes(),
                db.ReadCrusts(),
                Starter.GeneratePresets()
            );
            store.Name = "Store1";
            var order = store.CreateOrder(user);

            try
            {
                OrderPizzas(order, user, store);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        static void Welcome()
        {
            System.Console.WriteLine("Welcome to Pizza World!");
            System.Console.WriteLine("Without doubt the worst pizza delivery service you've ever heard of.");
            System.Console.WriteLine("But you have heard of us!\n");
        }

        static void OrderPizzas(Order cart, User user, Store store)
        {
            SelectPizza(cart, user, store);

            UserOptions(cart, user, store);
        }

        private static void SelectPizza(Order cart, User user1, Store store)
        {
            store.ViewMenu();

            var exit = false;
            do
            {
                int selection;
                System.Console.Write("\nMake a selection: ");
                if (!int.TryParse(Console.ReadLine(), out selection)
                    || selection < 1
                    || selection > store.PizzaPresets.Count
                )
                {
                    System.Console.WriteLine("Whoops, that wasn't an option!\n");
                    continue;
                }

                var pizza = store.CreatePizza(
                    store.PizzaPresets[selection - 1].Name,
                    store.PizzaPresets[selection - 1].Toppings,
                    cart
                );
                System.Console.WriteLine($"{pizza.Name} Pizza selected!");
                store.AddSize(GetUserSize(store), pizza);
                store.AddCrust(GetUserCrust(store), pizza);
                if (pizza.Name == "Custom")
                {
                    store.AddToppings(GetUserToppings(store), pizza);
                }
                System.Console.WriteLine($"{pizza.Name} Pizza added to your cart!");
                return;

            } while (!exit);
        }

        private static void UserOptions(Order cart, User user, Store store)
        {
            var exit = false;

            do
            {
                System.Console.WriteLine();
                cart.DisplayOrder();
                System.Console.WriteLine("Select 1 to add another pizza");
                System.Console.WriteLine("Select 2 to remove a pizza");
                System.Console.WriteLine("Select 3 to cancel your order");
                System.Console.WriteLine("Select 4 to checkout");
                System.Console.WriteLine();

                int selection;
                System.Console.Write("Your selection: ");
                int.TryParse(Console.ReadLine(), out selection);

                switch (selection)
                {
                    case 1:
                        SelectPizza(cart, user, store);
                        break;
                    case 2:
                        int PizzaIndex;
                        System.Console.WriteLine("\nWhich pizza would you like to remove?");
                        System.Console.Write("Your selection: ");
                        if (int.TryParse(Console.ReadLine(), out PizzaIndex))
                        {
                            try
                            {
                                cart.Pizzas.RemoveAt(PizzaIndex - 1);
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                System.Console.WriteLine("There didn't seem to be a pizza at that index.");
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Sorry, that didn't seem to be a number.");
                        }
                        break;
                    case 3:
                        user.Orders.Remove(cart);
                        store.Orders.Remove(cart);
                        exit = true;
                        System.Console.WriteLine("Order cancelled. Goodbye.");
                        break;
                    case 4:
                        var db = new Repository();
                        db.CreateOrder(cart, user, store);
                        exit = true;
                        System.Console.WriteLine("Order submitted!");
                        break;
                    default:
                        System.Console.WriteLine("Whoops, that wasn't an option!");
                        break;
                }
            } while (!exit);
        }

        private static List<Topping> GetUserToppings(Store store)
        {
            var UserToppings = new List<Topping>();
            var exit = false;

            System.Console.WriteLine("\nSelect 2 - 5 toppings from the list below. Press 11 to save your choices.");
            store.ViewToppings();

            do
            {
                int selection;
                System.Console.Write("\nYour choice: ");
                int.TryParse(Console.ReadLine(), out selection);

                // If user tries to exit
                if (selection == store.Toppings.Count + 1)
                {
                    if (UserToppings.Count > 1)
                    {
                        exit = true;
                        System.Console.WriteLine("Toppings selected!");
                    }
                    else
                    {
                        System.Console.WriteLine("You need to select at least 2 toppings");
                    }
                }
                else
                {
                    try
                    {
                        UserToppings.Add(store.Toppings[selection - 1]);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                        System.Console.WriteLine("That doesn't seem to be one of the options.");
                    }
                }

                if (UserToppings.Count == 5)
                {
                    exit = true;
                    System.Console.WriteLine("You have selected all five of your toppings!");
                }
            } while (!exit);

            return UserToppings;
        }

        private static Crust GetUserCrust(Store store)
        {
            System.Console.WriteLine();
            store.ViewCrusts();

            do
            {
                int selection;
                System.Console.Write("\nYour choice: ");
                int.TryParse(Console.ReadLine(), out selection);

                try
                {
                    return store.Crusts[selection - 1];
                }
                catch (System.ArgumentOutOfRangeException)
                {

                    System.Console.WriteLine("That doesn't seem to be one of the options.");
                }
            } while (true);
        }

        private static Size GetUserSize(Store store)
        {
            System.Console.WriteLine();
            store.ViewSizes();

            do
            {
                int selection;
                System.Console.Write("\nYour choice: ");
                int.TryParse(Console.ReadLine(), out selection);

                try
                {
                    return store.Sizes[selection - 1];
                }
                catch (System.ArgumentOutOfRangeException)
                {

                    System.Console.WriteLine("That doesn't seem to be one of the options.");
                }
            } while (true);
        }
    }
}
