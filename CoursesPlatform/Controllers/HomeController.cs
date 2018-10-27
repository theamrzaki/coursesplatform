using CoursesPlatform.Crud;
using CoursesPlatform.Models;
using CoursesPlatform.Models.Helpers;
using CoursesPlatform.Utility.SearchUtility;
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
        #region Register
        public ActionResult Register()
        {
            return View();
        }

        public JsonResult step(string step_number, string content)
        {
            // center deatils
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

            //center socail media
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

            // branch details
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

            // specs & tags
            else if (step_number == "4")
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
                {
                    // Deserialization from JSON  
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(SpecializationTags));
                    SpecializationTags Spec_Tags = (SpecializationTags)deserializer.ReadObject(ms);
                    long id = Convert.ToInt64(Session["register_course_id"]);

                    foreach (var s in Spec_Tags.Specializations)
                    {
                        SpecializationCenter_crud.add(id, s);
                    }

                    foreach (var t in Spec_Tags.Tags)
                    {
                        CenterTag_crud.add(id, t);
                    }

                    Center_crud.updateStep(id, RegistirationsSteps.Step4);
                }
            }

            // user details
            else if (step_number == "5")
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
                {
                    // Deserialization from JSON  
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Models.User));
                    Models.User user = (Models.User)deserializer.ReadObject(ms);
                    user.center_id = Convert.ToInt64(Session["register_course_id"]);
                    user.type = (int)Enums.UsersTypes.CenterAdmin;
                    user.id = User_crud.add(user);

                    Session["Logged_in_user"] = user;

                    Center_crud.updateStep(user.center_id, RegistirationsSteps.Completed);
                }
            }
            return Json("chamara", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Login
        public ActionResult Login()
        {
            test();
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            User loggedin_user = User_crud.Login(u);
            if (loggedin_user != null)
            {
                Session["Logged_in_user"] = loggedin_user;
                return RedirectToAction("Dashboard", "Center");
            }
            else
            {
                return View();
            }
        }
        #endregion
        
        #region Search
        public ActionResult Search()
        {
            return View();
        } 
        #endregion


        public void test()
        {
            List<SearchToken> search_tokeens = SearchToken.getTokens("Android Software Course using java or swift");
        }


      
    }
}