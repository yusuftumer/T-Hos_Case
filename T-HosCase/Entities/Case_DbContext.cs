using Microsoft.EntityFrameworkCore;
using T_HosCase.Entities;

namespace T_HosCase.Context
{
    public class Case_DbContext : DbContext
    {
        public Case_DbContext(DbContextOptions<Case_DbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
    }
}
