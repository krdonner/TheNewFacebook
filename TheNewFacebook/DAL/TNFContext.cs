using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewFacebook.Models;

namespace TheNewFacebook.DAL
{
    public class TNFContext : DbContext
    {

        public TNFContext() : base("TNFContext")
        {

        }

        public DbSet<NewsFeed> NewsFeed { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Comments> Comments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Groups>().
               HasMany(c => c.Users).
               WithMany(p => p.Groups).
               Map(
               m =>
               {
                   m.MapLeftKey("GroupID");
                   m.MapRightKey("UserID");
                   m.ToTable("GroupUser");

               });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
