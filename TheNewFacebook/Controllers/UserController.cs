using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheNewFacebook.DAL;
using TheNewFacebook.Models;

namespace TheNewFacebook.Controllers
{
    public class UserController : Controller
    {
        private TNFContext db = new TNFContext();

        // GET: User
        [HttpPost]
        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Index(string searchString)
        {

            var splitted = searchString.Split(' ');
            string firstName = splitted[0];
            string lastName = splitted[1];
            Debug.WriteLine("FirstNAME " + firstName);
            Debug.WriteLine("LastName " + lastName);

            var tempID = db.Users.Where(u => u.FirstName.Equals(firstName) && u.LastName.Equals(lastName))
               .Select(u => new
               {
                   ID = u.ID
               }).Single();
            var userID = tempID.ID;

            var user = from a in db.Users select a;
            user = user.Where(a => a.ID.Equals(userID));

            var newsfeed = from s in db.NewsFeed select s;
            newsfeed = newsfeed.Where(s => s.UserID.Equals(userID));

            ProfilePageViewModel profilePageViewModel = new ProfilePageViewModel();
            profilePageViewModel.NewsFeed = newsfeed;
            profilePageViewModel.User = user;
            return View(profilePageViewModel);
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Password,Image,City,Phone,RelationshipStatus,Workplace,Email")] Users users)
        {
            if (ModelState.IsValid)
            {
                Session["FirstName"] = users.FirstName;
                Session["LastName"] = users.LastName;
                Session["Email"] = users.Email;
                Session["Name"] = users.FirstName + " " + users.LastName;
                Debug.WriteLine(Session["User"]);
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("ProfilePage");
            }

            return View(users);
        }

        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult ProfilePage()
        {
            Debug.WriteLine("KOMMER TILL PROFILEPAGE");
            string Email = Session["Email"].ToString();

            var feed = db.Users.Where(u => u.Email == Email)
            .Select(u => new
            {
                ID = u.ID
            }).Single();
            var id = feed.ID;

            var user = from a in db.Users select a;
            user = user.Where(a => a.Email.Equals(Email));

            var newsfeed = from s in db.NewsFeed select s;
            newsfeed = newsfeed.Where(s => s.UserID.Equals(id));

            var tempUserID = db.Users.Where(u => u.Email == Email)
            .Select(u => new
            {
                ID = u.ID
            }).Single();
            var usersID = tempUserID.ID;

            string connStr = ConfigurationManager.ConnectionStrings["TNFContext"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TNFContext"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[GroupUser] WHERE UserID=@id", connection);
            command.Parameters.AddWithValue("@id", usersID);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                List<int> mylist = new List<int>();
                while (reader.Read())
                {
                    mylist.Add(reader.GetInt32(0));
                }

                getGroupInfo(mylist);


            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();





            ProfilePageViewModel profilePageViewModel = new ProfilePageViewModel();
            profilePageViewModel.NewsFeed = newsfeed;
            profilePageViewModel.User = user;

            return View(profilePageViewModel);
        }

        public void getGroupInfo(List<int> mylist)
        {

            foreach (var listitem in mylist)
            {
                string connStr = ConfigurationManager.ConnectionStrings["TNFContext"].ConnectionString;
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TNFContext"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Groups] WHERE ID=@id", connection);
                command.Parameters.AddWithValue("@id", listitem);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string name = reader.GetString(1);
                        ViewBag.Name = name;

                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }

        }

        public ActionResult Login()
        {



            return View();
        }



        [HttpPost]
        public ActionResult Login([Bind(Include = "Password,Email")] Users users)
        {
            Session["Password"] = users.Password;
            Session["Email"] = users.Email;

            var password = db.Users.Where(u => u.Email == users.Email)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Password = u.Password
                }).Single();
            var pass = password.Password;
            Session["Name"] = password.FirstName + " " + password.LastName;

            Debug.WriteLine(Session["Password"].ToString());
            Debug.WriteLine(pass.ToString());



            if (Session["Password"].ToString().Equals(pass.ToString()))
            {
                Debug.WriteLine("INNE I IF");
                return RedirectToAction("ProfilePage", "User");

            }
            else
            {
                Debug.WriteLine("INNE I ELSE");
                return RedirectToAction("Login", "User");

            }

        }

        [LayoutInjecter("_Layout")]
        public ActionResult LogOff()
        {

            return View("Login", "~/Views/Shared/_Layout.cshtml");
        }

        // GET: User/Edit/5
        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Password,Email,Image,City,Phone,RelationshipStatus,Workplace")] Users users)
        {


            //if (ModelState.IsValid)
            //{
            Debug.WriteLine("INNE I ISVALID");
            db.Entry(users).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ProfilePage");
            //}
            //return View(users);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class LayoutInjecterAttribute : ActionFilterAttribute
    {
        private readonly string _masterName;
        public LayoutInjecterAttribute(string masterName)
        {
            _masterName = masterName;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = _masterName;
            }
        }
    }
}
