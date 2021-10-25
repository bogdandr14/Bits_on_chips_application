using Authentication.Jwt;
using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Bits_on_chips_application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();

            services.AddDbContext<BitsOnChipsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BitsOnChipsDbContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 5;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(3);
                options.Lockout.MaxFailedAccessAttempts = 100;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ComponentService>();
            services.AddScoped<CartItemService>();
            services.AddScoped<OrderService>();
            services.AddScoped<WishItemService>();
            services.AddScoped<ShipmentMethodService>();
            services.AddScoped<PaymentMethodService>();
            services.AddScoped<UserService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = ConstantsJwt.Issuer,
                    ValidAudience = ConstantsJwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantsJwt.Secret))
                };
            });
            services.AddAuthorization();

            services.AddRazorPages();
            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/Home/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseSession();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                /*endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");*/

                /*endpoints.MapControllerRoute(
                   name: "User",
                   pattern: "{controller=User}/{action=Info}/{id?}");*/

                /*endpoints.MapControllerRoute(
                   name: "Store",
                   pattern: "{controller=Store}/{action=Categories}/{id?}");*/
                /*endpoints.MapControllerRoute(
                   name: "Cart",
                   pattern: "{controller=Store}/{action=ShoppingCart}/{id?}");*/
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
