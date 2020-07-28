using System;
using System.Collections.Generic;

namespace PizzaStore.Domain.Models
{
    public class Store
    {
        private List<Order> _orders = new List<Order>();
        public List<Order> Orders
        {
            get
            {
                return _orders;
            }
        }

        private List<Topping> _toppings = new List<Topping>();
        public List<Topping> Toppings
        {
            get
            {
                return _toppings;
            }
        }

        private List<Pizza> _presets = new List<Pizza>();
        public List<Pizza> PizzaPresets
        {
            get
            {
                return _presets;
            }
        }

        private List<Size> _sizes = new List<Size>();
        public List<Size> Sizes
        {
            get
            {
                return _sizes;
            }
        }

        private List<Crust> _crusts = new List<Crust>();
        public List<Crust> Crusts
        {
            get
            {
                return _crusts;
            }
        }

        public string Name { get; set; }

        public Store() { }

        public Store(List<Topping> toppings, List<Size> sizes, List<Crust> crusts, List<Pizza> presets)
        {
            Toppings.AddRange(toppings);
            Sizes.AddRange(sizes);
            Crusts.AddRange(crusts);
            PizzaPresets.AddRange(presets);
        }

        public Store(string name)
        {
            Name = name;
        }

        public Order CreateOrder(User user)
        {
            try
            {
                var order = new Order();

                user.Orders.Add(order);
                Orders.Add(order);

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

        public Pizza CreatePizza(string name, List<Topping> toppings, Order order)
        {
            return order.CreatePizza(name, toppings);
        }

        public void ViewMenu()
        {
            for (int i = 0; i < PizzaPresets.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}: {PizzaPresets[i].Name} Pizza");
            }
        }

        public void ViewSizes()
        {
            for (int i = 0; i < Sizes.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}: {Sizes[i]}");
            }
        }

        public void ViewCrusts()
        {
            for (int i = 0; i < Crusts.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}: {Crusts[i]}");
            }
        }

        public void ViewToppings()
        {
            for (int i = 0; i < Toppings.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}: {Toppings[i]}");
            }
            System.Console.WriteLine($"{Toppings.Count + 1}: Exit");
        }

        public void ViewOrders()
        {

        }

        public void AddSize(Size size, Pizza pizza)
        {
            pizza.Size = size;
        }

        public void AddCrust(Crust crust, Pizza pizza)
        {
            pizza.Crust = crust;
        }

        public void AddToppings(List<Topping> toppings, Pizza pizza)
        {
            pizza.AddToppings(toppings);
        }
    }
}