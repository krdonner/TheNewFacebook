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
                new NewsFeed {text="This is the second update", updateDate=DateTime.Now, Author="Kristian Donner", likes=5, UserID=1 },
            };

            newsFeed.ForEach(s => context.NewsFeed.Add(s));
            context.SaveChanges();

            var users = new List<Users>
            {
                new Users
                {
                    FirstName = "Tomas",
                    LastName = "Donner",
                    Email = "thomas.donner@mail.com",
                    Password = "abc123",
                    Image = "http://www.abf.se/ImageVaultFiles/id_6739/cf_77/j-rgen.JPG",
                    City = "London",
                    Phone = "0709 62 44 12",
                    RelationshipStatus = "Married",
                    Workplace = "Dentist Care"
                }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}
