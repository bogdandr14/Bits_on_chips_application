using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class CoolerService : BaseService
    {
        public CoolerService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Cooler> GetCoolers()
        {
            List<Cooler> coolers = repositoryWrapper.Cooler.FindAll().ToList();
            foreach (var item in coolers)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return coolers;
        }

        public List<Cooler> GetCoolersByCondition(Expression<Func<Cooler, bool>> expression)
        {
            List<Cooler> coolers = repositoryWrapper.Cooler.FindByCondition(expression).ToList();
            foreach (var item in coolers)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return coolers;
        }

        public Cooler GetCoolerById(params object[] keyValues)
        {
            Cooler cooler = repositoryWrapper.Cooler.FindById(keyValues);
            cooler.Component = repositoryWrapper.Component.FindById(cooler.ComponentId);
            cooler.Component.Category = repositoryWrapper.Category.FindById(cooler.Component.CategoryId);
            return cooler;
        }

        public void AddCooler(Cooler cooler)
        {
            Component component = repositoryWrapper.Component.Create(cooler.Component);
            cooler.ComponentId = component.ComponentId;
            repositoryWrapper.Cooler.Create(cooler);

        }

        public void UpdateCooler(Cooler cooler)
        {
            Component component = repositoryWrapper.Component.Update(cooler.Component);
            cooler.ComponentId = component.ComponentId;
            repositoryWrapper.Cooler.Update(cooler);
        }

        public void DeleteCooler(Cooler cooler)
        {
            repositoryWrapper.Cooler.Delete(cooler);
        }
    }
}
