using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class HddService : BaseService
    {
        public HddService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Hdd> GetHdds()
        {
            List<Hdd> hdds = repositoryWrapper.Hdd.FindAll().ToList();
            foreach (var item in hdds)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return hdds;
        }

        public List<Hdd> GetHddsByCondition(Expression<Func<Hdd, bool>> expression)
        {
            List<Hdd> hdds = repositoryWrapper.Hdd.FindByCondition(expression).ToList();
            foreach (var item in hdds)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return hdds;
        }

        public Hdd GetHddById(params object[] keyValues)
        {
            Hdd hdd = repositoryWrapper.Hdd.FindById(keyValues);
            hdd.Component = repositoryWrapper.Component.FindById(hdd.ComponentId);
            hdd.Component.Category = repositoryWrapper.Category.FindById(hdd.Component.CategoryId);
            return hdd;
        }

        public void AddHdd(Hdd hdd)
        {
            Component component = repositoryWrapper.Component.Create(hdd.Component);
            hdd.ComponentId = component.ComponentId;
            repositoryWrapper.Hdd.Create(hdd);

        }

        public void UpdateHdd(Hdd hdd)
        {
            Component component = repositoryWrapper.Component.Update(hdd.Component);
            hdd.ComponentId = component.ComponentId;
            repositoryWrapper.Hdd.Update(hdd);
        }

        public void DeleteHdd(Hdd hdd)
        {
            repositoryWrapper.Hdd.Delete(hdd);
        }
    }
}
