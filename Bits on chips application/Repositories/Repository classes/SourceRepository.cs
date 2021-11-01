﻿using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Repositories.Repository_interfaces;
using Repositories;

namespace Bits_on_chips_application.Repositories.Repository_classes
{
    public class SourceRepository : RepositoryBase<Source>, ISourceRepository
    {
        public SourceRepository(BitsOnChipsDbContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
