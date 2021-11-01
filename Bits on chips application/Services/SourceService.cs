using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class SourceService : BaseService
    {
        public SourceService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Source> GetSources()
        {
            List<Source> sources = repositoryWrapper.Source.FindAll().ToList();
            foreach (var item in sources)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return sources;
        }

        public List<Source> GetSourcesByCondition(Expression<Func<Source, bool>> expression)
        {
            List<Source> sources = repositoryWrapper.Source.FindByCondition(expression).ToList();
            foreach (var item in sources)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return sources;
        }

        public Source GetSourceById(params object[] keyValues)
        {
            Source source = repositoryWrapper.Source.FindById(keyValues);
            source.Component = repositoryWrapper.Component.FindById(source.ComponentId);
            source.Component.Category = repositoryWrapper.Category.FindById(source.Component.CategoryId);
            return source;
        }

        public void AddSource(Source source)
        {
            Component component = repositoryWrapper.Component.Create(source.Component);
            source.ComponentId = component.ComponentId;
            repositoryWrapper.Source.Create(source);
        }

        public void UpdateSource(Source source)
        {
            Component component = repositoryWrapper.Component.Update(source.Component);
            source.ComponentId = component.ComponentId;
            repositoryWrapper.Source.Update(source);
        }

        public void DeleteSource(Source source)
        {
            repositoryWrapper.Source.Delete(source);
        }
    }
}
