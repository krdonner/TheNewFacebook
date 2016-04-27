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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
