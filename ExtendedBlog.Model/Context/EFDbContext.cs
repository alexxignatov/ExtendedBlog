using ExtendedBlog.Model.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Context
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=EFDbContext")
        {

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<FileDescription> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Post>()
                        .HasMany(p => p.Tags)
                        .WithMany(p => p.Posts)
                        .Map(p =>
                        {
                            p.MapLeftKey("PostId");
                            p.MapRightKey("TagId");
                            p.ToTable("PostTags");
                        });

            base.OnModelCreating(modelBuilder);
        }
    }
}
