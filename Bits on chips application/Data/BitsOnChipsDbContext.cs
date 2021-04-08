using Bits_on_chips_application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Data
{
    public class BitsOnChipsDbContext: DbContext
    {
        public BitsOnChipsDbContext(DbContextOptions<BitsOnChipsDbContext> options) : base(options)
        {
        }
        public DbSet<Component> DBComponents { get; set; }
        public DbSet<User> DBUsers { get; set; }
        public DbSet<ShoppingCartItem> DBCarts { get; set; }
        public DbSet<Category> DBCategories { get; set; }
    }
}
