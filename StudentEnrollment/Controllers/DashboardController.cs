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

            //If the user and role aren't null
            if (Session["user"] != null && Session["role"] != null)
            {
                User currentUser = (User)Session["user"];
                //If the user is a student
                if (String.Equals("student", (string)Session["Role"], StringComparison.OrdinalIgnoreCase))
                {
                    Student student = proxy.getStudent(currentUser.ID);
                    currentSections = proxy.getStudentSections(student);
                }
                //If the user is an instructor
                else if (String.Equals("professor", (string)Session["Role"], StringComparison.OrdinalIgnoreCase))
                {
                    Instructor instructor = proxy.getInstructor(currentUser.ID);
                    currentSections = proxy.getInstructorSections(instructor);
                }
                //If the user is an admin
                else if (String.Equals("admin", (string)Session["Role"], StringComparison.OrdinalIgnoreCase))
                {

                }
            }

            

            return View(currentSections);
        }
    }
}
