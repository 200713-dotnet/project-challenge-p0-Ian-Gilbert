using System;
using System.Collections.Generic;
using PizzaStore.Domain.Models;
using PizzaStore.Storing.Repositories;

namespace PizzaStore.Client
{
    public class UserStory
    {
        private static Repository _db = new Repository();

        // entrypoint to user story
        public static void UserOptions(User user)
        {
            var exit = false;
            do
            {
                System.Console.WriteLine("\nHow can we help you today?");
                System.Console.WriteLine("1: Order pizzas");
                System.Console.WriteLine("2: View your order history");
                System.Console.WriteLine("3: Exit");

                int selection;
                System.Console.Write("Your selection: ");
                int.TryParse(Console.ReadLine(), out selection);

                switch (selection)
                {
                    // order pizzas option
                    case 1:
                        var storeList = _db.ReadAllStores(); // read stores from database

                        System.Console.WriteLine("\nWhich store would you like to order from?");
                        for (int i = 0; i < storeList.Count; i++)
                        {
                            System.Console.WriteLine($"{i + 1}: {storeList[i].Name}");
                        }

                        int storeId;
                        System.Console.Write("Your selection: ");
                        int.TryParse(Console.ReadLine(), out storeId);

                        var store = storeList[storeId - 1]; // select a store to order from
                        store.Toppings.AddRange(_db.ReadToppings());
                        store.Sizes.AddRange(_db.ReadSizes());
                        store.Crusts.AddRange(_db.ReadCrusts());
                        store.PizzaPresets.AddRange(Starter.GeneratePresets());

                        System.Console.WriteLine($"\nOrdering from {store.Name}");

                        var order = store.CreateOrder(user); // create an order
                        System.Console.WriteLine();

                        try
                        {
                            OrderPizzas(order, user, store); // begin ordering pizzas
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
                        }
                        break;
                    // view order history for user
                    case 2:
                        ViewOrderHistory(user);
                        break;
                    // exit application
                    case 3:
                        exit = true;
                        System.Console.WriteLine("\nThank you, have a good day!");
                        break;
                }
            } while (!exit);
        }

        private static void ViewOrderHistory(User user)
        {
            _db.ViewOrdersByUser(user);
        }

        // create and submit an order of pizzas
        private static void OrderPizzas(Order cart, User user, Store store)
        {
            // select first pizza
            SelectPizza(cart, user, store);

            // options to add more pizzas, remove pizzas, cancel order, or submit
            UserOrderOptions(cart, user, store);
        }

        // add a single pizza to an order
        private static void SelectPizza(Order cart, User user1, Store store)
        {
            store.ViewMenu();

            do
            {
                // select a base pizza from menu
                int selection;
                System.Console.Write("\nMake a selection: ");
                if
                (
                    !int.TryParse(Console.ReadLine(), out selection)
                    || selection < 1
                    || selection > store.PizzaPresets.Count
                )
                {
                    System.Console.WriteLine("Whoops, that wasn't an option!\n");
                    continue;
                }

                // create base pizza from user selection
                var pizza = store.CreatePizza
                (
                    store.PizzaPresets[selection - 1].Name,
                    store.PizzaPresets[selection - 1].Toppings,
                    cart
                );
                System.Console.WriteLine($"{pizza.Name} Pizza selected!");
                store.AddSize(GetUserSize(store), pizza); // get user size
                store.AddCrust(GetUserCrust(store), pizza); // get user crust
                // if pizza is custom, let user choose toppings
                if (pizza.Name == "Custom")
                {
                    store.AddToppings(GetUserToppings(store), pizza);
                }
                System.Console.WriteLine($"{pizza.Name} Pizza added to your cart!");
                return;

            } while (true);
        }

        // options for user to add or remove pizzas from cart, cancel or submit order
        private static void UserOrderOptions(Order cart, User user, Store store)
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
                    // add another pizza. Block if cart is full
                    case 1:
                        if (cart.Pizzas.Count == 50)
                        {
                            System.Console.WriteLine("Sorry, you have reached the 50 pizza limit per order.");
                        }
                        else
                        {
                            SelectPizza(cart, user, store);
                        }
                        break;

                    // remove a pizza
                    case 2:
                        int PizzaIndex;
                        System.Console.WriteLine("\nWhich pizza would you like to remove?\n");
                        cart.DisplayOrder(); // show cart (contains indexes to select a pizza)
                        System.Console.Write("Your selection: ");
                        if (int.TryParse(Console.ReadLine(), out PizzaIndex))
                        {
                            try
                            {
                                cart.Pizzas.RemoveAt(PizzaIndex - 1); // remove the pizza from cart
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

                    // cancel the order. Just remove it from the store and user order lists
                    case 3:
                        user.Orders.Remove(cart);
                        store.Orders.Remove(cart);
                        exit = true;
                        System.Console.WriteLine("Order cancelled.");
                        break;

                    // submit the order (i.e. add to database)
                    case 4:
                        _db.CreateOrder(cart, user, store);
                        exit = true;
                        System.Console.WriteLine("Order submitted!");
                        break;
                    default:
                        System.Console.WriteLine("Whoops, that wasn't an option!");
                        break;
                }
            } while (!exit);
        }

        // get user toppings for cutom pizza
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

                // If user is finished adding toppings. Check that they have added at least 2
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
                else // add a topping
                {
                    try
                    {
                        var topping = store.Toppings[selection - 1];
                        UserToppings.Add(topping);
                        System.Console.WriteLine($"{topping.Name} added");
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                        System.Console.WriteLine("That doesn't seem to be one of the options.");
                    }
                }

                // if user has added 5 toppings, they cannot add any more
                if (UserToppings.Count == 5)
                {
                    exit = true;
                    System.Console.WriteLine("You have selected all five of your toppings!");
                }
            } while (!exit);

            return UserToppings; // return selected toppings
        }

        // get user crust selection
        private static Crust GetUserCrust(Store store)
        {
            System.Console.WriteLine();
            store.ViewCrusts(); // crust options

            do
            {
                int selection;
                System.Console.Write("\nYour choice: ");
                int.TryParse(Console.ReadLine(), out selection);

                try
                {
                    var crust = store.Crusts[selection - 1];
                    System.Console.WriteLine($"{crust.Name} crust selected");
                    return crust; // return selected crust
                }
                catch (System.ArgumentOutOfRangeException)
                {

                    System.Console.WriteLine("That doesn't seem to be one of the options.");
                }
            } while (true);
        }

        // get user size selection
        private static Size GetUserSize(Store store)
        {
            System.Console.WriteLine();
            store.ViewSizes(); // size options

            do
            {
                int selection;
                System.Console.Write("\nYour choice: ");
                int.TryParse(Console.ReadLine(), out selection);

                try
                {
                    var size = store.Sizes[selection - 1];
                    System.Console.WriteLine($"Size {size.Name} pizza");
                    return size; // return selected size
                }
                catch (System.ArgumentOutOfRangeException)
                {

                    System.Console.WriteLine("That doesn't seem to be one of the options.");
                }
            } while (true);
        }
    }
}