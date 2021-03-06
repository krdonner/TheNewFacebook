﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheNewFacebook.Controllers
{
    public class HomeController : Controller
    {


        [LayoutInjecter("_LayoutLoggedIn")]
        public ActionResult Chat()
        {


            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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