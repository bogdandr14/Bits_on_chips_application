using Bits_on_chips_application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class WishItemService : BaseService
    {
        public WishItemService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<WishItem> GetWishItems()
        {
            return repositoryWrapper.WishItem.FindAll().ToList();
        }

        public List<WishItem> GetWishItemsByCondition(Expression<Func<WishItem, bool>> expression)
        {
            List<WishItem> wishItems = repositoryWrapper.WishItem.FindByCondition(expression).ToList();
            foreach (var item in wishItems)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return wishItems;
        }

        public WishItem GetWishItemById(params object[] keyValues)
        {
            WishItem wishItem = repositoryWrapper.WishItem.FindById(keyValues);
            wishItem.Component = repositoryWrapper.Component.FindById(wishItem.ComponentId);
            wishItem.Component.Category = repositoryWrapper.Category.FindById(wishItem.Component.CategoryId);
            return wishItem;
        }

        public void AddWishItem(WishItem wishItem)
        {
            WishItem auxCartItem = GetWishItemsByCondition(o => o.ComponentId == wishItem.ComponentId && o.UserId == wishItem.UserId).FirstOrDefault();
            if (auxCartItem == default(WishItem))
            {
                repositoryWrapper.WishItem.Create(wishItem);
            }
        }

        public void UpdateWishItem(WishItem wishItem)
        {
            repositoryWrapper.WishItem.Update(wishItem);
        }

        public void DeleteCartItem(WishItem wishItem)
        {
            repositoryWrapper.WishItem.Delete(wishItem);
        }
    }

}