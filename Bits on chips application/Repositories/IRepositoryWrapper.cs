using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; }
        IComponentRepository Component { get; }
        ICartItemRepository CartItem { get; }
        IOrderRepository Order { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
