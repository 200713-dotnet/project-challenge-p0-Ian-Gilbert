using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using domain = PizzaStore.Domain.Models;

namespace PizzaStore.Storing.Repositories
{
    public class Repository
    {
        private PizzaStoreDbContext _db = new PizzaStoreDbContext();

        private Pizza ConvertToDbPizza(domain.Pizza pizza)
        {
            var newPizza = new Pizza();

            newPizza.Crust = _db.Crust.FirstOrDefault(c => c.Name == pizza.Crust.Name);
            newPizza.Size = _db.Size.FirstOrDefault(s => s.Name == pizza.Size.Name);
            newPizza.Name = pizza.Name;
            newPizza.Price = pizza.CalculatePrice();

            foreach (var topping in pizza.Toppings)
            {
                newPizza.PizzaTopping.Add(new PizzaTopping()
                {
                    Topping = _db.Toppings.FirstOrDefault(t => t.Name == topping.Name)
                });
            }

            return newPizza;
        }

        public void CreateOrder(domain.Order order, domain.User user, domain.Store store)
        {
            var newOrder = new Orders();

            newOrder.Price = order.CalculatePrice();
            newOrder.UserSubmitted = _db.Users.FirstOrDefault(u => u.Name == user.Name);
            newOrder.StoreSubmitted = _db.Store.FirstOrDefault(s => s.Name == store.Name);

            foreach (var pizza in order.Pizzas)
            {
                newOrder.Pizza.Add(ConvertToDbPizza(pizza));
            }

            _db.Orders.Add(newOrder);
            _db.SaveChanges();
        }

        public List<domain.Topping> ReadToppings()
        {
            var domainToppingsList = new List<domain.Topping>();

            foreach (var topping in _db.Toppings.ToList())
            {
                domainToppingsList.Add(new domain.Topping(topping.Name, topping.Price));
            }

            return domainToppingsList;
        }

        public List<domain.Crust> ReadCrusts()
        {
            var domainCrustsList = new List<domain.Crust>();

            foreach (var crust in _db.Crust.ToList())
            {
                domainCrustsList.Add(new domain.Crust(crust.Name, crust.Price));
            }

            return domainCrustsList;
        }

        public List<domain.Size> ReadSizes()
        {
            var domainSizesList = new List<domain.Size>();

            foreach (var size in _db.Size.ToList())
            {
                domainSizesList.Add(new domain.Size(size.Name, size.Price));
            }

            return domainSizesList;
        }

        public domain.User ReadUser(string name)
        {
            var user = _db.Users.FirstOrDefault(u => u.Name == name);

            return new domain.User() { Name = user.Name };
        }

        public List<domain.User> ReadAllUsers()
        {
            List<domain.User> userList = new List<domain.User>();

            foreach (var user in _db.Users.ToList())
            {
                userList.Add(new domain.User() { Name = user.Name });
            }

            return userList;
        }

        public domain.Store ReadPizzaStore(string name)
        {
            var store = _db.Store.FirstOrDefault(s => s.Name == name);

            return new domain.Store() { Name = store.Name };
        }

        public List<domain.Store> ReadAllStores()
        {
            List<domain.Store> storeList = new List<domain.Store>();

            foreach (var store in _db.Store.ToList())
            {
                storeList.Add(new domain.Store() { Name = store.Name });
            }

            return storeList;
        }

        public void ViewOrdersByUser(domain.User user)
        {
            System.Console.WriteLine($"Viewing orders submitted by {user.Name}\n");
            var orders = _db.Orders.Where(o => o.UserSubmitted.Name == user.Name).Include(o => o.StoreSubmitted).ToList();

            foreach (var order in orders)
            {
                System.Console.WriteLine($"{order.PurchaseDate.ToString("G")} submitted to {order.StoreSubmitted.Name} ----- Order Total: {order.Price.ToString("C2")}");
                var PizzaList = _db.Pizza
                                    .Where(p => p.OrderId == order.OrderId)
                                    .Include(p => p.Size)
                                    .Include(p => p.Crust)
                                    .Include(p => p.PizzaTopping)
                                    .ThenInclude(p => p.Topping)
                                    .ToList();
                foreach (var pizza in PizzaList)
                {
                    System.Console.WriteLine($"    {pizza.Name}, {pizza.Size.Name}, {pizza.Crust.Name} ----- {pizza.Price.ToString("C2")}");
                    System.Console.WriteLine($"    Toppings: {string.Join(", ", pizza.PizzaTopping.Select(t => t.Topping.Name))}");
                    System.Console.WriteLine();
                }
            }
        }

        public void ViewOrdersByStore(domain.Store store)
        {
            System.Console.WriteLine($"Viewing orders submitted to {store.Name}\n");
            var orders = _db.Orders.Where(o => o.StoreSubmitted.Name == store.Name).Include(o => o.UserSubmitted).ToList();

            foreach (var order in orders)
            {
                System.Console.WriteLine($"{order.PurchaseDate.ToString("G")} submitted by {order.UserSubmitted.Name} ----- Order Total: {order.Price.ToString("C2")}");
                var PizzaList = _db.Pizza
                                    .Where(p => p.OrderId == order.OrderId)
                                    .Include(p => p.Size)
                                    .Include(p => p.Crust)
                                    .Include(p => p.PizzaTopping)
                                    .ThenInclude(p => p.Topping)
                                    .ToList();
                foreach (var pizza in PizzaList)
                {
                    System.Console.WriteLine($"    {pizza.Name}, {pizza.Size.Name}, {pizza.Crust.Name} ----- {pizza.Price.ToString("C2")}");
                    System.Console.WriteLine($"    Toppings: {string.Join(", ", pizza.PizzaTopping.Select(t => t.Topping.Name))}");
                    System.Console.WriteLine();
                }
            }
        }

        public void ViewOrdersByStore(domain.Store store, domain.User user)
        {
            System.Console.WriteLine($"Viewing orders submitted by {user.Name} to {store.Name}\n");
            var orders = _db.Orders.Where(
                o => o.StoreSubmitted.Name == store.Name
                && o.UserSubmitted.Name == user.Name
            ).ToList();

            foreach (var order in orders)
            {
                System.Console.WriteLine($"{order.PurchaseDate.ToString("G")} ----- Order Total: {order.Price.ToString("C2")}");
                var PizzaList = _db.Pizza
                                    .Where(p => p.OrderId == order.OrderId)
                                    .Include(p => p.Size)
                                    .Include(p => p.Crust)
                                    .Include(p => p.PizzaTopping)
                                    .ThenInclude(p => p.Topping)
                                    .ToList();
                foreach (var pizza in PizzaList)
                {
                    System.Console.WriteLine($"    {pizza.Name}, {pizza.Size.Name}, {pizza.Crust.Name} ----- {pizza.Price.ToString("C2")}");
                    System.Console.WriteLine($"    Toppings: {string.Join(", ", pizza.PizzaTopping.Select(t => t.Topping.Name))}");
                    System.Console.WriteLine();
                }
            }
        }

        public void ViewWeeklyRevenue(domain.Store store)
        {

        }

        public void ViewMonthlyRevenue(domain.Store store)
        {

        }
    }
}