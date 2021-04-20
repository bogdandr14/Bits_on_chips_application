using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Services
{
    public class OrderService : BaseService
    {
        public OrderService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Order> GetOrders()
        {
            return repositoryWrapper.Order.FindAll().ToList();
        }

        public List<Order> GetOrdersByCondition(Expression<Func<Order, bool>> expression)
        {
            return repositoryWrapper.Order.FindByCondition(expression).ToList();
        }

        public Order GetOrderById(params object[] keyValues)
        {
            return repositoryWrapper.Order.FindById(keyValues);
        }

        public List<Order> GetOrdersForUser(string userId)
        {
            IQueryable<CartItem> cartItems = repositoryWrapper.CartItem.FindByCondition(obj => obj.Id == userId && obj.OrderId != 1);
            IQueryable<int> queryable = (from item in cartItems select item.OrderId).Distinct();
            return repositoryWrapper.Order.FindByCondition(obj => queryable.Contains(obj.OrderId)).ToList();
        }

        public void AddOrder(Order order)
        {
            repositoryWrapper.Order.Create(order);
        }

        public void UpdateOrder(Order order)
        {
            repositoryWrapper.Order.Update(order);
        }

        public void DeleteOrder(Order order)
        {
            repositoryWrapper.Order.Delete(order);
        }
    }
}
