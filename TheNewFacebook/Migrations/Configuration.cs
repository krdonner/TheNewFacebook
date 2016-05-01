namespace TheNewFacebook.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TheNewFacebook.DAL.TNFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TheNewFacebook.DAL.TNFContext";
        }

        protected override void Seed(TheNewFacebook.DAL.TNFContext context)
        {
            /*
            var newsFeed = new List<NewsFeed>
            {

                new NewsFeed {text="This is the first update", updateDate=DateTime.Now, Author="Kristian Donner", likes=4, UserID=1 },
                new NewsFeed {text="This is the second update", updateDate=DateTime.Now, Author="Kristian Donner", likes=5, UserID=1 },
            };

            newsFeed.ForEach(s => context.NewsFeed.Add(s));
            context.SaveChanges();*/
        }
    }
}
