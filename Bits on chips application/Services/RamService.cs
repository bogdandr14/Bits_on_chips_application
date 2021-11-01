using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class RamService : BaseService
    {
        public RamService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Ram> GetRams()
        {
            List<Ram> rams = repositoryWrapper.Ram.FindAll().ToList();
            foreach (var item in rams)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return rams;
        }

        public List<Ram> GetRamsByCondition(Expression<Func<Ram, bool>> expression)
        {
            List<Ram> rams = repositoryWrapper.Ram.FindByCondition(expression).ToList();
            foreach (var item in rams)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return rams;
        }

        public Ram GetRamById(params object[] keyValues)
        {
            Ram ram = repositoryWrapper.Ram.FindById(keyValues);
            ram.Component = repositoryWrapper.Component.FindById(ram.ComponentId);
            ram.Component.Category = repositoryWrapper.Category.FindById(ram.Component.CategoryId);
            return ram;
        }

        public void AddRam(Ram ram)
        {
            Component component = repositoryWrapper.Component.Create(ram.Component);
            ram.ComponentId = component.ComponentId;
            repositoryWrapper.Ram.Create(ram);
        }

        public void UpdateRam(Ram ram)
        {
            Component component = repositoryWrapper.Component.Update(ram.Component);
            ram.ComponentId = component.ComponentId;
            repositoryWrapper.Ram.Update(ram);
        }

        public void DeleteRam(Ram ram)
        {
            repositoryWrapper.Ram.Delete(ram);
        }
    }
}
