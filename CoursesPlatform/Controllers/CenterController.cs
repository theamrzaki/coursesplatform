using CoursesPlatform.Crud;
using CoursesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Controllers
{
    public class CenterController : Controller
    {
        public ActionResult Dashboard()
        {
            User u = (User)Session["Logged_in_user"];

            Center c = Center_crud.getCenterByID(u.center_id);
            ViewBag.Center = c;
            return View();
        }

      
    }
}