using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TheNewFacebook.DAL;
using TheNewFacebook.Models;

namespace TheNewFacebook.Controllers
{
    public class NewsFeedsController : Controller
    {
        private TNFContext db = new TNFContext();

        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult AddNewsFeedFromProfilePage()
        {


            return PartialView("_AddNewsFeed");
        }

        /* [LayoutInjecter("_LayoutLoggedIn")]
         public ActionResult CommentsView([Bind(Include = "ID,text,Author,NewsFeedId,userId")] Comments comment)
         {

             //NewsFeed nf = new NewsFeed();
            // nf.Comments.Add(new Comments { })
             Debug.WriteLine("INNE I COMMENTSVIEW");
             var comments = new List<Comments>();

             var s =
                        from o in db.Comments
                        where o.NewsFeedId == 1
                        select o;
             foreach (var com in s)
             {
                 var cl = new Comments
                 {
                     Author = com.Author,
                     Text = com.Text
                 };
                 Debug.WriteLine("Kollar så att det finns comments    " + cl);
                 comments.Add(cl);

             }

             Debug.WriteLine(comments[0].Author + "    " + comments[0].Text);
             return PartialView("_Comments", comments);
         }*/


        // GET: NewsFeeds
        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Index()
        {

            var newsFeed = db.NewsFeed.ToList();
            var comments = db.Comments.ToList();

            foreach(var item in newsFeed)
            {
                foreach(var comment in comments)
                {
                    if(comment.NewsFeedId == item.ID)
                    {
                       // item.Comments = comment;

                    }

                }

            }


            return View(db.NewsFeed.ToList());
        }

        // GET: NewsFeeds/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsFeed newsFeed = db.NewsFeed.Find(id);
            if (newsFeed == null)
            {
                return HttpNotFound();
            }
            return View(newsFeed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromGroup([Bind(Include = "ID,text,updateDate,likes,author,groupname")] NewsFeed newsFeed)
        {
            string email = Session["Email"].ToString();
            string nameOfGroup = Session["CurrentlySelectedGroup"].ToString();



            var userID = db.Users.Where(u => u.Email == email)
            .Select(u => new
            {   
                
                ID = u.ID,
                Author = u.FirstName + " " + u.LastName
            }).Single();
            var id = userID.ID;
            var author = userID.Author;

            Debug.WriteLine("KOMMER IN I CREATE");

            if (ModelState.IsValid)
            {
                var t = new NewsFeed
                {
                    updateDate = DateTime.Now,
                    GroupName = nameOfGroup,
                    text = newsFeed.text,
                    Author = author,
                    likes = 0,
                    UserID = id



                };
                db.NewsFeed.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index", "Groups", new { groupName = nameOfGroup });
            }

            return View(newsFeed);
        }



        // GET: NewsFeeds/Create
        public ActionResult Create()
        {
            Debug.WriteLine("KOMMER IN I CREATE GET");
            return View();
        }

        // POST: NewsFeeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,text,updateDate,likes,author,ImagePath")] NewsFeed newsFeed, HttpPostedFileBase file)
        {

            string imagePath = null;
            string email = Session["Email"].ToString();



            var userID = db.Users.Where(u => u.Email == email)
            .Select(u => new
            {
                ID = u.ID,
                Author = u.FirstName + " " + u.LastName
            }).Single();
            var id = userID.ID;
            var author = userID.Author;

            Debug.WriteLine("KOMMER IN I CREATE");

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(100, 100, true, true);
                    img.Save(HttpContext.Server.MapPath("~/Images/")
                                                          + file.FileName);

                    
                }

                var t = new NewsFeed
                {
                    updateDate = DateTime.Now,
                    text = newsFeed.text,
                    Author = author,
                    likes = 0,
                    UserID = id



                };
                if (file != null)
                {
                    imagePath = "~/Images/" + file.FileName;

                }
                t.ImagePath = imagePath;


                db.NewsFeed.Add(t);
                db.SaveChanges();
                return RedirectToAction("ProfilePage", "User");
            }

            return View(newsFeed);
        }

        // GET: NewsFeeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsFeed newsFeed = db.NewsFeed.Find(id);
            if (newsFeed == null)
            {
                return HttpNotFound();
            }
            return View(newsFeed);
        }

        // POST: NewsFeeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,text,updateDate,likes,author")] NewsFeed newsFeed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsFeed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsFeed);
        }

        // GET: NewsFeeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsFeed newsFeed = db.NewsFeed.Find(id);
            if (newsFeed == null)
            {
                return HttpNotFound();
            }
            return View(newsFeed);
        }

        // POST: NewsFeeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsFeed newsFeed = db.NewsFeed.Find(id);
            db.NewsFeed.Remove(newsFeed);
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
