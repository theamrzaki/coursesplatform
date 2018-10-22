using CoursesPlatform.Crud;
using CoursesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Controllers
{
    public class BranchController : Controller
    {
        #region Add
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Branch b)
        {
            b.center_id = ((User)Session["Logged_in_user"]).center_id;
            Branch_crud.add(b);
            return RedirectToAction("Dashboard","Center");
        } 
        #endregion

        public ActionResult Dashboard(long id)
        {
            Branch b=  Branch_crud.getBranchesByID(id);
            ViewBag.Branch = b;
            return View();
        }
    }
}