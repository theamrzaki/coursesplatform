using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Controllers
{
    public class BranchController : Controller
    {
        public ActionResult Dashboard()
        {
            if (Session["Logged_in_user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Home");
            }
        }
    }
}