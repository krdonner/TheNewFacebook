using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheNewFacebook.Models
{
    public class UserInfo : IdentityUser
    {
        public virtual Users Users { get; set; }
    }

    public class Users
    {   
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string RelationshipStatus { get; set; }
        [Required]
        public string Workplace { get; set; }


        public  ICollection<NewsFeed> NewsFeed { get; set; }
        public  ICollection<Groups> Groups { get; set; }
    }
}