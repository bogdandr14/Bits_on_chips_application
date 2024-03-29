﻿using Bits_on_chips_application.Repositories.Repository_interfaces;
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
        IWishItemRepository WishItem { get; }
        IShipmentMethodRepository ShipmentMethod { get; }
        IPaymentMethodRepository PaymentMethod { get; }
        ICaseRepository Case { get; }
        ICoolerRepository Cooler { get; }
        ICpuRepository Cpu { get; }
        IGpuRepository Gpu { get; }
        IHddRepository Hdd { get; }
        IMotherboardRepository Motherboard { get; }
        IRamRepository Ram { get; }
        ISourceRepository Source { get; }
        ISsdRepository Ssd { get; }

        void Save();
    }
}
