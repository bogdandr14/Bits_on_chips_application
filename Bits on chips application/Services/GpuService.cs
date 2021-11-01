using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class GpuService : BaseService
    {
        public GpuService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Gpu> GetGpus()
        {
            List<Gpu> gpus = repositoryWrapper.Gpu.FindAll().ToList();
            foreach (var item in gpus)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return gpus;
        }

        public List<Gpu> GetGpusByCondition(Expression<Func<Gpu, bool>> expression)
        {
            List<Gpu> gpus = repositoryWrapper.Gpu.FindByCondition(expression).ToList();
            foreach (var item in gpus)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return gpus;
        }

        public Gpu GetGpuById(params object[] keyValues)
        {
            Gpu gpu = repositoryWrapper.Gpu.FindById(keyValues);
            gpu.Component = repositoryWrapper.Component.FindById(gpu.ComponentId);
            gpu.Component.Category = repositoryWrapper.Category.FindById(gpu.Component.CategoryId);
            return gpu;
        }

        public void AddGpu(Gpu gpu)
        {
            Component component = repositoryWrapper.Component.Create(gpu.Component);
            gpu.ComponentId = component.ComponentId;
            repositoryWrapper.Gpu.Create(gpu);
        }

        public void UpdateGpu(Gpu gpu)
        {
            Component component = repositoryWrapper.Component.Update(gpu.Component);
            gpu.ComponentId = component.ComponentId;
            repositoryWrapper.Gpu.Update(gpu);
        }

        public void DeleteGpu(Gpu gpu)
        {
            repositoryWrapper.Gpu.Delete(gpu);
        }
    }
}
