using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(BitsOnChipsDbContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }

}
