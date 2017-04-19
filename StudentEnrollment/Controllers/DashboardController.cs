using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using StudentEnrollment.Proxy;
using Newtonsoft.Json;
using System.IO;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class DashboardController : Controller
    {
        User currentUser;
        Section[] currentSections;
        public ActionResult SectionsList()
        {
            ViewData["title"] = "Enrollment Dashboard";

            IProxy proxy = new APIProxy();

            //If the user is a student
            if(currentUser.GetType() == typeof(Student))
            {
                //Student currentStudent = new Student(1, "student", "student@gmail.com", "Stue", "Dent", 3, 2.3f);
                currentSections = proxy.getStudentSections((Student)currentUser);
            }
            //If the user is a student
            else if (currentUser.GetType() == typeof(Instructor))
            {
                currentSections = proxy.getInstructorSections((Instructor)currentUser);
            }
            //If the user is an admin
            else if (currentUser.GetType() == typeof(Admin))
            {

            }

            return View(currentSections);
        }
    }
}
