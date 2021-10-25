using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Data
{
    public class BitsOnChipsDbContext : IdentityDbContext<ApplicationUser>
    {
        public BitsOnChipsDbContext(DbContextOptions<BitsOnChipsDbContext> options) : base(options)
        {
        }
        public DbSet<Component> DBComponents { get; set; }
        public DbSet<CartItem> DBCarts { get; set; }
        public DbSet<Category> DBCategories { get; set; }
        public DbSet<ApplicationUser> DBApplicationUsers { get; set; }
        public DbSet<Order> DBOrders { get; set; }
        public DbSet<WishItem> DBWishItems { get; set; }
        public DbSet<PaymentMethod> DbPaymentMethods { get; set; }
        public DbSet<ShipmentMethod> DBShipmentMethods { get; set; }
    }
}
