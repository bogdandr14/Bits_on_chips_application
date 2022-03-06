using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class CartItemService : BaseService
    {
        public CartItemService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<CartItem> GetCartItems()
        {
            return repositoryWrapper.CartItem.FindAll().ToList();
        }

        public List<CartItem> GetCartItemsByCondition(Expression<Func<CartItem, bool>> expression)
        {
            List<CartItem> cartItems = repositoryWrapper.CartItem.FindByCondition(expression).ToList();
            foreach (var item in cartItems)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return cartItems;
        }

        public CartItem GetCartItemById(params object[] keyValues)
        {
            CartItem cartItem = repositoryWrapper.CartItem.FindById(keyValues);
            cartItem.Component = repositoryWrapper.Component.FindById(cartItem.ComponentId);
            cartItem.Component.Category = repositoryWrapper.Category.FindById(cartItem.Component.CategoryId);
            return cartItem;
        }

        public void AddCartItem(CartItem cartItem)
        {
            CartItem auxCartItem = GetCartItemsByCondition(o => o.ComponentId == cartItem.ComponentId && o.Id == cartItem.Id && o.OrderId == 1).FirstOrDefault();
            if (auxCartItem == default(CartItem))
            {
                repositoryWrapper.CartItem.Create(cartItem);
            }
            else
            {
                ++auxCartItem.Quantity;
                UpdateCartItem(auxCartItem);
            }
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            repositoryWrapper.CartItem.Update(cartItem);
        }

        public void UpdateCartItemsForOrder(IList<CartItem> cartItems, Order order)
        {
            foreach (var item in cartItems)
            {
                item.OrderId = order.OrderId;
                Component component = repositoryWrapper.Component.FindById(item.ComponentId);
                component.Quantity -= item.Quantity;
                item.PricePaid = component.Price * item.Quantity;
                repositoryWrapper.Component.Update(component);
                UpdateCartItem(item);
            }
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            repositoryWrapper.CartItem.Delete(cartItem);
        }
    }
}
