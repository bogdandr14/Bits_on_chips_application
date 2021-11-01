using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class CaseService : BaseService
    {
        public CaseService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Case> GetCases()
        {
            List<Case> cases = repositoryWrapper.Case.FindAll().ToList();
            foreach(var item in cases){
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return cases;
        }

        public List<Case> GetCasesByCondition(Expression<Func<Case, bool>> expression)
        {
            List<Case> items = repositoryWrapper.Case.FindByCondition(expression).ToList();
            foreach (var item in items)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return items;
        }

        public Case GetCaseById(params object[] keyValues)
        {
            Case item = repositoryWrapper.Case.FindById(keyValues);
            item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            return item;
        }

        public void AddCase(Case item)
        {
            Component component = repositoryWrapper.Component.Create(item.Component);
            item.ComponentId = component.ComponentId;
            repositoryWrapper.Case.Create(item);
        }

        public void UpdateCase(Case item)
        {
            Component component = repositoryWrapper.Component.Update(item.Component);
            item.ComponentId = component.ComponentId;
            repositoryWrapper.Case.Update(item);
        }

        public void DeleteCase(Case item)
        {
            repositoryWrapper.Case.Delete(item);
        }
    }
}
