using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class SsdService : BaseService
    {
        public SsdService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Ssd> GetSsds()
        {
            List<Ssd> ssds = repositoryWrapper.Ssd.FindAll().ToList();
            foreach (var item in ssds)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return ssds;
        }

        public List<Ssd> GetSsdsByCondition(Expression<Func<Ssd, bool>> expression)
        {
            List<Ssd> ssds = repositoryWrapper.Ssd.FindByCondition(expression).ToList();
            foreach (var item in ssds)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return ssds;
        }

        public Ssd GetSsdById(params object[] keyValues)
        {
            Ssd ssd = repositoryWrapper.Ssd.FindById(keyValues);
            ssd.Component = repositoryWrapper.Component.FindById(ssd.ComponentId);
            ssd.Component.Category = repositoryWrapper.Category.FindById(ssd.Component.CategoryId);
            return ssd;
        }

        public void AddSsd(Ssd ssd)
        {
            Component component = repositoryWrapper.Component.Create(ssd.Component);
            ssd.ComponentId = component.ComponentId;
            repositoryWrapper.Ssd.Create(ssd);
        }

        public void UpdateSsd(Ssd ssd)
        {
            Component component = repositoryWrapper.Component.Update(ssd.Component);
            ssd.ComponentId = component.ComponentId;
            repositoryWrapper.Ssd.Update(ssd);
        }

        public void DeleteSsd(Ssd ssd)
        {
            repositoryWrapper.Ssd.Delete(ssd);
        }
    }
}
