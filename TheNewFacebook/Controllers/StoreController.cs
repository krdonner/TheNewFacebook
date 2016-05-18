using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheNewFacebook.DAL;
using TheNewFacebook.Models;

namespace TheNewFacebook.Controllers
{
    public class StoreController : Controller
    {
        TNFContext dB = new TNFContext();


        // GET: Store
        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Index()
        {

            var shirts = dB.Shirts.ToList();
            ShirtsViewModel shirtsViewModel = new ShirtsViewModel();
            shirtsViewModel.Shirts = shirts;
            

            return View(shirtsViewModel);
        }

        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Details(int id)
        {
            var shirt = dB.Shirts.Find(id);

            return View(shirt);
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