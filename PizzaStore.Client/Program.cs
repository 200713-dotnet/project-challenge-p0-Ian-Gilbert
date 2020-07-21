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
        }

        static void Welcome()
        {
            System.Console.WriteLine("Welcome to Pizza World!");
            System.Console.WriteLine("Certified almost award winning pizza");
            System.Console.WriteLine();

            var starter = new Starter();
            var user = new User();
            var store = new Store();
            var order = starter.CreateOrder(user, store);

            try
            {
                Menu(order);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        static void Menu(Order cart)
        {
            var exit = false;

            Starter.PrintMenu();

            do
            {
                int selection;
                System.Console.Write("Make a selection: ");
                int.TryParse(Console.ReadLine(), out selection);

                switch (selection)
                {
                    case 1:
                        cart.CreatePizza(
                            "L", "stuffed", new List<string> { "cheese" }
                        );
                        System.Console.WriteLine("added Cheese Pizza");
                        break;
                    case 2:
                        cart.CreatePizza(
                            "L", "Stuffed", new List<string> { "cheese", "pepperoni" }
                        );
                        System.Console.WriteLine("added Pepperoni Pizza");
                        break;
                    case 3:
                        cart.CreatePizza(
                            "L", "Stuffed", new List<string> { "cheese", "pinapple", "ham" }
                        );
                        System.Console.WriteLine("added Hawaiian Pizza");
                        break;
                    case 4:
                        cart.CreatePizza(
                            "L", "Stuffed", new List<string> { "custom" }
                        );
                        System.Console.WriteLine("added Custom Pizza");
                        break;
                    case 5:
                        DisplayCart(cart);
                        break;
                    case 6:
                        var fmw = new FileManager();
                        fmw.Write(cart);
                        System.Console.WriteLine("Saved!");
                        break;
                    case 7:
                        var fmr = new FileManager();
                        DisplayCart(fmr.Read());
                        break;
                    case 8:
                        exit = true;
                        System.Console.WriteLine("Thank you, goodbye!");
                        break;
                }

                System.Console.WriteLine();
            } while (!exit);

        }

        private static void DisplayCart(Order cart)
        {
            System.Console.WriteLine("\nHere is your cart:");
            foreach (var pizza in cart.Pizzas)
            {
                System.Console.WriteLine(pizza);
                System.Console.WriteLine();
            }
        }
    }
}
