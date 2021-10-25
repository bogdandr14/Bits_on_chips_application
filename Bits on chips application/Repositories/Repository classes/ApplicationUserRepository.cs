using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using System;
using System.Linq;

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
