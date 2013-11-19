using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;


namespace SqlRepository
{
    public class DBContextContainer : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DBContextContainer() : base("name=ApointDB") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users).WithMany(u => u.Roles)
                .Map(t => t.MapLeftKey("RoleID")
                    .MapRightKey("UserID")
                    .ToTable("RolesUsers"));
        }
    }
}
