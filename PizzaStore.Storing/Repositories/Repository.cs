using System.Collections.Generic;
using System.Linq;
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
                newOrder.PizzaOrders.Add(new PizzaOrders()
                {
                    Pizza = ConvertToDbPizza(pizza)
                });
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
    }
}