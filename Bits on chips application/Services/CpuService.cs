using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class CpuService : BaseService
    {
        public CpuService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<Cpu> GetCpus()
        {
            List<Cpu> cpus = repositoryWrapper.Cpu.FindAll().ToList();
            foreach (var item in cpus)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
            }
            return cpus;
        }

        public List<Cpu> GetCpusByCondition(Expression<Func<Cpu, bool>> expression)
        {
            List<Cpu> cpus = repositoryWrapper.Cpu.FindByCondition(expression).ToList();
            foreach (var item in cpus)
            {
                item.Component = repositoryWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = repositoryWrapper.Category.FindById(item.Component.CategoryId);
            }
            return cpus;
        }

        public Cpu GetCpuById(params object[] keyValues)
        {
            Cpu cpu = repositoryWrapper.Cpu.FindById(keyValues);
            cpu.Component = repositoryWrapper.Component.FindById(cpu.ComponentId);
            cpu.Component.Category = repositoryWrapper.Category.FindById(cpu.Component.CategoryId);
            return cpu;
        }

        public void AddCpu(Cpu cpu)
        {
            Component component = repositoryWrapper.Component.Create(cpu.Component);
            cpu.ComponentId = component.ComponentId;
            repositoryWrapper.Cpu.Create(cpu);

        }

        public void UpdateCpu(Cpu cpu)
        {
            Component component = repositoryWrapper.Component.Update(cpu.Component);
            cpu.ComponentId = component.ComponentId;
            repositoryWrapper.Cpu.Update(cpu);
        }

        public void DeleteCpu(Cpu cpu)
        {
            repositoryWrapper.Cpu.Delete(cpu);
        }
    }
}
