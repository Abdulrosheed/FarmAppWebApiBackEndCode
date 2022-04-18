using FirstProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<FarmInspector> FarmInspectors { get; set; }
        public DbSet<FarmProduce> FarmProduces { get; set; }
        public DbSet<FarmProduceFarm> FarmProduceFarms { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CategoryFarmProduce> CategoryFarmProduces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<FarmReport> FarmReports { get; set; }
        public DbSet<FarmProduct> FarmProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<FarmProductRequest> FarmProductRequests { get; set; }
        public DbSet<OrderFarmProduct> OrderFarmProducts { get; set; }

        


    }
}