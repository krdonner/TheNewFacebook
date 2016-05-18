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
using TheNewFacebook.SQLHandlers;

namespace TheNewFacebook.Controllers
{

    public class GroupsController : Controller
    {
        GroupsHandler groupsHandler = new GroupsHandler();

        private TNFContext db = new TNFContext();

        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult ShowAllGroups()
        {


            return View(db.Groups.ToList());

        }


        [HttpPost]
        public ActionResult JoinGroup(int idGroup)
        {
            var valueEmail = Session["Email"].ToString();

            var userID = db.Users.Where(u => u.Email == valueEmail)
            .Select(u => new
            {

                ID = u.ID,
                Author = u.FirstName + " " + u.LastName
            }).Single();
            var idUser = userID.ID;

            string connStr = ConfigurationManager.ConnectionStrings["TNFContext"].ConnectionString;
            string sqlStatement = "INSERT INTO [dbo].[GroupUser] (GroupID, UserID) VALUES (@val1, @val2)";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = sqlStatement;
                    comm.CommandType = CommandType.Text;

                    comm.Parameters.AddWithValue("@val1", idGroup);
                    comm.Parameters.AddWithValue("@val2", idUser);
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Debug.WriteLine("Fel: " + e);
                    }
                }
            }

            return RedirectToAction("ProfilePage", "User");

        }


        // GET: Groups
        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Index(string groupName)
        {
            Debug.WriteLine("PRINTING GROUPNAME " + groupName);
            Session["CurrentlySelectedGroup"] = groupName;

            var group = from a in db.Groups select a;
            group = group.Where(a => a.Name.Equals(groupName));
            var newsfeed = from s in db.NewsFeed select s;
            newsfeed = newsfeed.Where(s => s.GroupName.Contains(groupName));
            GroupsViewModel groupsViewModel = new GroupsViewModel();
            groupsViewModel.NewsFeed = newsfeed;
            groupsViewModel.Group = group;


            return View(groupsViewModel);
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Create([Bind(Include = "ID,Name")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(groups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groups);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groups);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groups groups = db.Groups.Find(id);
            db.Groups.Remove(groups);
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


}
