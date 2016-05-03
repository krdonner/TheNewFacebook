using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewFacebook.Models;

namespace TheNewFacebook.DAL
{
    public class TNFInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TNFContext>
    {
        protected override void Seed(TNFContext context)
        {
            var newsFeed = new List<NewsFeed>
            {

                new NewsFeed {text="This is the first update", updateDate=DateTime.Now, Author="Kristian Donner", likes=4, UserID=1 },
                new NewsFeed {text="This is the second update", updateDate=DateTime.Now, Author="Kristian Donner", likes=5, UserID=1 }
            };

            newsFeed.ForEach(s => context.NewsFeed.Add(s));
            context.SaveChanges();

            var groups = new List<Groups>
            {

                new Groups {Name = "Friluftsliv" },
                new Groups {Name = "Hockey" }
            };
            groups.ForEach(s => context.Groups.Add(s));
            context.SaveChanges();
        }
    }
}
