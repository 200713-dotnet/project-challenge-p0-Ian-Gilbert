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

            toppings.Add(new Topping("Tomato Sauce", 0.25m));
            toppings.Add(new Topping("Cheese", 0.25m));
            toppings.Add(new Topping("Pepperoni", 0.50m));
            toppings.Add(new Topping("Sausage", 0.50m));
            toppings.Add(new Topping("Olives", 0.50m));
            toppings.Add(new Topping("Ham", 0.50m));
            toppings.Add(new Topping("Pineapple", 0.50m));
            toppings.Add(new Topping("Mushrooms", 0.50m));
            toppings.Add(new Topping("Mozzarella Cheese", 0.50m));
            toppings.Add(new Topping("Basil", 0.25m));

            return toppings;
        }

        internal static List<Pizza> GeneratePresets()
        {
            var pizzas = new List<Pizza>();
            var toppings = GenerateToppings();

            pizzas.Add(new Pizza("Cheese", new List<Topping> { toppings[0], toppings[1] }));
            pizzas.Add(new Pizza("Pepperoni", new List<Topping> { toppings[0], toppings[1], toppings[2] }));
            pizzas.Add(new Pizza("Hawaiian", new List<Topping> { toppings[0], toppings[1], toppings[5], toppings[6] }));
            pizzas.Add(new Pizza("Custom", new List<Topping> { }));

            return pizzas;
        }

        internal static List<Size> GenerateSizes()
        {
            var sizes = new List<Size>();

            sizes.Add(new Size("S", 5.0m));
            sizes.Add(new Size("M", 7.0m));
            sizes.Add(new Size("L", 10.0m));

            return sizes;
        }

        internal static List<Crust> GenerateCrusts()
        {
            var sizes = new List<Crust>();

            sizes.Add(new Crust("Thin", 5.0m));
            sizes.Add(new Crust("Stuffed", 7.0m));
            sizes.Add(new Crust("Garlic", 7.0m));
            sizes.Add(new Crust("Garlic Stuffed", 10.0m));

            return sizes;
        }
    }
}