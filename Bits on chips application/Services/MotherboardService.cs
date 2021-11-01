using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class MotherboardService : BaseService
    {
        public MotherboardService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Motherboard> GetMotherboards()
        {
            List<Motherboard> motherboards = repositoryWrapper.Motherboard.FindAll().ToList();
            foreach (var item in motherboards)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return motherboards;
        }

        public List<Motherboard> GetMotherboardsByCondition(Expression<Func<Motherboard, bool>> expression)
        {
            List<Motherboard> motherboards = repositoryWrapper.Motherboard.FindByCondition(expression).ToList();
            foreach (var item in motherboards)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return motherboards;
        }

        public Motherboard GetMotherboardById(params object[] keyValues)
        {
            Motherboard motherboard = repositoryWrapper.Motherboard.FindById(keyValues);
            motherboard.Component = repositoryWrapper.Component.FindById(motherboard.ComponentId);
            motherboard.Component.Category = repositoryWrapper.Category.FindById(motherboard.Component.CategoryId);
            return motherboard;
        }

        public void AddMotherboard(Motherboard motherboard)
        {
            Component component = repositoryWrapper.Component.Create(motherboard.Component);
            motherboard.ComponentId = component.ComponentId;
            repositoryWrapper.Motherboard.Create(motherboard);

        }

        public void UpdateMotherboard(Motherboard motherboard)
        {
            Component component = repositoryWrapper.Component.Update(motherboard.Component);
            motherboard.ComponentId = component.ComponentId;
            repositoryWrapper.Motherboard.Update(motherboard);
        }

        public void DeleteMotherboard(Motherboard motherboard)
        {
            repositoryWrapper.Motherboard.Delete(motherboard);
        }
    }
}
