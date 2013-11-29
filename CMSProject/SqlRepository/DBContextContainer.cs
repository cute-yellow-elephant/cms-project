using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;
using IndexAttrEF;


namespace SqlRepository
{
    public class DBContextContainer : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DBContextContainer() : base("name=ApointDB") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users).WithMany(u => u.Roles)
                .Map(t => t.MapLeftKey("RoleID")
                    .MapRightKey("UserID")
                    .ToTable("UsersInRoles"));

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Posts).WithMany(p => p.Tags)
                .Map(t => t.MapLeftKey("TagID")
                    .MapRightKey("PostID")
                    .ToTable("TagsInPosts"));
        }
    }
}
