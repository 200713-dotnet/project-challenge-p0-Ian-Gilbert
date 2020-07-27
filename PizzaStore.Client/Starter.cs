using System;
using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client
{
    public class Starter
    {
        internal static List<Topping> GenerateToppings()
        {
            var toppings = new List<Topping>();

            toppings.Add(new Topping("Tomato Sauce", 0.25));
            toppings.Add(new Topping("Cheese", 0.25));
            toppings.Add(new Topping("Pepperoni", 0.50));
            toppings.Add(new Topping("Sausage", 0.50));
            toppings.Add(new Topping("Olives", 0.50));
            toppings.Add(new Topping("Ham", 0.50));
            toppings.Add(new Topping("Pineapple", 0.50));
            toppings.Add(new Topping("Mushrooms", 0.50));
            toppings.Add(new Topping("Mozzarella Cheese", 0.50));
            toppings.Add(new Topping("Basil", 0.25));

            return toppings;
        }

        internal static void PrintMenu()
        {
            System.Console.WriteLine("Select 1 for Cheese Pizza");
            System.Console.WriteLine("Select 2 for Pepperoni Pizza");
            System.Console.WriteLine("Select 3 for Hawaiian Pizza");
            System.Console.WriteLine("Select 4 for Custom Pizza");
            System.Console.WriteLine("Select 5 to exit");
        }

        internal static List<Size> GenerateSizes()
        {
            var sizes = new List<Size>();

            sizes.Add(new Size("S", 5));
            sizes.Add(new Size("M", 7));
            sizes.Add(new Size("L", 10));

            return sizes;
        }

        internal static List<Crust> GenerateCrusts()
        {
            var sizes = new List<Crust>();

            sizes.Add(new Crust("Thin", 5));
            sizes.Add(new Crust("Stuffed", 7));
            sizes.Add(new Crust("Garlic", 7));
            sizes.Add(new Crust("Garlic Stuffed", 10));

            return sizes;
        }
    }
}