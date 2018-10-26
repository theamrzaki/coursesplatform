using CoursesPlatform.Crud;
using CoursesPlatform.Models;
using CoursesPlatform.Models.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Controllers
{
    public class CourseController : Controller
    {
        #region Add
        public ActionResult Add(long branch_id)
        {
            ViewBag.branch_id = branch_id;
            return View();
        }

        public JsonResult Add_Submit(string content)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                // Deserialization from JSON  
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Course_Insert_Helper));
                Course_Insert_Helper course = (Course_Insert_Helper)deserializer.ReadObject(ms);
                long id= Course_crud.addCourse(course);
            }
            return Json("chamara", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}