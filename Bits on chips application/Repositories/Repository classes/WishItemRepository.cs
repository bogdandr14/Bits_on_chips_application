﻿using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Repositories.Repository_interfaces;
using Repositories;

namespace Bits_on_chips_application.Repositories.Repository_classes
{
    public class WishItemRepository : RepositoryBase<WishItem>, IWishItemRepository
    {
        public WishItemRepository(BitsOnChipsDbContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
