using CoursesPlatform.Crud;
using CoursesPlatform.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static CoursesPlatform.Models.Enums;

namespace CoursesPlatform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }
        
        public JsonResult step(string step_number, string content)
        {
            if (step_number == "1")
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
                {
                    // Deserialization from JSON  
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Center));
                    Center center = (Center)deserializer.ReadObject(ms);
                    long id = Center_crud.add(center);
                    Session["register_course_id"] = id;

                    Center_crud.updateStep(id, RegistirationsSteps.Step1);
                }
            }

            else if (step_number == "2")
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
                {
                    // Deserialization from JSON  
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Center));
                    Center center = (Center)deserializer.ReadObject(ms);
                    center.id = Convert.ToInt64(Session["register_course_id"]);

                    Center_crud.updateSocialMedia(center);
                    Center_crud.updateStep(center.id, RegistirationsSteps.Step2);

                }
            }

            else if (step_number == "3")
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
                {
                    // Deserialization from JSON  
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Branch));
                    Branch branch = (Branch)deserializer.ReadObject(ms);
                    branch.center_id = Convert.ToInt64(Session["register_course_id"]);
                    Branch_crud.add(branch);

                    Center_crud.updateStep(branch.center_id, RegistirationsSteps.Step3);
                }
            }

            return Json("chamara", JsonRequestBehavior.AllowGet);
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
    }
}