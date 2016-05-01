using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheNewFacebook.Models;

namespace TheNewFacebook.Controllers
{
    public class ProfilePageController : Controller
    {
        // GET: ProfilePage
        public ActionResult Index()
        {
            var users = new Users
            {
                FirstName = "Tomas",
                LastName = "Donner",
                Email = "thomas.donner@mail.com",
                Password = "abc123",
                Image = "http://www.abf.se/ImageVaultFiles/id_6739/cf_77/j-rgen.JPG",
                City = "Malmö",
                Phone = "0709 62 44 12",
                RelationshipStatus = "Married to Heather Wall",
                Workplace = "Dentist at Oral care London"
            };

            return View(users);
        }
    }
}