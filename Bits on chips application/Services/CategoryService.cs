using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Services
{
    public class CategoryService : BaseService
    {
        public CategoryService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Category> GetCategories()
        {
            return repositoryWrapper.Category.FindAll().ToList();
        }

        public List<Category> GetCategoriesByCondition(Expression<Func<Category, bool>> expression)
        {
            return repositoryWrapper.Category.FindByCondition(expression).ToList();
        }

        public Category GetCategoryById(params object[] keyValues)
        {
            return repositoryWrapper.Category.FindById(keyValues);
        }

        public void AddCategory(Category category)
        {
            repositoryWrapper.Category.Create(category);
        }

        public void UpdateCategory(Category category)
        {
            repositoryWrapper.Category.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            repositoryWrapper.Category.Delete(category);
        }
    }
}
