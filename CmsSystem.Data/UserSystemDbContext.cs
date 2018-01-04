using CmsSystem.Model.Models;
using System.Data.Entity;

namespace CmsSystem.Data
{
    public class UserSystemDbContext : DbContext
    {
        public UserSystemDbContext() : base("UserSystemConnection")
        {
            
        }

        public DbSet<Action> Actions { get; set; }

        public DbSet<ActionRole> ActionRoles { get; set; }

        public DbSet<FunctionRole> FunctionRoles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Function> Functions { get; set; }

        public DbSet<RoleUser> RoleUsers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
