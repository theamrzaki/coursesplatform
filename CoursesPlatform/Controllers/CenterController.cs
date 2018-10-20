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
            Center c =
                       new Center
                       {
                           name = "Yat",
                           about = "avdshbdjb",
                           Branches = new List<Branch>
                               {    new Branch { name = "Masr El Gdeda" ,abbrev =abbrv("Masr El Gdeda"),id = 1 , courses = 12 , users = 154 , color="bg-aqua"} ,
                                    new Branch { name = "Maadi"         ,abbrev =abbrv( "Maadi"  ),id = 2 , courses = 7 , users = 45, color="bg-green"},
                                    new Branch { name = "Madenet Nasr"  ,abbrev =abbrv("Madenet Nasr"),id = 2 , courses = 7 , users = 45, color="bg-yellow"},
                                    new Branch { name = "West El Balad" ,abbrev =abbrv("West El Balad"),id = 2 , courses = 7 , users = 45, color="bg-red"}

                               }
                       };// Center_crud.get_by_center_id(u.center_id);
            ViewBag.Center = c;
            return View();

            if (Session["Logged_in_user"] != null)
            {
                User u = (User)Session["Logged_in_user"];

                Center ccc = 
                        new Center
                        {
                            name = "Yat",
                            about = "avdshbdjb",
                            Branches = new List<Branch>
                                {   new Branch { name = "Masr El Gdeda" ,id = 1 , courses = 12 , users = 154 , color="bg-aqua"} ,
                                    new Branch { name = "Maadi" ,id = 2 , courses = 7 , users = 45, color="bg-green"},
                                    new Branch { name = "Madenet Nasr" ,id = 2 , courses = 7 , users = 45, color="bg-yellow"},
                                    new Branch { name = "West El Balad" ,id = 2 , courses = 7 , users = 45, color="bg-red"}

                                }
                        };// Center_crud.get_by_center_id(u.center_id);
                ViewBag.Center = c;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        private string abbrv(string Name)
        {
            List<char> letters = Name.Split(' ').Select(y => y[0]).ToList();
            StringBuilder output = new StringBuilder();
            foreach (var item in letters)
            {
                output.Append(item);
            }
            return output.ToString();
        }
    }
}