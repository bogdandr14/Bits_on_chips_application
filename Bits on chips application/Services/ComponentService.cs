using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Services
{
    public class ComponentService : BaseService
    {
        public ComponentService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Component> GetComponents()
        {
            return repositoryWrapper.Component.FindAll().ToList();
        }

        public List<Component> GetComponentsByCondition(Expression<Func<Component, bool>> expression)
        {
            List<Component> components = repositoryWrapper.Component.FindByCondition(expression).ToList();
            foreach (var component in components)
            {
                component.Category = repositoryWrapper.Category.FindById(component.CategoryId);
            }
            return components;
        }

        public Component GetComponentById(params object[] keyValues)
        {
            Component component = repositoryWrapper.Component.FindById(keyValues);
            component.Category = repositoryWrapper.Category.FindById(component.CategoryId);
            return component;
        }

        public void AddComponent(Component component)
        {
            repositoryWrapper.Component.Create(component);
        }

        public void UpdateComponent(Component component)
        {
            repositoryWrapper.Component.Update(component);
        }

        public void DeleteComponent(Component component)
        {
            repositoryWrapper.Component.Delete(component);
        }
    }
}
