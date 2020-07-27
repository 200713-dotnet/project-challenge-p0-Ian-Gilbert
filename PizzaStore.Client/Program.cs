using System;
using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client
{
    class Program
    {
        static void Main()
        {
            Welcome();

            var user = new User();
            var store = new Store();
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
            SelectPizza(cart);

            UserOptions(cart, user, store);

        }

        private static void SelectPizza(Order cart)
        {
            Starter.PrintMenu();

            var exit = false;
            do
            {
                int selection;
                System.Console.Write("\nMake a selection: ");
                if (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > 6)
                {
                    System.Console.WriteLine("Whoops, that wasn't an option!\n");
                    continue;
                }

                switch (selection)
                {
                    case 1:
                        System.Console.WriteLine("Cheese pizza selected");
                        var toppings = new List<Topping>{
                            new Topping("Tomato Sauce", 0.25),
                            new Topping("Cheese", 0.25)
                        };
                        cart.CreatePizza("Cheese", GetUserSize(), GetUserCrust(), toppings);
                        System.Console.WriteLine("added Cheese Pizza");
                        return;
                    case 2:
                        System.Console.WriteLine("Pepperoni pizza selected");
                        toppings = new List<Topping>{
                            new Topping("Tomato Sauce", 0.25),
                            new Topping("Cheese", 0.25),
                            new Topping("Pepperoni", 0.50)
                        };
                        cart.CreatePizza("Pepperoni", GetUserSize(), GetUserCrust(), toppings);
                        System.Console.WriteLine("added Pepperoni Pizza");
                        return;
                    case 3:
                        System.Console.WriteLine("Hawaiian pizza selected");
                        toppings = new List<Topping>{
                            new Topping("Tomato Sauce", 0.25),
                            new Topping("Cheese", 0.25),
                            new Topping("Ham", 0.50),
                            new Topping("Pineapple", 0.50)
                        };
                        cart.CreatePizza("Hawaiian", GetUserSize(), GetUserCrust(), toppings);
                        System.Console.WriteLine("added Hawaiian Pizza");
                        return;
                    case 4:
                        System.Console.WriteLine("Custom pizza selected");
                        cart.CreatePizza("Custom", GetUserSize(), GetUserCrust(), GetUserToppings());
                        System.Console.WriteLine("added Custom Pizza");
                        return;
                    case 6:
                        exit = true;
                        break;
                }
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
                        SelectPizza(cart);
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
                        System.Console.WriteLine("Goodbye.");
                        break;
                    case 4:
                        // store order in db
                        exit = true;
                        System.Console.WriteLine("Order submitted!");
                        break;
                    default:
                        System.Console.WriteLine("Whoops, that wasn't an option!");
                        break;
                }
            } while (!exit);
        }

        private static List<Topping> GetUserToppings()
        {
            var ToppingOptions = Starter.GenerateToppings();
            var UserToppings = new List<Topping>();
            var exit = false;

            System.Console.WriteLine("\nSelect 2 - 5 toppings from the list below. Press 11 to save your choices.");
            for (int i = 0; i < ToppingOptions.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}: {ToppingOptions[i]}");
            }
            System.Console.WriteLine($"{ToppingOptions.Count + 1}: Exit");

            do
            {
                int selection;
                System.Console.Write("\nYour choice: ");
                int.TryParse(Console.ReadLine(), out selection);

                switch (selection)
                {
                    case 1:
                        UserToppings.Add(ToppingOptions[0]);
                        System.Console.WriteLine("Tomato Sauce added");
                        break;
                    case 2:
                        UserToppings.Add(ToppingOptions[1]);
                        System.Console.WriteLine("Cheese added");
                        break;
                    case 3:
                        UserToppings.Add(ToppingOptions[2]);
                        System.Console.WriteLine("Pepperoni added");
                        break;
                    case 4:
                        UserToppings.Add(ToppingOptions[3]);
                        System.Console.WriteLine("Sausage added");
                        break;
                    case 5:
                        UserToppings.Add(ToppingOptions[4]);
                        System.Console.WriteLine("Olives added");
                        break;
                    case 6:
                        UserToppings.Add(ToppingOptions[5]);
                        System.Console.WriteLine("Ham added");
                        break;
                    case 7:
                        UserToppings.Add(ToppingOptions[6]);
                        System.Console.WriteLine("Pineapple added");
                        break;
                    case 8:
                        UserToppings.Add(ToppingOptions[7]);
                        System.Console.WriteLine("Mushrooms added");
                        break;
                    case 9:
                        UserToppings.Add(ToppingOptions[8]);
                        System.Console.WriteLine("Mozzarella added");
                        break;
                    case 10:
                        UserToppings.Add(ToppingOptions[9]);
                        System.Console.WriteLine("Basil added");
                        break;
                    case 11:
                        if (UserToppings.Count > 1)
                        {
                            exit = true;
                            System.Console.WriteLine("Toppings selected!");
                        }
                        else
                        {
                            System.Console.WriteLine("You need to select at least 2 toppings");
                        }
                        break;
                    default:
                        System.Console.WriteLine("Whoops, that wasn't an option!");
                        break;
                }

                if (UserToppings.Count == 5)
                {
                    exit = true;
                    System.Console.WriteLine("You have selected all five of your toppings!");
                }
            } while (!exit);

            return UserToppings;
        }

        private static Crust GetUserCrust()
        {
            var CrustOptions = Starter.GenerateCrusts();

            System.Console.WriteLine();
            for (int i = 0; i < CrustOptions.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}: {CrustOptions[i]}");
            }

            do
            {
                int selection;
                System.Console.Write("\nYour choice: ");
                int.TryParse(Console.ReadLine(), out selection);

                switch (selection)
                {
                    case 1:
                        System.Console.WriteLine("Thin crust selected");
                        return CrustOptions[0];
                    case 2:
                        System.Console.WriteLine("Stuffed crust selected");
                        return CrustOptions[1];
                    case 3:
                        System.Console.WriteLine("Garlic crust selected");
                        return CrustOptions[2];
                    case 4:
                        System.Console.WriteLine("Garlic stuffed crust selected");
                        return CrustOptions[3];
                    default:
                        System.Console.WriteLine("Whoops, that wasn't an option!");
                        break;
                }
            } while (true);
        }

        private static Size GetUserSize()
        {
            var SizeOptions = Starter.GenerateSizes();

            System.Console.WriteLine();
            for (int i = 0; i < SizeOptions.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}: {SizeOptions[i]}");
            }

            do
            {
                int selection;
                System.Console.Write("\nYour choice: ");
                int.TryParse(Console.ReadLine(), out selection);

                switch (selection)
                {
                    case 1:
                        System.Console.WriteLine("Small pizza selected");
                        return SizeOptions[0];
                    case 2:
                        System.Console.WriteLine("Medium pizza selected");
                        return SizeOptions[1];
                    case 3:
                        System.Console.WriteLine("Large pizza selected");
                        return SizeOptions[2];
                    default:
                        System.Console.WriteLine("Whoops, that wasn't an option!");
                        break;
                }
            } while (true);
        }
    }
}
