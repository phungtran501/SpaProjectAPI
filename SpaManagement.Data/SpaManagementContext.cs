using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpaManagement.Domain.Entities;

namespace SpaManagement.Data
{
    public class SpaManagementContext: IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        public SpaManagementContext(DbContextOptions<SpaManagementContext> options, IConfiguration configuration, IServiceProvider serviceProvider ) : base(options)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<AppointmentPlanDetail> AppointmentPlanDetail { get; set; }
        public DbSet<AppointmentProductDetail> AppointmentProductDetail { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<PlanDetail> PlanDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("ApplicationUserToken");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            string defaultName = "Admin";
            string defaultEmail = "admin@gmail.com";
            string password = "1";

            var passwordHasherService = _serviceProvider.CreateScope().ServiceProvider.GetService<PasswordHasher<ApplicationUser>>();

            string roleId = string.Empty;

            var roles = _configuration.GetSection("DefaultRole");

            if (roles.Exists())
            {
                foreach (var role in roles.GetChildren())
                {
                    string id = Guid.NewGuid().ToString();

                    modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                    {
                        Id = id,
                        Name = role.Value,
                        NormalizedName = role.Value.ToUpper(),
                    });

                    if (role.Value == defaultName)
                    {
                        roleId = id;
                    }
                }
            }

            string userId = Guid.NewGuid().ToString();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = userId,
                    UserName = defaultName.ToLower(),
                    NormalizedUserName = defaultName.ToUpper(),
                    Email = defaultEmail,
                    NormalizedEmail = defaultEmail.ToUpper(),              
                    AccessFailedCount = 0,
                    PasswordHash = passwordHasherService.HashPassword(new ApplicationUser
                    {
                        UserName = defaultName,
                        NormalizedUserName = defaultName.ToUpper(),
                        Email = defaultEmail,
                        NormalizedEmail = defaultEmail.ToUpper(),
                    }, password)
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId,
            });

            
        }
    }
}
