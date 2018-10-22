using CoursesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Controllers
{
    public class CourseController : Controller
    {
        #region Add
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Course C)
        {
            return View();
        } 
        #endregion
    }
}