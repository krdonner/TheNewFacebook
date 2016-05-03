using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheNewFacebook.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string RelationshipStatus { get; set; }
        public string Workplace { get; set; }

        public virtual ICollection<NewsFeed> NewsFeed { get; set; }
    }
}