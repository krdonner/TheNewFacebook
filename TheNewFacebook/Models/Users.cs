using System;
using System.Collections.Generic;

namespace TheNewFacebook.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<NewsFeed> NewsFeed { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
    }
}